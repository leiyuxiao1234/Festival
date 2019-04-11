using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFYuxiao.ViewModel;

namespace WPFYuxiao.View
{
    /// <summary>
    /// Place.xaml 的交互逻辑
    /// </summary>
    public partial class Place : Window
    {
        #region variables
        HttpClient client = new HttpClient();
        PlaceViewModel vm = new PlaceViewModel();
        #endregion

        #region method
        public Place()
        {
            InitializeComponent();

            client.BaseAddress = new Uri("http://localhost:55854");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            this.DataContext = vm;
        }

        void clear()
        {
            textName.Clear();
            textCapacity.Clear();
            textDescription.Clear();
        }

        private bool DataValidation()
        {
            string message = "";
            /*
            if (!vm.NameValidation(textName.Text))
            {
                ToolTip tt = new ToolTip { Content = "Le prénom ne peut que contenir des lettres et ne doit pas dépasser 20 caractères" };
                TextBoxSetError(textFirstName);
                textFirstName.ToolTip = tt;
                message += "Le prénom n'est pas correcte";
            }
            if (!vm.CapacityValidation(textCapacity.Text))
            {
                ToolTip tt = new ToolTip { Content = "Le nom ne peut que contenir des lettres et ne doit pas dépasser 20 caractères" };
                TextBoxSetError(textLastName);
                textLastName.ToolTip = tt;
                message += "\nLe nom n'est pas correcte";
            }
            if (!vm.DescriptionValidation(textDescription.Text))
            {
                ToolTip tt = new ToolTip { Content = "Le style ne peut que contenir des lettres et ne doit pas dépasser 15 caractères" };
                TextBoxSetError(textStyle);
                textStyle.ToolTip = tt;
                message += "\nLe style n'est pas correcte";
            }
            if (message != "")
            {
                MessageBox.Show(message);
                return false;
            }*/
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
            var s = sender as TextBox;
            s.Background = Brushes.White;
            s.Foreground = Brushes.Black;
        }
        #endregion
        
    
    }
}
