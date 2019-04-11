using System.Text.RegularExpressions;

namespace WPFYuxiao.ViewModel
{
    class ArtistViewModel : DefaultViewModel
    {
        private int id;
        public int ID
        {
            get { return this.id; }
            set
            {
                if (!int.Equals(this.id, value))
                {
                    this.id = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }

        private string nom;
        public string Nom
        {
            get { return this.nom; }
            set
            {
                // Implement with property changed handling for INotifyPropertyChanged
                if (!string.Equals(this.nom, value))
                {
                    this.nom = value;
                    this.RaisePropertyChanged("Nom"); // Method to raise the PropertyChanged event in your BaseViewModel class...
                }
            }
        }

        private string prenom;
        public string Prenom
        {
            get { return this.prenom; }
            set
            {
                // Implement with property changed handling for INotifyPropertyChanged
                if (!string.Equals(this.prenom, value))
                {
                    this.prenom = value;
                    this.RaisePropertyChanged("Prenom"); // Method to raise the PropertyChanged event in your BaseViewModel class...
                }
            }
        }

        private string style;
        public string Style
        {
            get { return this.style; }
            set
            {
                // Implement with property changed handling for INotifyPropertyChanged
                if (!string.Equals(this.style, value))
                {
                    this.style = value;
                    this.RaisePropertyChanged("Style"); // Method to raise the PropertyChanged event in your BaseViewModel class...
                }
            }
        }

        private string pays;
        public string Pays
        {
            get { return this.pays; }
            set
            {
                // Implement with property changed handling for INotifyPropertyChanged
                if (!string.Equals(this.pays, value))
                {
                    this.pays = value;
                    this.RaisePropertyChanged("Pays"); // Method to raise the PropertyChanged event in your BaseViewModel class...
                }
            }
        }

        private string commentaire;
        public string Commentaire
        {
            get { return this.commentaire; }
            set
            {
                // Implement with property changed handling for INotifyPropertyChanged
                if (!string.Equals(this.commentaire, value))
                {
                    this.commentaire = value;
                    this.RaisePropertyChanged("Commentaire"); // Method to raise the PropertyChanged event in your BaseViewModel class...
                }
            }
        }

        private string extraitM;
        public string ExtraitM
        {
            get { return this.extraitM; }
            set
            {
                // Implement with property changed handling for INotifyPropertyChanged
                if (!string.Equals(this.extraitM, value))
                {
                    this.extraitM = value;
                    this.RaisePropertyChanged("ExtraitM"); // Method to raise the PropertyChanged event in your BaseViewModel class...
                }
            }
        }

        public void CopyArtist(Model.Artist a)
        {
            if (a != null)
            {
                ID = a.ID;
                Nom = a.Nom;
                Prenom = a.Prenom;
                Style = a.Style;
                Pays = a.Pays;
                Commentaire = a.Commentaire;
                ExtraitM = a.ExtraitM;
            }
            
        }

        public bool NameValidation(string txt)
        {
            if(!Regex.IsMatch(txt, @"^[A-Za-z ]+$") || txt=="" || txt==null || txt.Length>20)
            {
                return false;
            }
            return true;
        }

        public bool FirstNameValidation(string txt)
        {
            txt = txt.Trim();
            if (!Regex.IsMatch(txt, @"^[A-Za-z ]+$") || txt == "" || txt == null || txt.Length > 20)
            {
                return false;
            }
            return true;
        }

        public bool LastNameValidation(string txt)
        {
            txt = txt.Trim();
            if (!Regex.IsMatch(txt, @"^[A-Za-z]+$") || txt == "" || txt == null || txt.Length > 20)
            {
                return false;
            }
            return true;
        }

        public bool StyleValidation(string txt)
        {
            txt = txt.Trim();
            if (txt == "") { return true; }
            else if (!Regex.IsMatch(txt, @"^[A-Za-z ]+$") || txt.Length > 15)
            {
                return false;
            }
            return true;
        }

        public bool HomeCountryValidation(string txt)
        {
            txt = txt.Trim();
            if (txt == "") { return true; }
            else if (!Regex.IsMatch(txt, @"^[A-Za-z ]+$") || txt.Length > 20)
            {
                return false;
            }
            return true;
        }

        public bool CommentValidation(string txt)
        {
            txt = txt.Trim();
            if (txt == "") { return true; }
            else if (txt.Length > 50)
            {
                return false;
            }
            return true;
        }

        public bool MEValidation(string txt)
        {
            txt = txt.Trim();
            if (txt == "") { return true; }
            else if (!Regex.IsMatch(txt, @"((http|ftp|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?") || txt.Length > 100)
            {
                return false;
            }
            return true;
        }
    }
}
