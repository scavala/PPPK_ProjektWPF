﻿using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Zadatak.Models;
using Zadatak.Utils;
using Zadatak.ViewModels;

namespace Zadatak
{
    /// <summary>
    /// Interaction logic for EditPage.xaml
    /// </summary>
    public partial class EditPersonPage : FramedPage<PersonViewModel>
    {
        private const string Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png";
        private readonly KlubViewModel klubViewModel;
        private readonly Person person;
        public EditPersonPage(PersonViewModel personViewModel, KlubViewModel klubViewModel, Person person = null) : base(personViewModel)
        {
            InitializeComponent();

            this.klubViewModel = klubViewModel;
            this.person = person ?? new Person();
            this.person.OmiljenKlub = this.person.OmiljenKlub ?? new Klub();

            this.klubViewModel.SelectedKlub = this.klubViewModel.Klubs.FirstOrDefault(k => k.IDKlub == this.person.OmiljenKlub.IDKlub);

            DataContext = new
            {
                person = this.person,
                klubovi = klubViewModel
            };

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new ListPeoplePage(ViewModel, klubViewModel) { Frame = Frame });


        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                person.Age = int.Parse(TbAge.Text.Trim());
                person.Email = TbEmail.Text.Trim();
                person.FirstName = TbFirstName.Text.Trim();
                person.LastName = TbLastName.Text.Trim();
                person.Picture = ImageUtils.BitmapImageToByteArray(Picture.Source as BitmapImage);

                person.OmiljenKlub = cbKlub.SelectedItem as Klub;

                if (person.IDPerson == 0)
                {
                    ViewModel.People.Add(person);

                }
                else
                {
                    ViewModel.Update(person);
                }

                Frame.Navigate(new ListPeoplePage(ViewModel, klubViewModel) { Frame = Frame });
            }
        }



        private bool FormValid()
        {
            bool valid = true;
            GridContainter.Children.OfType<TextBox>().ToList().ForEach(e =>
            {
                if (string.IsNullOrEmpty(e.Text.Trim())
                    || ("Int".Equals(e.Tag) && !int.TryParse(e.Text, out int age))
                    || ("Email".Equals(e.Tag) && !ValidationUtils.isValidEmail(TbEmail.Text.Trim())))
                {
                    e.Background = Brushes.LightCoral;
                    valid = false;
                }
                else
                {
                    e.Background = Brushes.White;
                }
            });
            if (Picture.Source == null)
            {
                PictureBorder.BorderBrush = Brushes.LightCoral;
                valid = false;
            }
            else
            {
                PictureBorder.BorderBrush = Brushes.WhiteSmoke;
            }
            return valid;
        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = Filter
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Picture.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
            person.Picture = ImageUtils.BitmapImageToByteArray(Picture.Source as BitmapImage);
        }


        private void onLoaded(object sender, RoutedEventArgs e)
        {
            if (klubViewModel.Klubs.Count == 0)
            {
                MessageBox.Show("Svaka osoba MORA imati omiljeni klub, stoga dodajte koji klub!!");
                Frame.Navigate(new ListKlubsPage(klubViewModel, ViewModel) { Frame = Frame });


            }
        }
    }
}
