using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using WPFYuxiao.ViewModel;

namespace WPFYuxiao.View
{
    /// <summary>
    /// Artist_List.xaml 的交互逻辑
    /// </summary>
    public partial class ArtistsList : Window
    {
        #region variables
        HttpClient client = new HttpClient();
        ArtistsViewModel vm = new ArtistsViewModel();
        ArtistViewModel vm2 = new ArtistViewModel();
        #endregion

        #region method
        public ArtistsList()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("http://localhost:55854");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            this.Grid1.DataContext = vm;
            this.Grid2.DataContext = vm2;
        }

        private bool DataValidation()
        {
            string message = "";
            //FirstName Validation
            if (!vm2.FirstNameValidation(textFirstName.Text))
            {
                ToolTip tt = new ToolTip { Content = "Le prénom ne peut que contenir des lettres et ne doit pas dépasser 20 caractères" };
                TextBoxSetError(textFirstName);
                textFirstName.ToolTip = tt;
                message += "Le prénom n'est pas correcte";
            }
            if (!vm2.LastNameValidation(textLastName.Text))
            {
                ToolTip tt = new ToolTip { Content = "Le nom ne peut que contenir des lettres et ne doit pas dépasser 20 caractères" };
                TextBoxSetError(textLastName);
                textLastName.ToolTip = tt;
                message += "\nLe nom n'est pas correcte";
            }
            if (!vm2.StyleValidation(textStyle.Text))
            {
                ToolTip tt = new ToolTip { Content = "Le style ne peut que contenir des lettres et ne doit pas dépasser 15 caractères" };
                TextBoxSetError(textStyle);
                textStyle.ToolTip = tt;
                message += "\nLe style n'est pas correcte";
            }
            if (!vm2.HomeCountryValidation(textHomeCountry.Text))
            {
                ToolTip tt = new ToolTip { Content = "Le pays ne peut que contenir des lettres et ne doit pas dépasser 20 caractères" };
                TextBoxSetError(textHomeCountry);
                textHomeCountry.ToolTip = tt;
                message += "\nLe pays n'est pas correcte";
            }
            if (!vm2.CommentValidation(textComment.Text))
            {
                ToolTip tt = new ToolTip { Content = "Le commentaire ne peut que contenir des lettres et ne doit pas dépasser 50 caractères" };
                TextBoxSetError(textComment);
                textComment.ToolTip = tt;
                message += "\nLe commentaire n'est pas correcte";
            }
            if (!vm2.MEValidation(textMusicalExtract.Text))
            {
                ToolTip tt = new ToolTip { Content = "L'extrait musical doit être un url valide et ne doit pas dépasser 100 caractères" };
                TextBoxSetError(textMusicalExtract);
                textMusicalExtract.ToolTip = tt;
                message += "\nL'extrait musical n'est pas correcte";
                message += "\neg. https://www.youtube.com/watch?v=UOxkGD8qRB4";
            }
            if (message != "")
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
        public void TextBoxInitError(object sender, RoutedEventArgs e)
        {
            var s = sender as TextBox;
            s.Background = Brushes.White;
            s.Foreground = Brushes.Black;
        }

        private async void BtnGetArtists_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                btnGetArtists.IsEnabled = false;

                var response = await client.GetAsync("api/Artists");
                response.EnsureSuccessStatusCode(); // Throw on error code（有错误码时报出异常）.

                var artists = await response.Content.ReadAsAsync<IEnumerable<Model.Artist>>();

                vm.CopyFrom(artists);

            }
            catch (Newtonsoft.Json.JsonException jEx)
            {
                // This exception indicates a problem deserializing the request body.
                // 这个异常指明了一个解序列化请求体的问题。
                MessageBox.Show(jEx.Message);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnGetArtists.IsEnabled = true;
            }
        }

        private void Artists_List_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var a = (Model.Artist)Artists_List.SelectedItem;
            vm2.CopyArtist(a);
        }

        private async void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            btnSubmit.IsEnabled = false;
            if (DataValidation())
            {
                try
                {
                    var response = await client.PutAsJsonAsync("api/Artists/" + vm2.ID.ToString(), vm2);
                    response.EnsureSuccessStatusCode(); // Throw on error code. 
                    MessageBox.Show("Artiste modifié!");

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
        private void BtnPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (vm2 != null && vm2.ID != 0)
            {
                Photo window = new Photo(vm2.ID.ToString());
                window.Owner = this;
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vous devez choisir un artiste!");
            }
        }



        private void ME_OnHyperlinkClick(object sender, RoutedEventArgs e)
        {
            var destination = ((Hyperlink)e.OriginalSource).NavigateUri;
            //string Pattern = @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&$%\$#\=~])*$";
            string Pattern = @"((http|ftp|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?";
            Regex r = new Regex(Pattern);
            Match m = r.Match(destination.ToString());
            if (m.Success)
                Process.Start(destination.ToString());
            else
                MessageBox.Show("Le Url n'est pas valide!");
        }

        public async void TextSearch_TextChanged(object sender, RoutedEventArgs e)
        {
            var response = await client.GetAsync("api/Artists");
            response.EnsureSuccessStatusCode(); // Throw on error code（有错误码时报出异常）.

            var artists = await response.Content.ReadAsAsync<IEnumerable<Model.Artist>>();
            vm.Search(artists, textSearch.Text);
        }
        #endregion
    }
}
