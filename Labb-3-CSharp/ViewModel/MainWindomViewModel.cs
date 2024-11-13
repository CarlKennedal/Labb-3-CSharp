using Labb_3_CSharp.Command;
using Labb_3_CSharp.Dialogs;
using Labb_3_CSharp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3_CSharp.ViewModel
{
    internal class MainWindomViewModel : ViewModelBase
    {
        public ObservableCollection<QuestionPackViewModel> ?Packs { get; set; }
        public PlayerViewModel PlayerViewModel { get; }
        public ConfigurationViewModel ConfigurationViewModel { get; }
        public DelegateCommand CreateNewPackCommand { get; }
        public DelegateCommand SelectPackCommand { get; }
        public DelegateCommand DeletePackCommand { get; }

        private QuestionPackViewModel? _activePack;

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
            PlayerViewModel = new PlayerViewModel(this);
            ConfigurationViewModel = new ConfigurationViewModel(this);
            Packs = new ObservableCollection<QuestionPackViewModel>();
            ActivePack = new QuestionPackViewModel(new QuestionPack("My Question Pack"));
            Packs.Add(ActivePack);
            CreateNewPackCommand = new DelegateCommand(CreateNewPack);
            DeletePackCommand = new DelegateCommand(DeleteSelectedPack);
            SelectPackCommand = new DelegateCommand(SelectQuestionPack);
        }


        public void CreateNewPack(object parameter)
        {
            var newQuestionPack = new QuestionPack($"Question pack {Packs.Count + 1}");
            var newQuestionPackViewModel = new QuestionPackViewModel(newQuestionPack);
            Packs.Add(newQuestionPackViewModel);
        }
        public void SelectQuestionPack(object parameter)
        {
            if (parameter is QuestionPack)
            {
                var newActivePack = parameter;
            }
        }
        private void DeleteSelectedPack(object obj)
        {
            throw new NotImplementedException();
        }

    }
}
