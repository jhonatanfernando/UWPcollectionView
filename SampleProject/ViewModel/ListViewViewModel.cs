using SampleProject.BindableModels;
using SampleProject.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SampleProject.ViewModel
{
    public class ViewModelData : ViewModelBase
    {
        private ObservableCollection<ObjectBindable> objectBindables = new ObservableCollection<ObjectBindable>();

        private ObservableCollection<ObjectBindable> readOnlyData = new ObservableCollection<ObjectBindable>();

        private CancellationTokenSource pageChangeCts = new CancellationTokenSource();

        private int pageSize = 10;
        
        public ObservableCollection<ObjectBindable> ReadOnlyData
        {
            get
            {
                return readOnlyData;
            }
        }


        public ObservableCollection<ObjectBindable> ObjectBindables
        {
            get { return objectBindables; }
            set { objectBindables = value; }
        }

        public ViewModelData()
        {
            LoadOriginalData();

            InitPagination();
        }

        private void LoadOriginalData()
        {
            Random rd = new Random();

            CPTotalPassengers = 100;

            for (int i = 0; i < 100; i++)
            {
                readOnlyData.Add(new ObjectBindable()
                {
                    FullName = "Name  " + (i+1).ToString(),
                    StatusType = (StatusLevel)Enum.ToObject(typeof(StatusLevel), rd.Next(0,5)), 
                    LevelCode = (i%2 ==0 ? "UA*G" : "HON"),
                    ClassOfTravel = FlightClass[rd.Next(0, 10)],
                    VIPLevelCode = VipValues[rd.Next(0, 4)]
                });
            }

            var totalPage = (readOnlyData.Count / pageSize) + (readOnlyData.Count % pageSize != 0 ? 1 : 0);
            var tempList = new List<string>();
            for (int i = 0; i < totalPage; i++)
            {
                tempList.Add((i + 1).ToString());
            }

            CPPages = tempList;
        }

        public async Task LoadData()
        {
            IsBusy = true;

            var result = readOnlyData.ToList().Skip(CPCurrentPage * pageSize).Take(pageSize);

            ObjectBindables.Clear();

            foreach (var item in result)
            {
                ObjectBindables.Add(item);
            }

            IsBusy = false;

            await Task.FromResult(true);
        }

        #region Pagination
        private int _cPTotalTransports = 0;
        public int CPTotalPassengers
        {
            get { return _cPTotalTransports; }
            private set
            {
                _cPTotalTransports = value;
                if (_cPTotalTransports != 0)
                {
                    var tempCurrentPage = CPCurrentPage;

                    while (_cPTotalTransports <= ((tempCurrentPage) * pageSize))
                    {
                        tempCurrentPage--;
                    }

                    CPCurrentPage = tempCurrentPage;
                }
                OnPropertyChanged("CPTotalTransports");
                RefreshCPPagingCommands();
            }
        }

        public bool CPHasNextPage
        {
            get { return CPTotalPassengers > ((CPCurrentPage + 1) * pageSize); }
        }

        public bool CPHasPreviousPage
        {
            get { return CPCurrentPage > 0; }
        }

        public bool CPHasPaging
        {
            get { return CPTotalPassengers > pageSize; }
        }

        public bool IsLoadingCPPaging { get; private set; } = false;

        private List<string> _cPPages = new List<string>();
        public List<string> CPPages
        {
            get
            {
                return _cPPages;
            }
            set
            {
                _cPPages = value;
                OnPropertyChanged("CPPages");
                OnPropertyChanged("CPCurrentPage");
            }
        }

        private int _cPCurrentPage = 0;
        public int CPCurrentPage
        {
            get { return _cPCurrentPage; }
            set
            {
                if (value < 0) return;
                if (value > _cPCurrentPage && !CPHasNextPage) return;
                if (value < _cPCurrentPage && !CPHasPreviousPage) return;
                if (value == _cPCurrentPage) return;
                ChangeCPPage(value);
                OnPropertyChanged("CPHasNextPage");
                OnPropertyChanged("CPHasPreviousPage");
            }
        }

        public ICommand CPNextPageCommand { get; private set; }

        public ICommand CPPreviousPageCommand { get; private set; }

        private void ChangeCPPage(int pageIndex)
        {
            try
            {

                IsLoadingCPPaging = true;
                OnPropertyChanged("IsLoadingCPPaging");

                Interlocked.Exchange(ref pageChangeCts, new CancellationTokenSource()).Cancel();
                Device.BeginInvokeOnMainThread(() =>
                {
                    // IsLoadingMorePassengers = true;
                    IsBusy = true;
                });

                Task.Delay(TimeSpan.FromMilliseconds(500), pageChangeCts.Token)
                    .ContinueWith(async (t) =>
                    {
                        _cPCurrentPage = pageIndex;
                        await LoadData();
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            OnPropertyChanged("CPCurrentPage");
                            OnPropertyChanged("CPPages");
                            RefreshCPPagingCommands();
                        });
                    },
                    CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion,
                    TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch { }
        }

        private void RefreshCPPagingCommands()
        {
            OnPropertyChanged("CPHasPreviousPage");
            OnPropertyChanged("CPHasNextPage");
            OnPropertyChanged("CPHasPaging");
            ((Command)CPNextPageCommand)?.ChangeCanExecute();
            ((Command)CPPreviousPageCommand)?.ChangeCanExecute();

            IsLoadingCPPaging = false;
            OnPropertyChanged("IsLoadingCPPaging");
        }

        private void InitPagination()
        {
            CPNextPageCommand = new Command(() =>
            {
                CPCurrentPage++;
            }, () => { return CPHasNextPage; });

            CPPreviousPageCommand = new Command(() =>
            {
                CPCurrentPage--;
            }, () => { return CPHasPreviousPage; });
        }

        #endregion
    }
}
