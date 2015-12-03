using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Msec.Diagnostics;

using PropertyInfo = System.Reflection.PropertyInfo;
using Msec.Personify.Services.PersonifyUniversalServiceImpl;

namespace Msec.Personify.Services {
	/// <summary>
	/// The base class for data objects that are returned from services.
	/// </summary>
	[Serializable()]
	public abstract class ServiceDataObject : Object {
	// Fields
		/// <summary>
		/// The properties on the service customer type.  This field is read-only.
		/// </summary>
		private static readonly IDictionary<Type, PropertyInfo[]> _serviceObjectProperties = new Dictionary<Type, PropertyInfo[]>();
		/// <summary>
		/// Controls access to the <see cref="F:_serviceObjectProperties"/> field.  This field is read-only.
		/// </summary>
		private static readonly Object _serviceObjectPropertiesDoor = new Object();
		/// <summary>
		/// The collection of values.  This field is read-only.
		/// </summary>
		private readonly IDictionary<String, Object> _values;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ServiceDataObject"/> class.
		/// </summary>
		/// <param name="values">The collection of fields and values from which to create this instance.</param>
		protected ServiceDataObject(IDictionary<String, Object> values)
			: base() {
			this._values = values != null ? new Dictionary<String, Object>(values) : new Dictionary<String, Object>(0);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ServiceDataObject"/> class.
		/// </summary>
		/// <param name="serviceObject">The service object from which to create this instance.</param>
		[SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "object", Justification = "The term 'serviceObject' is more indicative of the intent of the variable.")]
		protected ServiceDataObject(Object serviceObject)
			: base() {
			if (serviceObject == null) {
				this._values = new Dictionary<String, Object>(0);
			}
			else {
				PropertyInfo[] publicProperties = ServiceDataObject.GetPublicProperties(serviceObject.GetType());
				this._values = new Dictionary<String, Object>(publicProperties.Length);
				this.AppendProperties(serviceObject, publicProperties);
			}
		}

	// Properties
		/// <summary>
		/// Gets the value for the field specified.
		/// </summary>
		/// <param name="fieldName">The name or field name of the value to retrieve.</param>
		/// <returns>The value for the field, or a null reference.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="fieldName"/> is a null reference.</exception>
		public Object this[String fieldName] {
			get { return this.GetValue(fieldName); }
		}
		/// <summary>
		/// Gets the enumerable collection of field names available in the data object.
		/// </summary>
		public IEnumerable<String> FieldNames {
			get { return this._values.Keys; }
		}

