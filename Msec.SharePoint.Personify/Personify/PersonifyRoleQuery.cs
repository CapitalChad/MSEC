//using System;
//using System.Collections.Generic;
//using System.Diagnostics.CodeAnalysis;
//using System.Linq;
//using Msec.Collections;
//using Msec.Diagnostics;
//using Msec.Personify.Services;

//using IEnumerable = System.Collections.IEnumerable;
//using IEnumerator = System.Collections.IEnumerator;

//namespace Msec.Personify {
//    /// <summary>
//    /// Represents a query that will search for <see cref="T:PersonifyRole"/> objects.
//    /// </summary>
//    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is not a collection.")]
//    public abstract class PersonifyRoleQuery : Object, IEnumerable<PersonifyRole> {
//        #region private sealed class RoleNamePersonifyRoleQuery : PersonifyRoleQuery {...}
//        /// <summary>
//        /// Represents a role query that will search for a single role by its name.  This class may not be inherited.
//        /// </summary>
//        private sealed class RoleNamePersonifyRoleQuery : PersonifyRoleQuery {
//        // Fields
//            /// <summary>
//            /// The name of the role.  This field is read-only.
//            /// </summary>
//            private readonly String _roleName;

//        // Constructors
//            /// <summary>
//            /// Initializes a new instance of the <see cref="T:RoleNamePersonifyRoleQuery"/> class.
//            /// </summary>
//            /// <param name="roleName">The name of the role.</param>
//            public RoleNamePersonifyRoleQuery(String roleName)
//                : base() {
//                this._roleName = roleName != null ? roleName.Trim() : null;
//            }

//        // Methods
//            /// <summary>
//            /// Returns an enumerator that iterates through the collection.
//            /// </summary>
//            /// <returns>An <see cref="T:IEnumerator&lt;T&gt;"/> that can be used to iterate through the collection.</returns>
//            public override IEnumerator<PersonifyRole> GetEnumerator() {
//                List<PersonifyRole> roles = new List<PersonifyRole>(1);
//                if (this._roleName != null) {
//                    PersonifyRole role;
//                    if (PersonifyRole.TryParse(this._roleName, out role)) {
//                        if (role.Exists) {
//                            roles.Add(role);
//                        }
//                    }
//                }
//                return roles.GetEnumerator();
//            }
//        }
//        #endregion

//        #region private sealed class UserNamePersonifyRoleQuery : PersonifyRoleQuery {...}
//        /// <summary>
//        /// Represents a role query that will search for multiple roles associated to a user.  This class may not be inherited.
//        /// </summary>
//        private sealed class UserNamePersonifyRoleQuery : PersonifyRoleQuery {
//        // Fields
//            /// <summary>
//            /// The user name of the user whose roles should be retrieved.  This field is read-only.
//            /// </summary>
//            private readonly String _userName;

//        // Constructors
//            /// <summary>
//            /// Initializes a new instance of the <see cref="T:UserNamePersonifyRoleQuery"/> class.
//            /// </summary>
//            /// <param name="userName">The user name of the user whose roles should be retrieved.</param>
//            public UserNamePersonifyRoleQuery(String userName)
//                : base() {
//                this._userName = userName != null ? userName.Trim() : null;
//            }

//        // Methods
//            /// <summary>
//            /// Returns an enumerator that iterates through the collection.
//            /// </summary>
//            /// <returns>An <see cref="T:IEnumerator&lt;T&gt;"/> that can be used to iterate through the collection.</returns>
//            public override IEnumerator<PersonifyRole> GetEnumerator() {
//                List<PersonifyRole> roles = new List<PersonifyRole>();
//                if (this._userName != null) {
//                    using (PersonifyUniversalService universalService = PersonifyUniversalService.NewPersonifyUniversalService()) {
//                        Constraint[] constraints = new Constraint[] {
//                            new Constraint(CommitteeMemberData.CustomerUserNameKey, ConstraintOperator.Equals, this._userName),
//                            new Constraint(CommitteeMemberData.ParticipationStatusCodeKey, ConstraintOperator.Equals, "A"),
//                            new Constraint(CommitteeMemberData.BeginDateKey, ConstraintOperator.LessThanOrEqualTo, DateTime.Now.ToString())
//                        };
//                        DateTime unspecifiedEndDate = new DateTime(1, 1, 1);
//                        foreach (CommitteeMemberData committeeMemberData in universalService.GetCommitteeMembers(constraints)) {
//                            DateTime? endDate = committeeMemberData.EndDate;
//                            this.LogVerbose("Checking end date for committee {0} and member {1}: Raw value = {2} and DateTime value = {3}.", committeeMemberData.CommitteeUserName, committeeMemberData.CustomerUserName, committeeMemberData[CommitteeMemberData.EndDateKey], committeeMemberData.EndDate);
//                            Boolean hasValidEndDate = !endDate.HasValue || (endDate.Value >= DateTime.Now || endDate.Value == unspecifiedEndDate);
//                            if (hasValidEndDate) {
//                                String committeeUserName = committeeMemberData.CommitteeUserName;
//                                if (committeeUserName != null) {
//                                    PersonifyRole role = new PersonifyRole(committeeUserName);
//                                    roles.Add(role);
//                                }
//                            }
//                        }
//                    }
//                }
//                return roles.GetEnumerator();
//            }
//        }
//        #endregion

//    // Constructors
//        /// <summary>
//        /// Initializes a new instance of the <see cref="T:PersonifyRoleQuery"/> class.
//        /// </summary>
//        protected PersonifyRoleQuery() : base() { }

//    // Methods
//        /// <summary>
//        /// Returns a new instance of the <see cref="T:PersonifyRoleQuery"/> class that will search by role name.
//        /// </summary>
//        /// <param name="roleName">The name of the role.</param>
//        /// <returns>A reference to the <see cref="T:PersonifyRoleQuery"/> created.</returns>
//        public static PersonifyRoleQuery ByRoleName(String roleName) {
//            return new RoleNamePersonifyRoleQuery(roleName);
//        }
//        /// <summary>
//        /// Returns a new instance of the <see cref="T:PersonifyRoleQuery"/> class that will search by user name.
//        /// </summary>
//        /// <param name="userName">The user name of the user whose roles should be retrieved.</param>
//        /// <returns>A reference to the <see cref="T:PersonifyRoleQuery"/> created.</returns>
//        public static PersonifyRoleQuery ByUserName(String userName) {
//            return new UserNamePersonifyRoleQuery(userName);
//        }
//        /// <summary>
//        /// Returns an enumerator that iterates through the collection.
//        /// </summary>
//        /// <returns>An <see cref="T:IEnumerator&lt;T&gt;"/> that can be used to iterate through the collection.</returns>
//        public abstract IEnumerator<PersonifyRole> GetEnumerator();

//        #region IEnumerable Members (explicit)
//        IEnumerator IEnumerable.GetEnumerator() {
//            return this.GetEnumerator();
//        }
//        #endregion
//    }
//}
