using System;

using Code = Msec.Personify.Services.UniversalWebServiceImpl.Code;

namespace Msec.Personify.Services {
	/// <summary>
	/// Represents a mock service object.
	/// </summary>
	public class MockServiceObject {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MockServiceObject"/> class.
		/// </summary>
		public MockServiceObject() : base() { }

	// Properties
		/// <summary>
		/// Gets or sets an example value.
		/// </summary>
		public Boolean MyBoolean {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets an example value.
		/// </summary>
		public Code MyCode {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets an example value.
		/// </summary>
		public DateTime MyDateTime {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets an example value.
		/// </summary>
		public Double MyDouble {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets an example value.
		/// </summary>
		public Int32 MyInt32 {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets an example value.
		/// </summary>
		public Int64 MyInt64 {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets an example value.
		/// </summary>
		public String MyString {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets an example value.
		/// </summary>
		public String[] MyStringArray {
			get;
			set;
		}
	}
}
