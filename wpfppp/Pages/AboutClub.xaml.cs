using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для AboutClub.xaml
    /// </summary>
    public partial class AboutClub : Page
    {
        private Clubs thisclub;
        int idmemberclub = Convert.ToInt32(App.Current.Properties["IdUser"].ToString());
        public AboutClub(Clubs selectedclub)
        {
            InitializeComponent();
            if (selectedclub != null)
            {
                thisclub = selectedclub;
            }

            var clubobj = Entities3.GetContext().EventsInClub
                               .Where(e => thisclub.ClubID.Contains(e.IdClub))
                               .Select(x => x.IdEvents)
                               .ToList();
            var EventsInClub = Entities3.GetContext().Events
                                         .Where(x => clubobj.Contains(x.Id))
                                         .ToList();
            ListEvents.ItemsSource = EventsInClub;
            ClubName.Content = thisclub.ClubName;
            ClubDirector.Content = Entities3.GetContext().Directors
                               .FirstOrDefault(d => d.DirectorID == thisclub.DirectorID);
            ClubDescription.Content = thisclub.Description;
            ClubAdress.Content = thisclub.Location;

        }

        private void JoinClub_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Memberships newmember = new Memberships()
                {
                    MemberID = idmemberclub,
                    ClubID = thisclub.ClubID,
                    JoinDate = DateTime.Today
                };

                AppConnect.clubmodel.Memberships.Add(newmember);
                AppConnect.clubmodel.SaveChanges();

                MessageBox.Show("Вы успешно вступили в клуб!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.MainFrame.Navigate(new OrdClubList());
            }
            catch
            {
                MessageBox.Show("Ошибка!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
