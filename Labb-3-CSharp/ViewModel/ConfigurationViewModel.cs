using Labb_3_CSharp.Command;
using Labb_3_CSharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Labb_3_CSharp.ViewModel
{
    internal class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindomViewModel? mainWindomViewModel;

        private Question _selectedQuestion;
        public Question SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                _selectedQuestion = value;
                RaisePropertyChanged(nameof(SelectedQuestion));
            }
        }
        public DelegateCommand AddButtonCommand { get; }
        public DelegateCommand RemoveButtonCommand { get; }

        public ConfigurationViewModel(MainWindomViewModel? mainWindomViewModel)
        {
            this.mainWindomViewModel = mainWindomViewModel;

            AddButtonCommand = new DelegateCommand(AddButton);
            RemoveButtonCommand = new DelegateCommand(RemoveButton, RemoveButtonActive);
        }


        public QuestionPackViewModel? ActivePack { get => mainWindomViewModel?.ActivePack; }
        
        private void AddButton(object parameter)
        {
           ActivePack?.Questions.Add(new Question("Fråga","Rätt svar","Fel svar 1","Fel svar två","Fel svar tre"));
           AddButtonCommand.RaiseCanExecuteChanged();
        }

        private void RemoveButton(object parameter) 
        {
            ActivePack?.Questions.Remove(SelectedQuestion);
            RemoveButtonCommand.RaiseCanExecuteChanged();
        }
        private bool CanRemove(object parameter)
        {
            return SelectedQuestion != null;
        }
    }
}
