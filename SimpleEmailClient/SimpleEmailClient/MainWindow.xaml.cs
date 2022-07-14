using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleEmailClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            String Server = txtServer.Text;
            int Port = 0;
            Int32.TryParse(txtPort.Text, out Port);
            String UserName = txtUserName.Text;
            String Password = txtpassword.Password;

            Boolean SSL = Convert.ToBoolean(chkssl.IsChecked);

            Boolean TLS = Convert.ToBoolean(chkTLS.IsChecked);

            String ToEmail = txtToEmail.Text.Trim();
            String FromEmail = txtFrom.Text.Trim();
            String Body = txtBody.Text.Trim();
            String Subject = txtSubject.Text.Trim();

            try
            {

                if (TLS)
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                MailMessage msg = new MailMessage();
                msg.To.Add(new MailAddress(ToEmail));
                msg.From = new MailAddress(FromEmail);
                msg.Subject = Subject;
                msg.Body = Body;
                msg.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                client.Host = Server;
                client.Credentials = new System.Net.NetworkCredential(UserName, Password);
                client.Port = Port;
                if (SSL)
                    client.EnableSsl = true;
                client.Send(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
