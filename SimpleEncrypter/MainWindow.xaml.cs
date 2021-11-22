using System.Windows;
using Zadatak.Utils;

namespace SimpleEncrypter
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

        private void BtnEncrypt_Click(object sender, RoutedEventArgs e) =>TbEncrypted.Text = EncryptionUtils.Encrypt(TbPlain.Text, "fru1tc@k3");
        
    }
}
