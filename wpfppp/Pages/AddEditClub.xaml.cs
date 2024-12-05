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
    /// Логика взаимодействия для AddEditClub.xaml
    /// </summary>
    public partial class AddEditClub : Page
    {
        private Clubs thisclub = new Clubs();
        public AddEditClub(Clubs selectedclub)
        {
            InitializeComponent();
            if (selectedClub != null)
            {
                thisclub = selectedClub;
            }
            DirectorCombo.ItemsSource = Entities3.GetContext().Directors.ToList();
            ClubTypeCombo.ItemsSource = Entities3.GetContext().ClubTypes.ToList();

            DataContext = thisclub;
        }
        private void AddClub()
        {
            try
            {
                thisclub = new Clubs()
                {
                    ClubName = ClubNameTb.Text,
                    ClubTypeID = ClubTypeCombo.SelectedIndex + 1,
                    DirectorID = DirectorCombo.SelectedIndex + 1,
                    Description = lubDescriptionTb.Text,
                    Location = LocationTb.Text,
                    ClubPhoto = null,
                };
                AppConnect.clubmodel.Clubs.Add(thisclub);
                AppConnect.clubmodel.SaveChanges();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void UpdateClub()
        {
            if (thisclub.ClubID > 0)
            {
                try
                {
                    thisclub.ClubName = ClubNameTb.Text;
                    thisclub.DirectorID = DirectorCombo.SelectedIndex + 1;
                    thisclub.ClubTypeID = ClubTypeCombo.SelectedIndex + 1;
                    thisclub.Description = lubDescriptionTb.Text;
                    thisclub.Location = LocationTb.Text;
                    thisclub.ClubPhoto = thisclub.ClubPhoto;
                    AppConnect.clubmodel.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (thisclub.ClubID == 0)
            {
                AddClub();
                AppFrame.MainFrame.Navigate(new AdminClubList());

            }
            else
            {
                UpdateClub();
                AppFrame.MainFrame.Navigate(new AdminClubList());
            }
        }
    }
}
