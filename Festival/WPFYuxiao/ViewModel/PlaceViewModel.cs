namespace WPFYuxiao.ViewModel
{
    class PlaceViewModel : DefaultViewModel
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
                if (!string.Equals(this.nom, value))
                {
                    this.nom = value;
                    this.RaisePropertyChanged("Nom");
                }
            }
        }

        private int capacite;
        public int Capacite
        {
            get { return this.capacite; }
            set
            {
                if (!int.Equals(this.capacite, value))
                {
                    this.capacite = value;
                    this.RaisePropertyChanged("Capacite");
                }
            }
        }

        private string description;
        public string Description
        {
            get { return this.description; }
            set
            {
                if (!string.Equals(this.description, value))
                {
                    this.description = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }

        private int idFestival;
        public int IDFestival
        {
            get { return this.idFestival; }
            set
            {
                if (!int.Equals(this.idFestival, value))
                {
                    this.idFestival = value;
                    this.RaisePropertyChanged("IDFestival");
                }
            }
        }
    }
}
