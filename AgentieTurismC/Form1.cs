using log4net.Config;

namespace AgentieTurismC
{
    public partial class LoginForm : Form
    {

        public LoginForm()
        {

            InitializeComponent(); 
            XmlConfigurator.Configure(new System.IO.FileInfo("F:\\Semestrul 2\\MPP\\AgentieTurismC#\\mpp-proiect-csharp-Aleex-C\\AgentieTurismC\\log4net.config"));

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            Form2 mainPage = new Form2();
            mainPage.Show();
            this.Hide();
        }
    }
}