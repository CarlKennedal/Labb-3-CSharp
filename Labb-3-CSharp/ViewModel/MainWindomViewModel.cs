using Labb_3_CSharp.Command;
using Labb_3_CSharp.Dialogs;
using Labb_3_CSharp.Model;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Labb_3_CSharp.Views;

namespace Labb_3_CSharp.ViewModel
{
    internal class MainWindomViewModel : ViewModelBase
    {
        public DelegateCommand CreateNewPackCommand { get; }
        public DelegateCommand SelectPackCommand { get; }
        public DelegateCommand DeletePackCommand { get; }
        public DelegateCommand ExitAppCommand { get; }
        public DelegateCommand PlayButtonCommand { get; }
        public ObservableCollection<QuestionPackViewModel>? Packs { get; set; }
        public PlayerViewModel PlayerViewModel { get; }
        public ConfigurationViewModel ConfigurationViewModel { get; }
        private QuestionPackViewModel? _activePack;
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                RaisePropertyChanged();
            }
        }
        public QuestionPackViewModel? ActivePack
        {
            get => _activePack;
            set
            {
                _activePack = value;
                RaisePropertyChanged();
                ConfigurationViewModel.RaisePropertyChanged("ActivePack");
            }
        }
        public MainWindomViewModel()
        {
            CurrentView = new ConfigurationView();
            PlayerViewModel = new PlayerViewModel(this);
            ConfigurationViewModel = new ConfigurationViewModel(this);
            Packs = new ObservableCollection<QuestionPackViewModel>();
            ActivePack = new QuestionPackViewModel(new QuestionPack("My Question Pack"));
            Packs.Add(ActivePack);
            CreateNewPackCommand = new DelegateCommand(CreateNewPack);
            DeletePackCommand = new DelegateCommand(DeleteSelectedPack, CanRemove);
            SelectPackCommand = new DelegateCommand(ExecuteSelectPack);
            ExitAppCommand = new DelegateCommand(ExitApp);
            PlayButtonCommand = new DelegateCommand(PlayButton);
        }
        private void PlayButton(object obj)
        {
            var playerView = new PlayerView();
            playerView.DataContext = PlayerViewModel;
            CurrentView = playerView;
            PlayerViewModel.LoadQuestion();
        }
        public void EndGame()
        {
            var configurationView = new ConfigurationView();
            configurationView.DataContext = ConfigurationViewModel;
            CurrentView = configurationView;
        }

        private void ExitApp(object obj)
        {
            Application.Current.Shutdown();
        }
        public void CreateNewPack(object parameter)
        {
            var newQuestionPack = new QuestionPack($"Question pack {Packs.Count + 1}");
            var newQuestionPackViewModel = new QuestionPackViewModel(newQuestionPack);
            Packs.Add(newQuestionPackViewModel);
        }
        public void ExecuteSelectPack(object parameter)
        {
            ActivePack = parameter as QuestionPackViewModel;
            RaisePropertyChanged();
        }
        private void DeleteSelectedPack(object obj)
        {
            Packs.Remove(ActivePack);
            RaisePropertyChanged();
        }
        private bool CanRemove(object parameter)
        {
            return ActivePack != null;
        }
    }
}
