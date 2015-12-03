using System;

namespace Msec.Personify.UpaSyncService.TestHarness {
	class Program {
		static void Main(String[] args) {
			using (UpaSyncJobScheduler scheduler = new UpaSyncJobScheduler()) {
				Console.WriteLine("Press [Enter] to start the service.");
				Console.ReadLine();
				scheduler.Start();
				Console.WriteLine("Press [Enter] to stop the service.");
				Console.ReadLine();
				scheduler.Stop();
			}
		}
	}
}
