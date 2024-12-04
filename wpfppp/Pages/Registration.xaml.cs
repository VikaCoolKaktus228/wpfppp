using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using wpfppp.AppData;

namespace wpfppp.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
            logintb.MaxLength = 50;
            passtb.MaxLength = 50;
            repeatpasstb.MaxLength = 50;
            nametb.MaxLength = 50;
            emailtb.MaxLength = 100;
        }

        private void regbtn_Click(object sender, object e)
        {
            if (string.IsNullOrWhiteSpace(logintb.Text) || string.IsNullOrEmpty(passtb.Password) || string.IsNullOrEmpty(repeatpasstb.Password) || string.IsNullOrWhiteSpace(nametb.Text) || string.IsNullOrWhiteSpace(emailtb.Text))
            {
                MessageBox.Show("заполните все поля!!");
            }
            else
            {
                try
                {
                    Users user = new Users()
                    {
                        IdUser = 1,
                        NameUser = nametb.Text,
                        Login = logintb.Text,
                        Password = passtb.Password,
                        Email = emailtb.Text,
                        IdRole = 2
                    };
                    AppConnect.clubmodel.Users.Add(user);
                    AppConnect.clubmodel.SaveChanges();
                    MessageBox.Show("вы успешно зарегистрировались");
                    AppFrame.MainFrame.Navigate(new Authorization());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ошибка данных");
                }
            }
        }

        private void repeatpasstb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (passtb.Password != repeatpasstb.Password)
            {
                regbtn.IsEnabled = false;
            }
            else if (passtb.Password == repeatpasstb.Password)
            {
                regbtn.IsEnabled = true;
            }
        }

        private void nametb_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.A || e.Key > Key.Z) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
        }
    }
}
