using System.Windows;
using System.Windows.Controls;
using Zadatak.Models;
using Zadatak.ViewModels;

namespace Zadatak
{
    /// <summary>
    /// Interaction logic for ListPersonsPage.xaml
    /// </summary>
    public partial class ListPeoplePage : FramedPage<PersonViewModel>
    {

        public KlubViewModel KlubViewModel { get; }

        public ListPeoplePage(PersonViewModel personViewModel, KlubViewModel klubViewModel) : base(personViewModel)
        {
            InitializeComponent();
            LvUsers.ItemsSource = personViewModel.People;

            DataContext = personViewModel;

            KlubViewModel = klubViewModel;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new EditPersonPage(ViewModel, KlubViewModel) { Frame = Frame });

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (LvUsers.SelectedItem != null)
            {
                Frame.Navigate(new EditPersonPage(ViewModel, KlubViewModel, LvUsers.SelectedItem as Person) { Frame = Frame });

            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LvUsers.SelectedItem != null)
            {
                ViewModel.People.Remove(LvUsers.SelectedItem as Person);
            }
        }

        private void BtnKlubovi_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new ListKlubsPage(KlubViewModel, ViewModel) { Frame = Frame });
        }
    }
}
