using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFYuxiao.ViewModel;

namespace WPFYuxiao
{
    /// <summary>
    /// Artist.xaml 的交互逻辑
    /// </summary>
    public partial class Artist : Window
    {
        #region variables
        HttpClient client = new HttpClient();
        ArtistViewModel vm = new ArtistViewModel();
        #endregion

        #region method
        public Artist()
        {
            InitializeComponent();

            client.BaseAddress = new Uri("http://localhost:55854");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            this.DataContext = vm;
        }  

        void clear()
        {
            textFirstName.Clear();
            textLastName.Clear();
            textStyle.Clear();
            textComment.Clear();
            textHomeCountry.Clear();
            textMusicalExtract.Clear();
        }

        private bool DataValidation()
        {
            string message = "";
            //FirstName Validation
            if (!vm.FirstNameValidation(textFirstName.Text))
            {
                ToolTip tt = new ToolTip { Content = "Le prénom ne peut que contenir des lettres et ne doit pas dépasser 20 caractères"};
                TextBoxSetError(textFirstName);
                textFirstName.ToolTip = tt;
                message += "Le prénom n'est pas correcte";
            }
            if (!vm.LastNameValidation(textLastName.Text))
            {
                ToolTip tt = new ToolTip { Content = "Le nom ne peut que contenir des lettres et ne doit pas dépasser 20 caractères" };
                TextBoxSetError(textLastName);
                textLastName.ToolTip = tt;
                message += "\nLe nom n'est pas correcte";
            }
            if (!vm.StyleValidation(textStyle.Text))
            {
                ToolTip tt = new ToolTip { Content = "Le style ne peut que contenir des lettres et ne doit pas dépasser 15 caractères" };
                TextBoxSetError(textStyle);
                textStyle.ToolTip = tt;
                message += "\nLe style n'est pas correcte";
            }
            if (!vm.HomeCountryValidation(textHomeCountry.Text))
            {
                ToolTip tt = new ToolTip { Content = "Le pays ne peut que contenir des lettres et ne doit pas dépasser 20 caractères" };
                TextBoxSetError(textHomeCountry);
                textHomeCountry.ToolTip = tt;
                message += "\nLe pays n'est pas correcte";
            }
            if (!vm.CommentValidation(textComment.Text))
            {
                ToolTip tt = new ToolTip { Content = "Le commentaire ne peut que contenir des lettres et ne doit pas dépasser 50 caractères" };
                TextBoxSetError(textComment);
                textComment.ToolTip = tt;
                message += "\nLe commentaire n'est pas correcte";
            }
            if (!vm.MEValidation(textMusicalExtract.Text))
            {
                ToolTip tt = new ToolTip { Content = "L'extrait musical doit être un url valide et ne doit pas dépasser 100 caractères" };
                TextBoxSetError(textMusicalExtract);
                textMusicalExtract.ToolTip = tt;
                message += "\nL'extrait musical n'est pas correcte";
                message += "\neg. https://www.youtube.com/watch?v=UOxkGD8qRB4";
            }
            if (message!="")
            {
                MessageBox.Show(message);
                return false;
            }
            return true;
        }

        public void TextBoxSetError(object sender)
        {
            var s = sender as TextBox;
            s.Background = (Brush)new BrushConverter().ConvertFrom("#DDD");
            s.Foreground = Brushes.Red;
        }
        #endregion

        #region action
        private async void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            btnSubmit.IsEnabled = false;
            if (DataValidation())
            {
                try
                {
                    var response = await client.PostAsJsonAsync("api/Artists", vm);
                    response.EnsureSuccessStatusCode(); // Throw on error code. 
                    clear();
                    MessageBox.Show("Artiste ajouté!");

                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {

                }
            }
            btnSubmit.IsEnabled = true;

        }

        public void TextBoxInitError(object sender, RoutedEventArgs e)
        {
            var s= sender as TextBox;
            s.Background = Brushes.White;
            s.Foreground = Brushes.Black;
        }
        #endregion

    }
}
