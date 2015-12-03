using System;

namespace Msec.Personify.UpaSyncService {
	/// <summary>
	/// Provides information about how a job finished execution.  This class may not be inherited.
	/// </summary>
	public sealed class ExecuteFinishedEventArgs : EventArgs {
	// Fields
		/// <summary>
		/// The error that occurred, or a null reference.  This field is read-only.
		/// </summary>
		private readonly Exception _error;
		/// <summary>
		/// The state in which the job finished executing.  This field is read-only.
		/// </summary>
		private readonly FinishedState _finishedState;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ExecuteFinishedEventArgs"/> class.
		/// </summary>
		/// <param name="finishedState">The state in which the job finished executing.</param>
		public ExecuteFinishedEventArgs(FinishedState finishedState) : this(finishedState, null) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ExecuteFinishedEventArgs"/> class.
		/// </summary>
		/// <param name="finishedState">The state in which the job finished executing.</param>
		/// <param name="error">The error that occurred during the execution.</param>
		public ExecuteFinishedEventArgs(FinishedState finishedState, Exception error)
			: base() {
			this._finishedState = finishedState;
			this._error = error;
		}

	// Properties
		/// <summary>
		/// Gets the error that occurred, or a null reference.
		/// </summary>
		public Exception Error {
			get { return this._error; }
		}
		/// <summary>
		/// Gets the state in which the job finished executing.
		/// </summary>
		public FinishedState FinishedState {
			get { return this._finishedState; }
		}
	}
}
