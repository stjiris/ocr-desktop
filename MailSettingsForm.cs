using System.Configuration;

namespace Tesseract_UI_Tools
{
    public partial class MailSettingsForm : Form
    {
        EmailUIParameters EmailParams;
        public MailSettingsForm(EmailUIParameters parameters)
        {
            EmailParams = parameters;
            InitializeComponent();
            emailUIParametersBindingSource.DataSource = EmailParams;
        }

        private void MailSettingsForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            EmailParams.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EmailParams.Save();
        }
    }

    public class EmailUIParameters : ApplicationSettingsBase
    {
        [UserScopedSettingAttribute()]
        [DefaultSettingValue("")]
        public string EmailTo
        {
            get { return (string)this["EmailTo"]; }
            set { this["EmailTo"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValue("")]
        public string Host
        {
            get { return (string)this["Host"]; }
            set { this["Host"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValue("21")]
        public int Port
        {
            get { return (int)this["Port"]; }
            set { this["Port"] = value; }
        }
    }
}
