using mDitaEditor.Project;
using System;
using System.Collections.Specialized;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace mDitaEditor.CustomForms
{
    public partial class ExceptionForm : Form
    {
        private Exception _exception;
        public Exception Exception
        {
            get
            {
                return _exception;
            }
            set
            {
                _exception = value;
                if (_exception == null)
                {
                    lblExceptionMessage.Text = "";
                    txbStackTrace.Text = "";
                }
                else
                {
                    txbStackTrace.Text = _exception.StackTrace;
                    lblExceptionMessage.Text = _exception.Message;
                }
            }
        }
        
        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                txbExceptionEmail.Text = _email;
            }
        }

        public ExceptionForm(Exception ex = null, string email = "")
        {
            InitializeComponent();
            Exception = ex;
            Email = email;
        }

        private void ExceptionForm_Load(object sender, EventArgs e)
        {
            txbExceptionDescription.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendReport();
        }

        public void SendReport()
        {
            //new Thread(() =>
            //{

            
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["stacktrace"] = txbStackTrace.Text;
                values["naslov"] = txbExceptionName.Text;
                values["email"] = txbExceptionEmail.Text;
                values["opis"] = txbExceptionDescription.Text;

                if (chbSendZip.Checked)
                    {
                        ZipFile.CreateFromDirectory(ProjectSingleton.Project.ProjectDir, "c:\\TEST.ZIP");
                
                    }
                    client.UploadValuesCompleted += Client_UploadValuesCompleted;
                    var response = client.UploadValues("http://mdita.metropolitan.ac.rs/ticket/noviticket.php", values);
                    var responseString = Encoding.Default.GetString(response);
                    

                //try
                //{

                
                
                //}
                //catch
                //{

                //}
            }
            //}).Start();
           
        }

        private void Client_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
        {
            Close();
        }

        private void lblExceptionMessage_Click(object sender, EventArgs e)
        {
            
        }

        
        private void chbSendZip_CheckedChanged(object sender, EventArgs e)
        {
            WebClient myWebClient = new WebClient();

            string uriString = "http://mdita.metropolitan.ac.rs/ticket/noviticket.php";
            string fileName = "";
            byte[] responseArray = myWebClient.UploadFile(uriString, "POST", fileName);
        }
    }
}
