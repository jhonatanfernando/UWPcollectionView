using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SampleProject.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set//Better to be protected
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }
        }

        public string[] VipValues
        {
            get
            {
                return new string[] { "VIPC", "VIPP", "VIP", "" };
            }
        }

        public string[] FlightClass
        {
            get
            {
                return new string[] { "F", "A", "O", "C", "D", "I", "Z", "J", "P", "R" };
            }
        }
    }
}
