namespace Msec.Personify.UpaSyncService.ServiceProcess {
	partial class MainInstaller {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ServiceProcess.ServiceInstaller _mainServiceInstaller;
			System.ServiceProcess.ServiceProcessInstaller _mainServiceProcessInstaller;
			_mainServiceInstaller = new System.ServiceProcess.ServiceInstaller();
			_mainServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
			// 
			// _mainServiceInstaller
			// 
			_mainServiceInstaller.Description = "Synchronizes Personify customer data with the User Profile Service Application in" +
				" SharePoint 2010.";
			_mainServiceInstaller.DisplayName = "MSEC Personify UPA Service";
			_mainServiceInstaller.ServiceName = "Msec.Personify.UpaSyncService";
			_mainServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
			// 
			// _mainServiceProcessInstaller
			// 
			_mainServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.NetworkService;
			_mainServiceProcessInstaller.Password = null;
			_mainServiceProcessInstaller.Username = null;
			// 
			// MainInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            _mainServiceInstaller,
            _mainServiceProcessInstaller});

		}

		#endregion

	}
}