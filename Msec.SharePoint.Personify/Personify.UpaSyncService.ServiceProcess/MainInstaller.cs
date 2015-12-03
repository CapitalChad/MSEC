using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;

namespace Msec.Personify.UpaSyncService.ServiceProcess {
	/// <summary>
	/// The main installer for the service.
	/// </summary>
	[RunInstaller(true)]
	public partial class MainInstaller : Installer {
	// Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MainInstaller"/> class.
		/// </summary>
		public MainInstaller()
			: base() {
			this.InitializeComponent();
		}
	}
}
