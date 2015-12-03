//using System;
//using System.Collections.Generic;

//namespace Msec.Personify.Services {
//    /// <summary>
//    /// Represents committee member data returned from the Personify services.  This class may not be inherited.
//    /// </summary>
//    [Serializable()]
//    public sealed class CommitteeMemberData : ServiceDataObject {
//    // Constants
//        /// <summary>
//        /// The key for the begin date field = "BeginDate".
//        /// </summary>
//        public const String BeginDateKey = "BeginDate";
//        /// <summary>
//        /// The key for the user name field = "CommitteeMasterCustomer".
//        /// </summary>
//        public const String CommitteeUserNameKey = "CommitteeMasterCustomer";
//        /// <summary>
//        /// The key for the user name field = "MemberMasterCustomer".
//        /// </summary>
//        public const String CustomerUserNameKey = "MemberMasterCustomer";
//        /// <summary>
//        /// The key for the begin date field = "EndDate".
//        /// </summary>
//        public const String EndDateKey = "EndDate";
//        /// <summary>
//        /// The key for the participation status code field = "ParticipationStatusCode".
//        /// </summary>
//        public const String ParticipationStatusCodeKey = "ParticipationStatusCode";
//        /// <summary>
//        /// The key for the user name field = "PositionCode".
//        /// </summary>
//        public const String PositionCodeKey = "PositionCode";

//    // Constructors
//        /// <summary>
//        /// Initializes a new instance of the <see cref="T:CommitteeMemberData"/> class.
//        /// </summary>
//        /// <param name="values">The collection of fields and values from which to create this instance.</param>
//        public CommitteeMemberData(IDictionary<String, Object> values) : base(values) { }
//        internal CommitteeMemberData(CommitteeMember serviceObject) : base(serviceObject) { }

//    // Properties
//        /// <summary>
//        /// Gets the begin date for the membership association.
//        /// </summary>
//        public DateTime? BeginDate {
//            get { return this.GetNullableDateTime(BeginDateKey); }
//        }
//        /// <summary>
//        /// Gets the user name for the committee.
//        /// </summary>
//        public String CommitteeUserName {
//            get { return this.GetString(CommitteeUserNameKey); }
//        }
//        /// <summary>
//        /// Gets the user name for the customer.
//        /// </summary>
//        public String CustomerUserName {
//            get { return this.GetString(CustomerUserNameKey); }
//        }
//        /// <summary>
//        /// Gets the end date for the membership association.
//        /// </summary>
//        public DateTime? EndDate {
//            get { return this.GetNullableDateTime(EndDateKey); }
//        }
//        /// <summary>
//        /// Gets the participation status code for the customer in the committee.
//        /// </summary>
//        public String ParticipationStatusCode {
//            get { return this.GetString(ParticipationStatusCodeKey); }
//        }
//        /// <summary>
//        /// Gets the position code for the customer in the committee.
//        /// </summary>
//        public String PositionCode {
//            get { return this.GetString(PositionCodeKey); }
//        }
//    }
//}
