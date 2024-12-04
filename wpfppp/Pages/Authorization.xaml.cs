using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            var userobj = AppConnect.clubmodel.Users.FirstOrDefault(x => x.Login == LoginTb.Text && x.Password == PasswordPb.Password);
            if (userobj != null)
            {
                try
                {
                    var password = AppConnect.clubmodel.Users.FirstOrDefault(x => x.Password == PasswordPb.Password);
                    if (password == null)
                    {
                        MessageBox.Show("неверный пароль",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    switch (userobj.IdRole)
                    {
                        case 1:
                            MessageBox.Show("здраствйте администратор " + userobj.Login,
                    "уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.MainFrame.Navigate(new AdminClubList());
                            break;
                        case 2:
                            MessageBox.Show("здраствйте пользователь " + userobj.Login,
                    "уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.MainFrame.Navigate(new OrdClubList());
                            break;
                        case 3:
                            break;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка" + ex.Message.ToString(),
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует");
            }
        }

        private void ToRegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.MainFrame.Navigate(new Registration());
        }
    }
}