	// Methods
		/// <summary>
		/// Appends properties to this instance from the service object specified.
		/// </summary>
		/// <param name="serviceObject">The service object whose values should be appended.</param>
		protected void AppendProperties(Object serviceObject) {
			if (serviceObject == null) {
				return;
			}

			PropertyInfo[] publicProperties = ServiceDataObject.GetPublicProperties(serviceObject.GetType());
			this.AppendProperties(serviceObject, publicProperties);
		}
		/// <summary>
		/// Appends properties to this instance from the service object specified.
		/// </summary>
		/// <param name="serviceObject">The service object whose values should be appended.</param>
		/// <param name="properties">The properties to append.</param>
		private void AppendProperties(Object serviceObject, PropertyInfo[] properties) {
			if (serviceObject == null || properties == null || properties.Length == 0) {
				return;
			}

			foreach (PropertyInfo property in properties) {
				Object value;
				try {
				value = property.GetValue(serviceObject, null);
				}
				catch (Exception ex) {
					if (!ex.CanBeHandledSafely())
						throw;

					this.LogWarning("Property {0} could not be retrieved for service object {1}: {2}", property.Name, serviceObject, ex);
					continue;
				}

				try {
					TypeCode typeCode = Type.GetTypeCode(property.PropertyType);
					switch (typeCode) {
						case TypeCode.Boolean:
						case TypeCode.Byte:
						case TypeCode.Char:
						case TypeCode.DateTime:
						case TypeCode.Decimal:
						case TypeCode.Double:
						case TypeCode.Int16:
						case TypeCode.Int32:
						case TypeCode.Int64:
						case TypeCode.SByte:
						case TypeCode.Single:
						case TypeCode.String:
						case TypeCode.UInt16:
						case TypeCode.UInt32:
						case TypeCode.UInt64:
							if (!this._values.ContainsKey(property.Name)) {
								this._values.Add(property.Name, value);
							}
							break;
						case TypeCode.Object:
						case TypeCode.DBNull:
						case TypeCode.Empty:
						default:
							if (property.PropertyType == typeof(CodeAndDescription)) {
								if (!this._values.ContainsKey(property.Name)) {
									CodeAndDescription code = (CodeAndDescription)value;
									this._values.Add(property.Name, code != null ? code.Code : null);
								}
							}
							// Skip other object types.
							break;
					}
				}
				catch (Exception ex) {
					if (!ex.CanBeHandledSafely())
						throw;

					this.LogWarning("Property {0} with value {1} could not be mapped for service object {2}: {3}", property.Name, value, serviceObject, ex);
				}
			}
		}
		/// <summary>
		/// Returns a nullable date/time object for the field specified.
		/// </summary>
		/// <param name="fieldName">The name or field name of the value to retrieve.</param>
		/// <returns>The value for the field, or a null reference.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="fieldName"/> is a null reference.</exception>
		public DateTime? GetNullableDateTime(String fieldName) {
			Object value = this.GetValue(fieldName);
			if (value != null && value is DateTime) {
				return (DateTime)value;
			}
			return null;
		}
		/// <summary>
		/// Returns a nullable Int32 object for the field specified.
		/// </summary>
		/// <param name="fieldName">The name or field name of the value to retrieve.</param>
		/// <returns>The value for the field, or a null reference.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="fieldName"/> is a null reference.</exception>
		public Int32? GetNullableInt32(String fieldName) {
			Object value = this.GetValue(fieldName);
			if (value != null && value is Int32) {
				return (Int32)value;
			}
			return null;
		}
		/// <summary>
		/// Returns a nullable Int64 object for the field specified.
		/// </summary>
		/// <param name="fieldName">The name or field name of the value to retrieve.</param>
		/// <returns>The value for the field, or a null reference.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="fieldName"/> is a null reference.</exception>
		public Int64? GetNullableInt64(String fieldName) {
			Object value = this.GetValue(fieldName);
			if (value != null && value is Int64) {
				return (Int64)value;
			}
			return null;
		}
		/// <summary>
		/// Returns the public properties for the type specified.
		/// </summary>
		/// <param name="type">The type whose public properties should be retrieved.</param>
		/// <returns>The array of public properties for the type.</returns>
		private static PropertyInfo[] GetPublicProperties(Type type) {
			Debug.Assert(type != null);
			if (!ServiceDataObject._serviceObjectProperties.ContainsKey(type)) {
				lock (ServiceDataObject._serviceObjectPropertiesDoor) {
					if (!ServiceDataObject._serviceObjectProperties.ContainsKey(type)) {
						ServiceDataObject._serviceObjectProperties.Add(type, type.GetProperties());
					}
				}
			}
			return ServiceDataObject._serviceObjectProperties[type];
		}
		/// <summary>
		/// Returns a string object for the field specified.
		/// </summary>
		/// <param name="fieldName">The name or field name of the value to retrieve.</param>
		/// <returns>The value for the field, or a null reference.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="fieldName"/> is a null reference.</exception>
		public String GetString(String fieldName) {
			return this.GetValue(fieldName) as String;
		}
		/// <summary>
		/// Returns the value for the field specified.
		/// </summary>
		/// <param name="fieldName">The name or field name of the value to retrieve.</param>
		/// <returns>The value for the field, or a null reference.</returns>
		/// <exception cref="System.ArgumentNullException"><paramref name="fieldName"/> is a null reference.</exception>
		public Object GetValue(String fieldName) {
			if (fieldName == null) {
				throw new ArgumentNullException("fieldName");
			}
			if (this._values.ContainsKey(fieldName)) {
				return this._values[fieldName];
			}
			this.LogInformation("The field name specified {0} does not exist.", fieldName);
			return null;
		}
	}
}
