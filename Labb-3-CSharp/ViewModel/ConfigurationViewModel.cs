using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3_CSharp.ViewModel
{
    internal class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindomViewModel? mainWindomViewModel;

        public ConfigurationViewModel(MainWindomViewModel? mainWindomViewModel)
        {
            this.mainWindomViewModel = mainWindomViewModel;
        }

        public QuestionPackViewModel? ActivePack { get => mainWindomViewModel.ActivePack; }
    }
}
