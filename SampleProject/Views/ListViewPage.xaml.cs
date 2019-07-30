using SampleProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage : ContentPage
    {
        private ViewModelData viewModel;
        private Random rd = new Random();
        private bool timeEnabled = true;

        public ListViewPage()
        {
            InitializeComponent();
            this.BindingContext = viewModel = new ViewModelData();


            //Simulating the events from SignalR 
            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                var rowToRemove = rd.Next(0, 10);
                viewModel.ObjectBindables.RemoveAt(rowToRemove);

                var indexToAdd = rd.Next(0, 10);
                var rowToAdd = rd.Next(11, 99);
                viewModel.ObjectBindables.Insert(indexToAdd, viewModel.ReadOnlyData[rowToAdd]);

                return timeEnabled;
            });
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.LoadData();
        }

        protected override void OnDisappearing()
        {
            timeEnabled = false;
            base.OnDisappearing();
        }
    }
}