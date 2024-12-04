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
    /// Логика взаимодействия для AdminClubList.xaml
    /// </summary>
    public partial class AdminClubList : Page
    {
        public AdminClubList()
        {
            InitializeComponent();
            List<Clubs> clubslist = AppConnect.clubmodel.Clubs.ToList();
            quantity.Content = clubslist.Count + " из " + clubslist.Count;
            ListClubs.ItemsSource = clubslist;

            ComboSort.Items.Add("от A-Я");
            ComboSort.Items.Add("от Я-А");
            ComboFilter.Items.Add("все типы");
            var filter = Entities1.GetContext().ClubTypes.Select(x => x.TypeName).ToList();
            filter.Insert(0, "все типы");
            ComboFilter.Items.Clear();
            ComboFilter.ItemsSource = filter;
        }
        Clubs[] FindClub()
        {
            List<Clubs> club = AppConnect.clubmodel.Clubs.ToList();
            var Allclubs = club;
            if (SearchTb != null)
            {
                club = club.Where(x => x.ClubName.ToLower().Contains(SearchTb.Text.ToLower())).ToList();
                quantity.Content = club.Count + " из " + Allclubs.Count;
            }

            if (ComboFilter.SelectedIndex >= 0)
            {
                switch (ComboFilter.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        club = club.Where(x => x.ClubTypeID == 1).ToList();
                        quantity.Content = club.Count + " из " + Allclubs.Count;
                        break;
                    case 2:
                        club = club.Where(x => x.ClubTypeID == 2).ToList();
                        quantity.Content = club.Count + " из " + Allclubs.Count;
                        break;
                    case 3:
                        club = club.Where(x => x.ClubTypeID == 3).ToList();
                        quantity.Content = club.Count + " из " + Allclubs.Count;
                        break;
                    case 4:
                        club = club.Where(x => x.ClubTypeID == 4).ToList();
                        quantity.Content = club.Count + " из " + Allclubs.Count;
                        break;
                }
            }
            if (ComboSort.SelectedIndex >= 0)
            {
                switch (ComboSort.SelectedIndex)
                {
                    case 0:
                        club = club.OrderBy(x => x.ClubName).ToList();
                        quantity.Content = club.Count + " из " + Allclubs.Count;
                        break;
                    case 1:
                        club = club.OrderByDescending(x => x.ClubName).ToList<Clubs>();
                        quantity.Content = club.Count + " из " + Allclubs.Count;
                        break;
                }
            }
            ListClubs.ItemsSource = club;
            return club.ToArray();
        }

            private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindClub();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindClub();
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            FindClub();
        }

        private void ExitBtton_Click(object sender, object e)
        {
            AppFrame.MainFrame.Navigate(new Authorization());
        }
    }
}
