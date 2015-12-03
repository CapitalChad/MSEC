using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Configuration;

namespace Msec.Personify.ConfigEditor {
	public partial class MainForm : Form {
		// Constants
		private const String PersonifyElementName = "msec.personify";

		// Fields
		private Lazy<String> _imServiceUrl = new Lazy<String>(() => ConfigurationManager.AppSettings["IMServiceUrl"] ?? "http://dev-my.msec.org/IMS/IMService.asmx");
		private Lazy<String> _imVendorPassword = new Lazy<String>(() => ConfigurationManager.AppSettings["IMVendorPassword"] ?? "10BB61615AF73164F1F9B9AC9655439C");
		private Lazy<String> _imVendorUserName = new Lazy<String>(() => ConfigurationManager.AppSettings["IMVendorUserName"] ?? "TIMSS");
		private Lazy<String> _membershipProviderName = new Lazy<String>(() => ConfigurationManager.AppSettings["MembershipProviderName"] ?? "MsecPersonifyMembershipProvider");
		private Lazy<String> _personifyAssemblyName = new Lazy<String>(() => ConfigurationManager.AppSettings["PersonifyAssemblyName"] ?? "Msec.Personify, Version=1.0.0.0, Culture=neutral, PublicKeyToken=fc79a8b777a0b65c");
		private Lazy<String> _roleManagerProviderName = new Lazy<String>(() => ConfigurationManager.AppSettings["RoleManagerProviderName"] ?? "MsecPersonifyRoleProvider");
		private Lazy<String> _ssoLoginPageUrl = new Lazy<String>(() => ConfigurationManager.AppSettings["SsoLoginPageUrl"] ?? "http://dev-my.msec.org/SSO/login.aspx");
		private Lazy<String> _ssoServiceUrl = new Lazy<String>(() => ConfigurationManager.AppSettings["SsoServiceUrl"] ?? "http://dev-my.msec.org/SSO/webservice/service.asmx");
		private Lazy<String> _ssoVendorBlock = new Lazy<String>(() => ConfigurationManager.AppSettings["SsoVendorBlock"] ?? "3E918C58FB082D1B168F0D2B38830F38");
		private Lazy<String> _ssoVendorIdentifier = new Lazy<String>(() => ConfigurationManager.AppSettings["SsoVendorIdentifier"] ?? "7");
		private Lazy<String> _ssoVendorPassword = new Lazy<String>(() => ConfigurationManager.AppSettings["SsoVendorPassword"] ?? "10BB61615AF73164F1F9B9AC9655439C");
		private Lazy<String> _ssoVendorUserName = new Lazy<String>(() => ConfigurationManager.AppSettings["SsoVendorUserName"] ?? "TIMSS");
		private Lazy<String> _universalPassword = new Lazy<String>(() => ConfigurationManager.AppSettings["UniversalPassword"] ?? "webuser2013");
		private Lazy<String> _universalServiceUrl = new Lazy<String>(() => ConfigurationManager.AppSettings["UniversalServiceUrl"] ?? "http://dev-my.msec.org/DataServices/PersonifyDataMSEC.svc");
		private Lazy<String> _universalUserName = new Lazy<String>(() => ConfigurationManager.AppSettings["UniversalUserName"] ?? "webuser");

		// Constructors
		public MainForm()
			: base() {
			this.InitializeComponent();
		}

