using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FP_CLOCK
{
    public partial class LoginCheck : Form
    {
        private FirestoreService _firestoreService;
        private string credentialsPath;

        public LoginCheck()
        {
            InitializeComponent();
            
            credentialsPath = @"C:\EnGoPer\Data\fp-clock-firebase-adminsdk-ebft1-4f832f8f70";
            _firestoreService = new FirestoreService(credentialsPath);
        }

        private void LoginCheck_Load(object sender, EventArgs e)
        {
            textBox1.PasswordChar = '*'; // Şifre girişini gizli yap
            textBox1.Focus();
        }

        private async void pw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await loginClickAsync();
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            await loginClickAsync();
        }

        // Kullanıcı adı ve şifre doğrulama işlemi
        private async Task loginClickAsync()
        {
            string password = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Lütfen şifre giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Firestore ile kimlik doğrulama
            bool loginSuccessful = await _firestoreService.LoginUser(password);

            if (loginSuccessful)
            {
                this.DialogResult = DialogResult.OK; // Başarılı giriş
                this.Close();
            }
            else
            {
                MessageBox.Show("Giriş başarısız! Şifre hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                textBox1.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


    }
}
