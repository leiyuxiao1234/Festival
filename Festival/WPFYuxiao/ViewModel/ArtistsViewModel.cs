using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WPFYuxiao.ViewModel
{
    class ArtistsViewModel : DefaultViewModel
    {
        ObservableCollection<Model.Artist> _mylist = new ObservableCollection<Model.Artist>();
        public ObservableCollection<Model.Artist> mylist
        {

            get { return _mylist; }
            set
            {
                _mylist = value;
                RaisePropertyChanged("mylist");
            }
        }
        //构造函数
        public ArtistsViewModel()
        {
        }

        public void CopyFrom(IEnumerable<Model.Artist> Artists)
        {
            this.mylist.Clear();
            foreach (var a in Artists)
            {
                this.mylist.Add(a);
            }
           
        }

        public void Search(IEnumerable<Model.Artist> Artists,string str)
        {
            str = str.Trim();
            this.mylist.Clear();
            foreach(var a in Artists)
            {
                if((a.Nom.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0) || (a.Prenom.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0)
                    || ((a.Nom+" "+a.Prenom).IndexOf(str, StringComparison.OrdinalIgnoreCase)>=0) || ((a.Prenom + " " + a.Nom).IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    this.mylist.Add(a);
                }
            }
        }
    }
}
