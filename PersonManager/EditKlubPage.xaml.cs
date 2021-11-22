using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Zadatak.Models;
using Zadatak.ViewModels;

namespace Zadatak
{
    /// <summary>
    /// Interaction logic for EditKlubPage.xaml
    /// </summary>
    public partial class EditKlubPage : FramedPage<KlubViewModel>
    {
        private readonly Klub klub;
        public EditKlubPage(KlubViewModel klubViewModel, Klub klub = null) : base(klubViewModel)
        {
            InitializeComponent();
            this.klub = klub ?? new Klub();
            DataContext = klub;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {

                klub.Name = TbName.Text.Trim();

                if (klub.IDKlub == 0)
                {
                    ViewModel.Klubs.Add(klub);
                }
                else
                {
                    ViewModel.Update(klub);
                }
                Frame.NavigationService.GoBack();
            }
        }

        private bool FormValid()
        {
            bool valid = true;
            GridContainter.Children.OfType<TextBox>().ToList().ForEach(e =>
            {
                if (string.IsNullOrEmpty(e.Text.Trim()))
                {
                    e.Background = Brushes.LightCoral;
                    valid = false;
                }
                else
                {
                    e.Background = Brushes.White;
                }
            });

            return valid;
        }


    }
}
