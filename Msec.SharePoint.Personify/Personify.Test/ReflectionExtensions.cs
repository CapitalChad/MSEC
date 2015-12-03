using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Msec.Personify {
	/// <summary>
	/// Provides reflection-based extension methods.  This class may not be inherited.
	/// </summary>
	internal static class ReflectionExtensions {
	// Methods
		/// <summary>
		/// Creates and returns an instance of the type specified.
		/// </summary>
		/// <param name="type">The type to create.</param>
		/// <param name="args">The array of arguments to supply to the constructor.</param>
		/// <returns>The instance created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="type"/> is a null reference.</exception>
		/// <exception cref="System.Reflection.MissingMemberException">A constructor matching the types specified could not be found.</exception>
		public static Object CreateInstance(this Type type, params Object[] args) {
			Type[] argTypes = null;
			if (args != null) {
				argTypes = args
					.Select(arg => arg != null ? arg.GetType() : typeof(Object))
					.ToArray();
			}
			return ReflectionExtensions.CreateInstance(type, argTypes, args);
		}
		/// <summary>
		/// Creates and returns an instance of the type specified.
		/// </summary>
		/// <param name="type">The type to create.</param>
		/// <param name="argTypes">The array of types accepted in the constructor.</param>
		/// <param name="args">The array of arguments to supply to the constructor.</param>
		/// <returns>The instance created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="type"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException">The length of <paramref name="args"/> does not match the length of <paramref name="argTypes"/>.</exception>
		/// <exception cref="System.Reflection.MissingMemberException">A constructor matching the types specified could not be found.</exception>
		public static Object CreateInstance(this Type type, Type[] argTypes, Object[] args) {
			if (type == null) {
				throw new ArgumentNullException("type");
			}
			if ((argTypes ?? new Type[0]).Length != (args ?? new Object[0]).Length) {
				throw new ArgumentException("The length of the arguments must match the length of the types.", "args");
			}
			
			BindingFlags bindingFlags = BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
			ConstructorInfo constructor = type.GetConstructor(bindingFlags, null, argTypes ?? new Type[0], null);
			if (constructor == null) {
				throw new MissingMemberException("A constructor matching the arguments specified could not be found.");
			}
			try {
				return constructor.Invoke(args ?? new Object[0]);
			}
			catch (TargetInvocationException ex) {
				throw ex.InnerException;
			}
		}
		/// <summary>
		/// Finds a field on the type specified.  Base types are also checked.
		/// </summary>
		/// <param name="type">The type on which to find the field.</param>
		/// <param name="name">The name of the field.</param>
		/// <param name="isStatic"><c>true</c> if the field is static; otherwise, <c>false</c>.</param>
		/// <returns>The field found, or a null reference if it is not found.</returns>
		private static FieldInfo FindField(this Type type, String name, Boolean isStatic) {
			Debug.Assert(type != null);
			Debug.Assert(name != null);

			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
			if (isStatic) {
				bindingFlags |= BindingFlags.Static;
			}
			else {
				bindingFlags |= BindingFlags.Instance;
			}

			Type currentType = type;
			while (currentType != typeof(Object)) {
				foreach (FieldInfo fieldInfo in currentType.GetFields(bindingFlags)) {
					if (fieldInfo.Name == name) {
						return fieldInfo;
					}
				}
				currentType = currentType.BaseType;
			}
			return null;
		}
		/// <summary>
		/// Finds the nested type specified.
		/// </summary>
		/// <param name="type">The type on which to search.</param>
		/// <param name="name">The name of the nested type.</param>
		/// <returns>The type found.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="type"/> is a null reference.
		/// -or- <paramref name="name"/> is a null reference.</exception>
		public static Type FindNestedType(this Type type, String name) {
			if (type == null) {
				throw new ArgumentNullException("type");
			}
			if (name == null) {
				throw new ArgumentNullException("name");
			}

			return type.GetNestedType(name, BindingFlags.Public | BindingFlags.NonPublic);
		}
		/// <summary>
		/// Finds a property on the type specified.  Base types are also checked.
		/// </summary>
		/// <param name="type">The type on which to find the property.</param>
		/// <param name="name">The name of the property.</param>
		/// <param name="isStatic"><c>true</c> if the property is static; otherwise, <c>false</c>.</param>
		/// <returns>The property found, or a null reference if it is not found.</returns>
		private static PropertyInfo FindProperty(this Type type, String name, Boolean isStatic) {
			Debug.Assert(type != null);
			Debug.Assert(name != null);

			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
			if (isStatic) {
				bindingFlags |= BindingFlags.Static;
			}
			else {
				bindingFlags |= BindingFlags.Instance;
			}

			Type currentType = type;
			while (currentType != typeof(Object)) {
				foreach (PropertyInfo propertyInfo in currentType.GetProperties(bindingFlags)) {
					if (propertyInfo.Name == name) {
						return propertyInfo;
					}
				}
				currentType = currentType.BaseType;
			}
			return null;
		}
		/// <summary>
		/// Returns the value of the field specified.
		/// </summary>
		/// <param name="instance">The instance from which to get the field value.  For static fields, this should be the type.</param>
		/// <param name="name">The name of the field.</param>
		/// <returns>The value of the field.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="name"/> is a null reference.</exception>
		/// <exception cref="System.Reflection.MissingMemberException">The field specified cannot be found.</exception>
		public static Object GetFieldValue(this Object instance, String name) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (name == null) {
				throw new ArgumentNullException("name");
			}

			Type type = instance as Type;
			Boolean isStatic = instance is Type;

			FieldInfo field;
			if (isStatic) {
				field = type.FindField(name, true);
			}
			else {
				field = instance.GetType().FindField(name, false);
			}

			if (field == null) {
				throw new MissingMemberException("The field specified {0} does not exist on the type as {1} field.".FormatInvariant(name, isStatic ? "a static" : "an instance"));
			}
			try {
				return field.GetValue(isStatic ? null : instance);
			}
			catch (TargetInvocationException ex) {
				throw ex.InnerException;
			}
		}
		/// <summary>
		/// Returns the value of the property specified.
		/// </summary>
		/// <param name="instance">The instance from which to get the property value.  For static properties, this should be the type.</param>
		/// <param name="name">The name of the property.</param>
		/// <returns>The value of the property.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="name"/> is a null reference.</exception>
		/// <exception cref="System.Reflection.MissingMemberException">The property specified cannot be found.</exception>
		public static Object GetPropertyValue(this Object instance, String name) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (name == null) {
				throw new ArgumentNullException("name");
			}

			Type type = instance as Type;
			Boolean isStatic = instance is Type;

			PropertyInfo property;
			if (isStatic) {
				property = type.FindProperty(name, true);
			}
			else {
				property = instance.GetType().FindProperty(name, false);
			}

			if (property == null) {
				throw new MissingMemberException("The property specified {0} does not exist on the type as {1} property.".FormatInvariant(name, isStatic ? "a static" : "an instance"));
			}
			try {
				return property.GetValue(isStatic ? null : instance, null);
			}
			catch (TargetInvocationException ex) {
				throw ex.InnerException;
			}
		}
		/// <summary>
		/// Invokes a method and returns the result.
		/// </summary>
		/// <param name="instance">The object or type from which to invoke the method.</param>
		/// <param name="name">The name of the method.</param>
		/// <param name="args">The array of arguments to supply to the method.</param>
		/// <returns>The instance created.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		/// <exception cref="System.Reflection.MissingMemberException">A method matching the types specified could not be found.</exception>
		public static Object InvokeMethod(this Object instance, String name, params Object[] args) {
			Type[] argTypes = null;
			if (args != null) {
				argTypes = args
					.Select(arg => arg != null ? arg.GetType() : typeof(Object))
					.ToArray();
			}
			return ReflectionExtensions.InvokeMethod(instance, name, argTypes, args);
		}
		/// <summary>
		/// Invokes a method and returns the result.
		/// </summary>
		/// <param name="instance">The object or type from which to invoke the method.</param>
		/// <param name="name">The name of the method.</param>
		/// <param name="argTypes">The array of argument types expected.</param>
		/// <param name="args">The array of arguments to supply to the method.</param>
		/// <returns>The return result of the method.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.</exception>
		/// <exception cref="System.ArgumentException">The length of <paramref name="args"/> does not match the length of <paramref name="argTypes"/>.</exception>
		/// <exception cref="System.Reflection.MissingMemberException">A method matching the types specified could not be found.</exception>
		public static Object InvokeMethod(this Object instance, String name, Type[] argTypes, Object[] args) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (name == null) {
				throw new ArgumentNullException("name");
			}
			if ((argTypes ?? new Type[0]).Length != (args ?? new Object[0]).Length) {
				throw new ArgumentException("The length of the arguments must match the length of the types.", "args");
			}

			Type type = instance as Type;
			Boolean isStatic = type != null;
			if (type == null) {
				type = instance.GetType();
			}

			BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
			if (isStatic) {
				bindingFlags |= BindingFlags.Static;
			}
			else {
				bindingFlags |= BindingFlags.Instance;
			}

			MethodInfo method = type.GetMethod(name, bindingFlags, null, argTypes ?? new Type[0], null);
			if (method == null) {
				throw new MissingMemberException("A method matching the arguments specified could not be found.");
			}
			try {
				Object target = !isStatic ? instance : null;
				return method.Invoke(target, args ?? new Object[0]);
			}
			catch (TargetInvocationException ex) {
				throw ex.InnerException;
			}
		}
		/// <summary>
		/// Sets a field to a specified value.
		/// </summary>
		/// <param name="instance">The instance on which to set the field.  For static fields, this should be the type.</param>
		/// <param name="name">The name of the field.</param>
		/// <param name="value">The value to which to set the field.</param>
		/// <exception cref="System.ArgumentNullException"><paramref name="instance"/> is a null reference.
		/// -or- <paramref name="name"/> is a null reference.</exception>
		/// <exception cref="System.Reflection.MissingMemberException">The field specified cannot be found.</exception>
		/// <exception cref="System.InvalidCastException">The value specified does not match the type of the field.</exception>
		public static void SetFieldValue(this Object instance, String name, Object value) {
			if (instance == null) {
				throw new ArgumentNullException("instance");
			}
			if (name == null) {
				throw new ArgumentNullException("name");
			}

			Type type = instance as Type;
			Boolean isStatic = instance is Type;

			FieldInfo field;
			if (isStatic) {
				field = type.FindField(name, true);
			}
			else {
				field = instance.GetType().FindField(name, false);
			}

			if (field == null) {
				throw new MissingMemberException("The field specified {0} does not exist on the type as {1} field.".FormatInvariant(name, isStatic ? "a static" : "an instance"));
			}
			if (value == null) {
				if (!field.FieldType.IsByRef) {
					throw new InvalidCastException();
				}
			}
			else {
				if (!field.FieldType.IsAssignableFrom(value.GetType())) {
					throw new InvalidCastException();
				}
			}
			field.SetValue(isStatic ? null : instance, value);
		}
	}
}
