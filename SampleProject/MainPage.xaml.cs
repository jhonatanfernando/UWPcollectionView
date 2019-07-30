using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SampleProject
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnListViewClick(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new ListViewPage());
        }

        private void OnCollectionView(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new CollectionViewPage());
        }
    }
}
