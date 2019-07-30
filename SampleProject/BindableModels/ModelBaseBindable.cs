using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SampleProject.BindableModels
{
    public class ModelBaseBindable : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}