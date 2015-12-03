using System;
using System.ComponentModel;
using System.Threading;
using Msec.Diagnostics;

namespace Msec.Personify.UpaSyncService {
	/// <summary>
	/// Schedules synchronization jobs for the User Profile Application in SharePoint.  This class may not be inherited.
	/// </summary>
	public sealed class UpaSyncJobScheduler : Component {
	// Fields
		/// <summary>
		/// <c>true</c> if this instance is running; otherwise, <c>false</c>.
		/// </summary>
		private Boolean _isRunning;
		/// <summary>
		/// The next date and time to run.
		/// </summary>
		private DateTime _nextRunTime;
		/// <summary>
		/// Controls access to the <see cref="F:_isRunning"/> field.  This field is read-only.
		/// </summary>
		private readonly Object _runningDoor = new Object();
		/// <summary>
		/// The thread on which the scheduling is occurring, or a null reference if this instance is not running.
		/// </summary>
		private Thread _schedulingThread;
		/// <summary>
		/// The amount of time to sleep between checking for times to run.
		/// </summary>
		private TimeSpan _sleepTimeout;
		/// <summary>
		/// The time of day each job should run.
		/// </summary>
		private TimeSpan _timeOfDayToRun;
		/// <summary>
		/// The current synchronization job, or a null reference.
		/// </summary>
		private UpaSyncJob _upaSyncJob;

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:UpaSyncJobScheduler"/> class.
		/// </summary>
		public UpaSyncJobScheduler()
			: base() {
			UpaSyncConfiguration configuration = UpaSyncConfiguration.Instance;
			this._sleepTimeout = configuration.SleepTimeout;
			this._timeOfDayToRun = configuration.TimeOfDayToRun;
		}

	// Properties
		/// <summary>
		/// Gets a value indicating if it is currently time to run a synchronization job.
		/// </summary>
		private Boolean IsTimeToRun {
			get { return DateTime.Now >= this._nextRunTime; }
		}

	// Methods
		/// <summary>
		/// Acts as the main loop for the scheduler.
		/// </summary>
		private void MainLoop() {
			while (this._isRunning) {
				if (!this.IsTimeToRun) {
					this.LogVerbose("Current time {0} is not time to run.  Sleeping for {1}.", DateTime.Now, this._sleepTimeout);
					Thread.Sleep(this._sleepTimeout);
					continue;
				}

				if (this._upaSyncJob == null) {
					this.LogInformation("Creating new synchronization job.");
					this._upaSyncJob = new UpaSyncJob();
					this._upaSyncJob.ExecuteFinished += this.UpaSyncJob_ExecuteFinished;
					this._upaSyncJob.BeginExecute();
				}
				else {
					this.LogWarning("Synchronization job already in progress.  The interval between runs may be too small.");
				}
				this._nextRunTime = DateTime.Today.AddDays(1).Add(this._timeOfDayToRun);
				this.LogInformation("Setting next run time to {0}.", this._nextRunTime);
			}
		}
		/// <summary>
		/// Starts this instance.
		/// </summary>
		public void Start() {
			lock (this._runningDoor) {
				if (!this._isRunning) {
					this._isRunning = true;
					this._schedulingThread = new Thread(this.MainLoop);
					this._schedulingThread.Start();
				}
			}
		}
		/// <summary>
		/// Stops this instance.
		/// </summary>
		public void Stop() {
			lock (this._runningDoor) {
				if (this._isRunning) {
					this._isRunning = false;
					if (this._upaSyncJob != null) {
						this._upaSyncJob.CancelExecute();
						this._upaSyncJob.Dispose();
						this._upaSyncJob = null;
					}
					TimeSpan sixtySeconds = TimeSpan.FromSeconds(60);
					if (!this._schedulingThread.Join(sixtySeconds)) {
						this._schedulingThread.Abort();
					}
					this._schedulingThread = null;
				}
			}
		}
		/// <summary>
		/// Handles the <see cref="E:UpaSyncJob.ExecuteFinished"/> event handler.
		/// </summary>
		/// <param name="sender">The <see cref="T:UpaSyncJob"/> sending the event.</param>
		/// <param name="e">Provides information about the event.</param>
		private void UpaSyncJob_ExecuteFinished(Object sender, ExecuteFinishedEventArgs e) {
			if (this._upaSyncJob != null) {
				this._upaSyncJob.Dispose();
				this._upaSyncJob = null;
			}
		}
	}
}
