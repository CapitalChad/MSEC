using System;
using System.ServiceProcess;

namespace Msec.Personify.UpaSyncService.ServiceProcess {
	/// <summary>
	/// Contains the entry point for the service application.  This class may not be inherited.
	/// </summary>
	internal static class Program {
	// Methods
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		private static void Main() {
			ServiceBase[] servicesToRun = new ServiceBase[] { 
				new MainService() 
			};
			ServiceBase.Run(servicesToRun);
		}
	}
}
