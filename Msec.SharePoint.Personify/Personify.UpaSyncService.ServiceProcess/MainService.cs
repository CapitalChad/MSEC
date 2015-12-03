using System;
using System.ServiceProcess;

namespace Msec.Personify.UpaSyncService.ServiceProcess {
	/// <summary>
	/// Acts as the main service for the service application.  This class may not be inherited.
	/// </summary>
	public sealed partial class MainService : ServiceBase {
	// Fields
		/// <summary>
		/// Schedules synchronization jobs for the User Profile Application.  This field is read-only.
		/// </summary>
		private readonly UpaSyncJobScheduler _upaSyncJobScheduler = new UpaSyncJobScheduler();

	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MainService"/> class.
		/// </summary>
		public MainService()
			: base() {
			this.InitializeComponent();

			if (this.components == null) {
				this.components = new System.ComponentModel.Container();
			}
			this.components.Add(this._upaSyncJobScheduler);
		}

	// Methods
		/// <summary>
		/// Executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically).  Specifies actions to take when the service starts.
		/// </summary>
		/// <param name="args">Data passed by the start command.</param>
		protected override void OnStart(String[] args) {
			this._upaSyncJobScheduler.Start();
		}
		/// <summary>
		/// Executes when a Stop command is sent to the service by the Service Control Manager (SCM).  Specifies actions to take when a service stops running.
		/// </summary>
		protected override void OnStop() {
			this._upaSyncJobScheduler.Stop();
		}
	}
}
