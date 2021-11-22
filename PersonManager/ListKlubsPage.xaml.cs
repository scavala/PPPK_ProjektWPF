using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Zadatak.Models;
using Zadatak.ViewModels;

namespace Zadatak
{
    /// <summary>
    /// Interaction logic for ListKlubsPage.xaml
    /// </summary>
    public partial class ListKlubsPage : FramedPage<KlubViewModel>
    {
        public PersonViewModel PersonViewModel { get; }

        public ListKlubsPage(KlubViewModel klubViewModel, PersonViewModel personViewModel) : base(klubViewModel)
        {
            InitializeComponent();
            LvKlubs.ItemsSource = klubViewModel.Klubs;
            PersonViewModel = personViewModel;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new EditKlubPage(ViewModel) { Frame = Frame });
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (LvKlubs.SelectedItem != null)
            {
                Frame.Navigate(new EditKlubPage(ViewModel, LvKlubs.SelectedItem as Klub) { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LvKlubs.SelectedItem != null)
            {
                PersonViewModel.People.Where(p => p.OmiljenKlub.IDKlub == (LvKlubs.SelectedItem as Klub).IDKlub).ToList().ForEach(o => PersonViewModel.People.Remove(o));
                ViewModel.Klubs.Remove(LvKlubs.SelectedItem as Klub);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new ListPeoplePage(PersonViewModel, ViewModel) { Frame = Frame });


    }
}