		// Methods
		private void BrowseForCentralAdminFilePath() {
			this.BrowseForFilePath(this._centralAdminOpenFileDialog, this._centralAdminFilePathTextBox);
		}
		private void BrowseForFilePath(OpenFileDialog openFileDialog, TextBox filePathTextBox) {
			String initialDirectory = null;
			try {
				initialDirectory = Path.GetDirectoryName(filePathTextBox.Text.Trim());
			}
			catch { }

			if (initialDirectory != null)
				openFileDialog.InitialDirectory = initialDirectory;

			if (openFileDialog.ShowDialog(this) == DialogResult.OK)
				filePathTextBox.Text = openFileDialog.FileName;
		}
		private void BrowseForSiteAdminFilePath() {
			this.BrowseForFilePath(this._siteOpenFileDialog, this._siteFilePathTextBox);
		}
		private void BrowseForWebServicesRootFilePath() {
			this.BrowseForFilePath(this._webServicesRootOpenFileDialog, this._webServicesRootFilePathTextBox);
		}
		private void RefreshUpdateButton() {
			try {
				this._updateButton.Enabled = File.Exists(this._siteFilePathTextBox.Text.Trim())
					&& File.Exists(this._centralAdminFilePathTextBox.Text.Trim())
					&& File.Exists(this._webServicesRootFilePathTextBox.Text.Trim());
			}
			catch {
				this._updateButton.Enabled = false;
			}
		}
		private void UpdateConfigurationFiles() {
			this._siteFilePathTextBox.Enabled =
				this._siteFilePathButton.Enabled =
				this._centralAdminFilePathTextBox.Enabled =
				this._centralAdminFilePathButton.Enabled =
				this._webServicesRootFilePathTextBox.Enabled =
				this._webServicesRootFilePathButton.Enabled =
				this._updateButton.Enabled =
				false;
			this._outputTextBox.Text = String.Empty;

			String[] configurationFilePaths = new String[] {
				this._siteFilePathTextBox.Text.Trim(),
				this._centralAdminFilePathTextBox.Text.Trim(),
				this._webServicesRootFilePathTextBox.Text.Trim()
			};
			this._mainBackgroundWorker.RunWorkerAsync(configurationFilePaths);
		}
		private Boolean EnsureConfigSectionsUpdated(XDocument document) {
			IDictionary<String, String> sectionTypesByName = new Dictionary<String, String> {
				{ "personifySso", "Msec.Personify.Configuration.PersonifySsoConfigurationSection" },
				{ "personifyUniversal", "Msec.Personify.Configuration.PersonifyUniversalConfigurationSection" },
				{ "personifyIM", "Msec.Personify.Configuration.PersonifyIMConfigurationSection" } };

			XElement configSections = document.Root.Element("configSections");
			if (configSections == null)
				throw new InvalidOperationException("No element named 'configSections' exists in the configuration file.  Ensure the file path points to a valid web.config file.");

			XElement sectionGroupElement = configSections
				.Elements("sectionGroup")
				.Where(element => element.Attribute("name").Value == PersonifyElementName)
				.SingleOrDefault();
			if (sectionGroupElement == null) {
				configSections.Add(
					new XElement("sectionGroup",
						new XAttribute("name", PersonifyElementName),
						sectionTypesByName
							.Select(pair => new XElement("section",
								new XAttribute("name", pair.Key),
								new XAttribute("type", pair.Value + ", " + this._personifyAssemblyName.Value)))
							.ToArray()));
				this._mainBackgroundWorker.ReportProgress(1, "\tAdded the configuration/configSections/sectionGroup[@name='" + PersonifyElementName + "'] element.");
				return true;
			}

			Boolean isDirty = false;
			{
				XElement[] elementsToRemove = sectionGroupElement.Elements()
					.Where(element => element.Name.LocalName != "section")
					.ToArray();
				foreach (var element in elementsToRemove) {
					element.Remove();
					this._mainBackgroundWorker.ReportProgress(1, "\tRemoved the configuration/configSections/sectionGroup[@name='" + PersonifyElementName + "']/" + element.Name.LocalName + "[@name='" + element.Attribute("name").Value + "']  element.");
				}
			}

			IDictionary<String, XElement> sectionElementsByName = sectionGroupElement.Elements()
				.ToDictionary(element => element.Attribute("name").Value, element => element);
			foreach (var pair in sectionTypesByName) {
				String name = pair.Key;
				String type = pair.Value + ", " + this._personifyAssemblyName.Value;

				if (!sectionElementsByName.ContainsKey(name)) {
					sectionGroupElement.Add(
						new XElement("section",
							new XAttribute("name", name),
							new XAttribute("type", type)));
					this._mainBackgroundWorker.ReportProgress(1, "\tAdded the section[@name='" + name + "'] element to the configuration/configSections/sectionGroup[@name='" + PersonifyElementName + "'] element.");
					isDirty = true;
				}
				else {
					if (sectionElementsByName[name].Attribute("type").Value != type) {
						sectionElementsByName[name].Attribute("type").Value = type;
						this._mainBackgroundWorker.ReportProgress(1, "\tUpdated the type for the configuration/configSections/sectionGroup[@name='" + PersonifyElementName + "']/section[@name='" + name + "'] element");
						isDirty = true;
					}
					sectionElementsByName.Remove(name);
				}
			}

			if (sectionElementsByName.Count > 0) {
				foreach (var element in sectionElementsByName.Values) {
					element.Remove();
					this._mainBackgroundWorker.ReportProgress(1, "\tRemoved the configuration/configSections/sectionGroup[@name='" + PersonifyElementName + "']/section[@name='" + element.Attribute("name").Value + "']  element.");
				}
				isDirty = true;
			}

			return isDirty;
		}
		private Boolean EnsureMembershipAndRoleManagerUpdated(XDocument document) {
			Boolean isDirty = false;
			{
				XElement membershipSection = document.Root
					.Element("system.web")
					.Element("membership");
				if (membershipSection == null)
					throw new InvalidOperationException("No element named 'membership' exists in the 'system.web' element.  Ensure the file path points to a valid web.config file.");

				String name = this._membershipProviderName.Value;
				String type = "Msec.Personify.Web.PersonifyMembershipProvider, " + this._personifyAssemblyName.Value;
				XElement existingProviderElement = membershipSection.Element("providers")
					.Elements("add")
					.Where(element => element.Attribute("name").Value == name)
					.SingleOrDefault();
				if (existingProviderElement == null) {
					membershipSection.Element("providers").Add(
						new XElement("add",
							new XAttribute("name", name),
							new XAttribute("type", type)));
					this._mainBackgroundWorker.ReportProgress(1, "\tAdded the '" + name + "' membership provider.");
					isDirty = true;
				}
				else if (existingProviderElement.Attribute("type").Value != type) {
					existingProviderElement.Attribute("type").Value = type;
					this._mainBackgroundWorker.ReportProgress(1, "\tUpdated the type for the '" + name + "' membership provider.");
					isDirty = true;
				}
			}

			{
				XElement roleManagerSection = document.Root
					.Element("system.web")
					.Element("roleManager");
				if (roleManagerSection == null)
					throw new InvalidOperationException("No element named 'roleManager' exists in the 'system.web' element.  Ensure the file path points to a valid web.config file.");

				if (!roleManagerSection.Attributes().Any(attribute => attribute.Name == "enabled")) {
					roleManagerSection.Add(new XAttribute("enabled", "true"));
					this._mainBackgroundWorker.ReportProgress(1, "\tEnabled the RoleManager.");
					isDirty = true;
				}
				else if (roleManagerSection.Attribute("enabled").Value != "true") {
					roleManagerSection.Attribute("enabled").Value = "true";
					this._mainBackgroundWorker.ReportProgress(1, "\tEnabled the RoleManager.");
					isDirty = true;
				}

				String name = this._roleManagerProviderName.Value;
				String type = "Msec.Personify.Web.PersonifyRoleProvider, " + this._personifyAssemblyName.Value;
				XElement existingProviderElement = roleManagerSection.Element("providers")
					.Elements("add")
					.Where(element => element.Attribute("name").Value == name)
					.SingleOrDefault();
				if (existingProviderElement == null) {
					roleManagerSection.Element("providers").Add(
						new XElement("add",
							new XAttribute("name", name),
							new XAttribute("type", type)));
					this._mainBackgroundWorker.ReportProgress(1, "\tAdded the '" + name + "' role provider.");
					isDirty = true;
				}
				else if (existingProviderElement.Attribute("type").Value != type) {
					existingProviderElement.Attribute("type").Value = type;
					this._mainBackgroundWorker.ReportProgress(1, "\tUpdated the type for the '" + name + "' role provider.");
					isDirty = true;
				}
			}

			return isDirty;
		}
		private Boolean EnsureHttpModulesUpdated(XDocument document) {
			Boolean isDirty = false;

			{
				XElement httpModulesSection = document.Root
					.Element("system.web")
					.Element("httpModules");
				if (httpModulesSection == null)
					throw new InvalidOperationException("No element named 'httpModules' exists in the 'system.web' element.  Ensure the file path points to a valid web.config file.");

				const String Name = "MsecPersonifySSO";
				String type = "Msec.Personify.Web.PersonifySsoModule, " + this._personifyAssemblyName.Value;
				XElement existingModuleElement = httpModulesSection
					.Elements("add")
					.Where(element => element.Attribute("name").Value == Name)
					.SingleOrDefault();
				if (existingModuleElement == null) {
					httpModulesSection.Add(
						new XElement("add",
							new XAttribute("name", Name),
							new XAttribute("type", type)));
					this._mainBackgroundWorker.ReportProgress(1, "\tAdded the '" + Name + "' HTTP module.");
					isDirty = true;
				}
				else if (existingModuleElement.Attribute("type").Value != type) {
					existingModuleElement.Attribute("type").Value = type;
					this._mainBackgroundWorker.ReportProgress(1, "\tUpdated the type for the '" + Name + "' HTTP module.");
					isDirty = true;
				}
			}

			{
				XElement modulesSection = document.Root
					.Element("system.webServer")
					.Element("modules");
				if (modulesSection == null)
					throw new InvalidOperationException("No element named 'modules' exists in the 'system.webServer' element.  Ensure the file path points to a valid web.config file.");

				const String Name = "MsecPersonifySSO";
				String type = "Msec.Personify.Web.PersonifySsoModule, " + this._personifyAssemblyName.Value;
				XElement existingModuleElement = modulesSection
					.Elements("add")
					.Where(element => element.Attribute("name").Value == Name)
					.SingleOrDefault();
				if (existingModuleElement == null) {
					modulesSection.Add(
						new XElement("add",
							new XAttribute("name", Name),
							new XAttribute("type", type)));
					this._mainBackgroundWorker.ReportProgress(1, "\tAdded the '" + Name + "' IIS module.");
					isDirty = true;
				}
				else if (existingModuleElement.Attribute("type").Value != type) {
					existingModuleElement.Attribute("type").Value = type;
					this._mainBackgroundWorker.ReportProgress(1, "\tUpdated the type for the '" + Name + "' IIS module.");
					isDirty = true;
				}
			}

			return isDirty;
		}
		private Boolean EnsurePersonifyConfigurationUpdated(XDocument document) {
			IDictionary<String, String> ssoAttributes = new Dictionary<String, String> {
				{ "serviceUrl", this._ssoServiceUrl.Value },
				{ "loginPageUrl", this._ssoLoginPageUrl.Value },
				{ "vendorIdentifier", this._ssoVendorIdentifier.Value },
				{ "vendorUserName", this._ssoVendorUserName.Value },
				{ "vendorPassword", this._ssoVendorPassword.Value },
				{ "vendorBlock", this._ssoVendorBlock.Value } };
			IDictionary<String, String> universalAttributes = new Dictionary<String, String> {
				{ "serviceUrl", this._universalServiceUrl.Value },
				{ "userName", this._universalUserName.Value },
				{ "password", this._universalPassword.Value } };
			IDictionary<String, String> imAttributes = new Dictionary<String, String> {
				{ "serviceUrl", this._imServiceUrl.Value },
				{ "vendorUserName", this._imVendorUserName.Value },
				{ "vendorPassword", this._imVendorPassword.Value } };

			XElement existingPersonifyElement = document.Root.Element(PersonifyElementName);
			if (existingPersonifyElement == null) {
				document.Root
					.Add(new XElement(PersonifyElementName,
						new XElement("personifySso",
							ssoAttributes
								.Select(pair => new XAttribute(pair.Key, pair.Value))
								.ToArray()),
						new XElement("personifyUniversal",
							universalAttributes
								.Select(pair => new XAttribute(pair.Key, pair.Value))
								.ToArray()),
						new XElement("personifyIM",
							imAttributes
								.Select(pair => new XAttribute(pair.Key, pair.Value))
								.ToArray())));
				this._mainBackgroundWorker.ReportProgress(1, "\tAdded the configuration/" + PersonifyElementName + " element.");
				return true;
			}

			Boolean isDirty = false;

			{
				XElement personifySso = existingPersonifyElement.Element("personifySso");
				if (personifySso == null) {
					existingPersonifyElement.Add(
						new XElement("personifySso",
							ssoAttributes
									.Select(pair => new XAttribute(pair.Key, pair.Value))
									.ToArray()));
					this._mainBackgroundWorker.ReportProgress(1, "\tAdded the configuration/" + PersonifyElementName + "/personifySso element.");
					isDirty = true;
				}
				else {
					foreach (var pair in ssoAttributes) {
						if (personifySso.Attribute(pair.Key).Value != pair.Value) {
							personifySso.Attribute(pair.Key).Value = pair.Value;
							this._mainBackgroundWorker.ReportProgress(1, "\tUpdated the value for the configuration/" + PersonifyElementName + "/personifySso[@" + pair.Key + "] attribute.");
							isDirty = true;
						}
					}
				}
			}

			{
				XElement personifyUniversal = existingPersonifyElement.Element("personifyUniversal");
				if (personifyUniversal == null) {
					existingPersonifyElement.Add(
						new XElement("personifyUniversal",
							universalAttributes
									.Select(pair => new XAttribute(pair.Key, pair.Value))
									.ToArray()));
					this._mainBackgroundWorker.ReportProgress(1, "\tAdded the configuration/" + PersonifyElementName + "/personifyUniversal element.");
					isDirty = true;
				}
				else {
					foreach (var pair in universalAttributes) {
						if (personifyUniversal.Attribute(pair.Key).Value != pair.Value) {
							personifyUniversal.Attribute(pair.Key).Value = pair.Value;
							this._mainBackgroundWorker.ReportProgress(1, "\tUpdated the value for the configuration/" + PersonifyElementName + "/personifyUniversal[@" + pair.Key + "] attribute.");
							isDirty = true;
						}
					}
				}
			}

			{
				XElement personifyIM = existingPersonifyElement.Element("personifyIM");
				if (personifyIM == null) {
					existingPersonifyElement.Add(
						new XElement("personifyIM",
							universalAttributes
									.Select(pair => new XAttribute(pair.Key, pair.Value))
									.ToArray()));
					this._mainBackgroundWorker.ReportProgress(1, "\tAdded the configuration/" + PersonifyElementName + "/personifyIM element.");
					isDirty = true;
				}
				else {
					foreach (var pair in imAttributes) {
						if (personifyIM.Attribute(pair.Key).Value != pair.Value) {
							personifyIM.Attribute(pair.Key).Value = pair.Value;
							this._mainBackgroundWorker.ReportProgress(1, "\tUpdated the value for the configuration/" + PersonifyElementName + "/personifyIM[@" + pair.Key + "] attribute.");
							isDirty = true;
						}
					}
				}
			}

			return isDirty;
		}

