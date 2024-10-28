using Labb_3_CSharp.Command;
using System.Windows.Threading;

namespace Labb_3_CSharp.ViewModel
{
    internal class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindomViewModel? mainWindomViewModel;

        private DispatcherTimer timer;

        private string _testData;
        public string TestData { 
            get => _testData;
            private set 
            {
                _testData = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand UpdateButtonCommand { get; }

        public PlayerViewModel(MainWindomViewModel? mainWindomViewModel)
        {
            this.mainWindomViewModel = mainWindomViewModel;

            TestData = "Start value: ";

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            //timer.Start();

            UpdateButtonCommand = new DelegateCommand(UpdateButton,CanUpdateButton);
        }
        private bool CanUpdateButton(object? arg) => TestData.Length < 20;

        private void UpdateButton(object obj)
        {
            TestData += "x";
            UpdateButtonCommand.RaiseCanExecuteChanged();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            TestData += "x";
        }
    }
}
