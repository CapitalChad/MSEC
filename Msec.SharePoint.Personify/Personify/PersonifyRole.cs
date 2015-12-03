//using System;
//using System.Diagnostics.CodeAnalysis;
//using System.Linq;
//using Msec.Personify.Services;

//using ImmutableObject = System.ComponentModel.ImmutableObjectAttribute;

//namespace Msec.Personify {
//    /// <summary>
//    /// Represents a role from Personify.
//    /// This class may not be inherited.
//    /// </summary>
//    [Serializable()]
//    [ImmutableObject(true)]
//    public sealed class PersonifyRole : Object, IEquatable<PersonifyRole> {
//    // Fields
//        /// <summary>
//        /// The user name of the committee for the role.
//        /// This field is read-only.
//        /// </summary>
//        private readonly String _committeeUserName;

//    // Constructors
//        /// <summary>
//        /// Initializes a new instance of the <see cref="T:PersonifyRole"/> class.
//        /// </summary>
//        /// <param name="committeeUserName">The user name of the committee for the role.</param>
//        /// <exception cref="System.ArgumentNullException"><paramref name="committeeUserName"/> is a null reference.</exception>
//        /// <exception cref="System.ArgumentException"><paramref name="committeeUserName"/> is empty.
//        /// -or- <paramref name="committeeUserName"/> contains only white-space characters.</exception>
//        public PersonifyRole(String committeeUserName)
//            : base() {
//            if (committeeUserName == null)
//                throw new ArgumentNullException("committeeUserName");
//            String trimmedCommitteeName = committeeUserName.Trim();
//            if (trimmedCommitteeName.Length == 0)
//                throw new ArgumentException("The argument may not be empty or contain only white-space characters.", "committeeUserName");

//            this._committeeUserName = trimmedCommitteeName;
//        }

//    // Properties
//        /// <summary>
//        /// Gets the user name of the committee associated with the role.
//        /// </summary>
//        public String CommitteeUserName {
//            get { return this._committeeUserName; }
//        }
//        /// <summary>
//        /// Gets a value indicating if the role exists.
//        /// </summary>
//        public Boolean Exists {
//            get {
//                //using (PersonifyIMService imService = PersonifyIMService.NewPersonifyIMService()) {
//                //    imService.GetRole
//                //}
//                using (PersonifyUniversalService universalService = PersonifyUniversalService.NewPersonifyUniversalService()) {
//                    Constraint constraint = new Constraint(CustomerData.UserNameKey, ConstraintOperator.Equals, this.CommitteeUserName);
//                    CustomerData customerData = universalService.GetCustomers(constraint).FirstOrDefault();
//                    return customerData != null;
//                }
//            }
//        }

//    // Methods
//        /// <summary>
//        /// Returns a value indicating if the object specified is equal to this instance.
//        /// </summary>
//        /// <param name="obj">The object to compare to this instance.</param>
//        /// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
//        public override Boolean Equals(Object obj) {
//            PersonifyRole other = obj as PersonifyRole;
//            return this.Equals(other);
//        }
//        /// <summary>
//        /// Returns a value indicating if the object specified is equal to this instance.
//        /// </summary>
//        /// <param name="other">The object to compare to this instance.</param>
//        /// <returns><c>true</c> if the object specified is equal to this instance; otherwise, <c>false</c>.</returns>
//        public Boolean Equals(PersonifyRole other) {
//            if (other == null)
//                return false;
//            return String.Equals(this._committeeUserName, other._committeeUserName, StringComparison.Ordinal);
//        }
//        /// <summary>
//        /// Returns a value that can be used as a hash code for this instance.
//        /// </summary>
//        /// <returns>The value that can be used as a hash code for this instance.</returns>
//        public override Int32 GetHashCode() {
//            return this._committeeUserName.GetHashCode();
//        }
//        /// <summary>
//        /// Returns the string representation for this instance.
//        /// </summary>
//        /// <returns>The string representation for this instance.</returns>
//        public override String ToString() {
//            return this._committeeUserName;
//        }
//        /// <summary>
//        /// Attempts to parse a string value and create a <see cref="T:PersonifyRole"/> object.
//        /// </summary>
//        /// <param name="s">The string value to parse.</param>
//        /// <param name="result">After the method returns, this value will be the object created, or a null reference.</param>
//        /// <returns><c>true</c> if the object was created successfully; otherwise, <c>false</c>.</returns>
//        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s", Justification = "The term 's' is a convention by Parse and TryParse methods.")]
//        public static Boolean TryParse(String s, out PersonifyRole result) {
//            if (s != null) {
//                String committeeUserName = s.Trim();
//                if (committeeUserName.Length != 0) {
//                    result = new PersonifyRole(committeeUserName);
//                    return true;
//                }
//            }
//            result = null;
//            return false;
//        }

//    // Operators
//        /// <summary>
//        /// Implicitly casts a <see cref="T:PersonifyRole"/> object to a <see cref="T:String"/> object.
//        /// </summary>
//        /// <param name="instance">The object to convert.</param>
//        /// <returns>The <see cref="T:String"/> object created.</returns>
//        public static implicit operator String(PersonifyRole instance) {
//            if (instance == null)
//                return null;
//            return instance.ToString();
//        }
//    }
//}
