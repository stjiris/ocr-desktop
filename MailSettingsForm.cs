using System.Configuration;

namespace Tesseract_UI_Tools
{
    public partial class MailSettingsForm : Form
    {
        readonly EmailUIParameters EmailParams;
        public MailSettingsForm(EmailUIParameters parameters)
        {
            EmailParams = parameters;
            InitializeComponent();
            emailUIParametersBindingSource.DataSource = EmailParams;
            GmailProvider.LoginStatusUpdate += SetGoogleString;
        }

        private void MailSettingsForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            EmailParams.Save();
        }

        private void SaveSettings(object sender, EventArgs e)
        {
            EmailParams.Save();
        }

        private void GoogleBtn_Click(object sender, EventArgs e)
        {
            if( GmailProvider.IsReady())
            {
                GmailProvider.LogOut();

            }
            else
            {
                GmailProvider.Initialize();
            }
        }

        private void SetGoogleString() {
            if (GmailProvider.IsReady())
            {
                GoogleBtn.Text = "Google Logout";
            }
            else
            {
                GoogleBtn.Text = "Google Login";
            }
        }
    }

    public class EmailUIParameters : ApplicationSettingsBase
    {
        [UserScopedSettingAttribute()]
        [DefaultSettingValue("")]
        public string EmailTo
        {
            get { return (string)this[nameof(EmailTo)]; }
            set { this[nameof(EmailTo)] = value; }
        }


        [UserScopedSettingAttribute()]
        [DefaultSettingValue("")]
        public string EmailFrom
        {
            get { return (string)this[nameof(EmailFrom)]; }
            set { this[nameof(EmailFrom)] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValue("")]
        public string Host
        {
            get { return (string)this[nameof(Host)]; }
            set { this[nameof(Host)] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValue("21")]
        public int Port
        {
            get { return (int)this[nameof(Port)]; }
            set { this[nameof(Port)] = value; }
        }
    }
}
