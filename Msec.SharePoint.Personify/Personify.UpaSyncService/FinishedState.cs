using System;

namespace Msec.Personify.UpaSyncService {
	/// <summary>
	/// Represents the state in which a job finished.
	/// </summary>
	public enum FinishedState {
		/// <summary>
		/// No state is specified, or the job is still executing.
		/// </summary>
		None = 0,
		/// <summary>
		/// The job completed successfully.
		/// </summary>
		Complete,
		/// <summary>
		/// The job was cancelled.
		/// </summary>
		Cancelled,
		/// <summary>
		/// The job stopped due to an error.
		/// </summary>
		Error
	}
}
