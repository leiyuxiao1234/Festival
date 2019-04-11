using Microsoft.Win32;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WPFYuxiao
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Photo : Window
    {
        #region Variables
        int ImageID = 0;
        String strFilePath = "";
        BitmapImage bi;
        Byte[] ImageByteArray;
        private SqlConnection sqlcon = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FestivalContext;Integrated Security=True");
        private DataTable dtblImages;
        public string ArtistID;
        #endregion

        #region Methods
        public Photo(string str)
        {
            this.ArtistID = str;
            InitializeComponent();
            Clear();
            RefreshImageGrid();
           
        }
        public void setArtistID(string str)
        {
            this.ArtistID = str;
        }

        private void RefreshImageGrid()
        {
            
            if (sqlcon.State == ConnectionState.Closed)
                sqlcon.Open();
            SqlCommand cmd = new SqlCommand("select * from sys.tables where name ='Image'", sqlcon);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows == false)
            {
                reader.Close();
                SqlCommand create = new SqlCommand("CREATE TABLE [Image] " +
                    "([ImageID] INT IDENTITY (1, 1) NOT NULL," +
                    " [Title]   VARCHAR (50) NOT NULL," +
                    " [Image]  IMAGE NOT NULL," +
                    " [ArtistID] INT NOT NULL," +
                    "  PRIMARY KEY CLUSTERED ([ImageID] ASC))",
                    sqlcon);
                create.ExecuteNonQuery();
            }
            if(!reader.IsClosed)
            reader.Close();

            
            SqlCommand sel = new SqlCommand("SELECT ImageID,Title FROM Image WHERE ArtistID = "+ArtistID,sqlcon);
            SqlDataAdapter sqlda = new SqlDataAdapter(sel);
            dtblImages = new DataTable();
            if (dtblImages != null)
            {
                sqlda.Fill(dtblImages);
                infoImages.ItemsSource = dtblImages.DefaultView;
            }

            
 
        }

        private void Clear()
        {
            ImageID = 0;
            textTitle.Clear();
            bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri("\\Resources\\No_Image_Available.jpg", UriKind.RelativeOrAbsolute);
            bi.EndInit();
            imagePreview.Source = bi;
            strFilePath = "";
            btnSave.Content = "Save";
        }
        #endregion

        #region Events

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images(.jpg,.png)|*.png;*.jpg";
            if (ofd.ShowDialog() == true)
            {
                strFilePath = ofd.FileName;
                bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource= new Uri(strFilePath,UriKind.RelativeOrAbsolute);
                bi.EndInit();
                imagePreview.Source = bi;
                if (textTitle.Text.Trim().Length == 0)//Auto-Fill title if is empty
                    textTitle.Text = System.IO.Path.GetFileName(strFilePath);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (textTitle.Text.Trim() != "")
            {

                if (strFilePath == "")
                {
                    if (ImageByteArray.Length != 0)
                        ImageByteArray = new byte[] { };
                }
                else
                {
                    System.Drawing.Image temp = new Bitmap(strFilePath);
                    MemoryStream strm = new MemoryStream();
                    temp.Save(strm, System.Drawing.Imaging.ImageFormat.Jpeg);
                    ImageByteArray = strm.ToArray();
                }
                if (sqlcon.State == ConnectionState.Closed)
                    sqlcon.Open();
                SqlCommand sqlCmd = new SqlCommand("UPDATE Image SET Title = @Title ,Image = @Image, ArtistID = @ArtistID WHERE ImageID= @ImageID", sqlcon);
                sqlCmd.Parameters.AddWithValue("@ImageID", ImageID);
                sqlCmd.Parameters.AddWithValue("@Title", textTitle.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Image", ImageByteArray);
                sqlCmd.Parameters.AddWithValue("@ArtistID", ArtistID);
                if (ImageID == 0)
                {
                    sqlCmd = new SqlCommand("INSERT INTO Image (Title,Image,ArtistID) VALUES (@Title,@Image,@ArtistID)", sqlcon);
                    sqlCmd.Parameters.AddWithValue("@Title", textTitle.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Image", ImageByteArray);
                    sqlCmd.Parameters.AddWithValue("@ArtistID", ArtistID);
                }

                sqlCmd.ExecuteNonQuery();
                sqlcon.Close();
                MessageBox.Show("Saved successfuly");
                Clear();
                RefreshImageGrid();
            }
            else
            {
                MessageBox.Show("Please enter image title");
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void InfoImages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = infoImages.SelectedItem;
            DataRowView b = a as DataRowView;
            if (a != null)
            {
                textTitle.Text = b["Title"].ToString();
                ImageID = int.Parse(b["ImageID"].ToString());
                if (sqlcon.State == ConnectionState.Closed)
                    sqlcon.Open();
                SqlCommand cmd = new SqlCommand("SELECT Image FROM Image WHERE ImageID=@ImageID", sqlcon);
                cmd.Parameters.AddWithValue("@ImageID",ImageID);
                byte[] ImageArray = (byte[])cmd.ExecuteScalar();
                if (ImageArray.Length == 0)
                {
                    bi = new BitmapImage();
                    bi.BeginInit();
                    bi.UriSource = new Uri("\\Resources\\No_Image_Available.jpg", UriKind.RelativeOrAbsolute);
                    bi.EndInit();
                    imagePreview.Source = bi;
                }
                else
                {
                    ImageByteArray = ImageArray;
                    MemoryStream ms = new MemoryStream(ImageArray);
                    bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = ms;
                    bi.EndInit();
                    imagePreview.Source = bi;
                }
                btnSave.Content = "Update";
            }
            
        }

        #endregion
    }
}
