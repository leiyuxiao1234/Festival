using System.Windows;

namespace WPFYuxiao.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnArtist_Click(object sender, RoutedEventArgs e)
        {
            Artist artist = new Artist();
            artist.ShowDialog();
        }


        private void BtnArtistsList_Click(object sender, RoutedEventArgs e)
        {
            ArtistsList artistList = new ArtistsList();
            artistList.ShowDialog();
        }

        private void BtnPlace_Click(object sender, RoutedEventArgs e)
        {
            Place place = new Place();
            place.ShowDialog();
        }
    }
}