		// Event Handler Methods
		private void _centralAdminFilePathButton_Click(Object sender, EventArgs e) {
			this.BrowseForCentralAdminFilePath();
		}
		private void _centralAdminFilePathTextBox_TextChanged(Object sender, EventArgs e) {
			this.RefreshUpdateButton();
		}
		private void _fileExitMenuItem_Click(Object sender, EventArgs e) {
			this.Close();
		}
		private void _mainBackgroundWorker_DoWork(Object sender, DoWorkEventArgs e) {
			var configurationFilePaths = new {
				Site = ((String[])e.Argument)[0],
				CentralAdmin = ((String[])e.Argument)[1],
				WebServicesRoot = ((String[])e.Argument)[2]
			};

			this._mainBackgroundWorker.ReportProgress(1, "Updating the site's configuration file...");
			{
				XDocument siteWebConfig = XDocument.Load(configurationFilePaths.Site);
				this._mainBackgroundWorker.ReportProgress(1, "\tFile loaded into memory.");

				Boolean isDirty = this.EnsureConfigSectionsUpdated(siteWebConfig)
					| this.EnsureMembershipAndRoleManagerUpdated(siteWebConfig)
					| this.EnsureHttpModulesUpdated(siteWebConfig)
					| this.EnsurePersonifyConfigurationUpdated(siteWebConfig);

				if (isDirty) {
					siteWebConfig.Save(configurationFilePaths.Site);
					this._mainBackgroundWorker.ReportProgress(1, "\tAll changes have been saved." + Environment.NewLine);
				}
				else {
					this._mainBackgroundWorker.ReportProgress(1, "\tNo changes are needed." + Environment.NewLine);
				}
			}

			this._mainBackgroundWorker.ReportProgress(1, "Updating central admin's configuration file...");
			{
				XDocument centralAdminWebConfig = XDocument.Load(configurationFilePaths.CentralAdmin);
				this._mainBackgroundWorker.ReportProgress(1, "\tFile loaded into memory.");

				Boolean isDirty = this.EnsureConfigSectionsUpdated(centralAdminWebConfig)
					| this.EnsureMembershipAndRoleManagerUpdated(centralAdminWebConfig)
					| this.EnsurePersonifyConfigurationUpdated(centralAdminWebConfig);

				if (isDirty) {
					centralAdminWebConfig.Save(configurationFilePaths.CentralAdmin);
					this._mainBackgroundWorker.ReportProgress(1, "\tAll changes have been saved." + Environment.NewLine);
				}
				else {
					this._mainBackgroundWorker.ReportProgress(1, "\tNo changes are needed." + Environment.NewLine);
				}
			}

			this._mainBackgroundWorker.ReportProgress(1, "Updating the web service's root configuration file...");
			{
				XDocument webServicesRootWebConfig = XDocument.Load(configurationFilePaths.WebServicesRoot);
				this._mainBackgroundWorker.ReportProgress(1, "\tFile loaded into memory.");

				Boolean isDirty = this.EnsureConfigSectionsUpdated(webServicesRootWebConfig)
					| this.EnsureMembershipAndRoleManagerUpdated(webServicesRootWebConfig)
					| this.EnsurePersonifyConfigurationUpdated(webServicesRootWebConfig);

				if (isDirty) {
					webServicesRootWebConfig.Save(configurationFilePaths.WebServicesRoot);
					this._mainBackgroundWorker.ReportProgress(1, "\tAll changes have been saved." + Environment.NewLine);
				}
				else {
					this._mainBackgroundWorker.ReportProgress(1, "\tNo changes are needed." + Environment.NewLine);
				}
			}
		}
		private void _mainBackgroundWorker_ProgressChanged(Object sender, ProgressChangedEventArgs e) {
			this._outputTextBox.AppendText((String)e.UserState + Environment.NewLine);
		}
		private void _mainBackgroundWorker_RunWorkerCompleted(Object sender, RunWorkerCompletedEventArgs e) {
			this._outputTextBox.AppendText(Environment.NewLine);
			if (e.Error != null) {
				this._outputTextBox.AppendText(e.Error.ToString() + Environment.NewLine);
			}
			else if (e.Cancelled) {
				this._outputTextBox.AppendText("Operation cancelled." + Environment.NewLine);
			}
			else {
				this._outputTextBox.AppendText("Completed successfully." + Environment.NewLine);
			}
		}
		private void _siteFilePathButton_Click(Object sender, EventArgs e) {
			this.BrowseForSiteAdminFilePath();
		}
		private void _siteFilePathTextBox_TextChanged(Object sender, EventArgs e) {
			this.RefreshUpdateButton();
		}
		private void _updateButton_Click(Object sender, EventArgs e) {
			this.UpdateConfigurationFiles();
		}
		private void _webServicesRootFilePathButton_Click(Object sender, EventArgs e) {
			this.BrowseForWebServicesRootFilePath();
		}
		private void _webServicesRootFilePathTextBox_TextChanged(Object sender, EventArgs e) {
			this.RefreshUpdateButton();
		}
	}
}
