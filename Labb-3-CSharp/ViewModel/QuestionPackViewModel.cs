using Labb_3_CSharp.Command;
using Labb_3_CSharp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace Labb_3_CSharp.ViewModel
{
    internal class QuestionPackViewModel : ViewModelBase
    {
        public readonly QuestionPack pack;
        public QuestionPackViewModel(QuestionPack questionPack) 
        {
            pack = questionPack;
        }

        public string Name 
        { get => pack.Name; 
            set
            {
                pack.Name = value;
                RaisePropertyChanged();
            }
        }
        public Difficulty Difficulty
        {
            get => pack.Difficulty;
            set
            {
                pack.Difficulty = value;
                RaisePropertyChanged();
            }
        }
        public int TimeLimitInSeconds
        {
            get => pack.TimeLimitInSeconds;
            set
            {
                pack.TimeLimitInSeconds = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<Question> _questions;
        public ObservableCollection<Question> Questions {
            get
            {
                if (_questions == null && pack.Questions != null)
                {
                    _questions = new ObservableCollection<Question>(pack.Questions);
                }
                return _questions;
            }
            set
            {
                if (_questions != value)
                {
                    _questions = value;

                    if (_questions != null)
                    {
                        pack.Questions = new ObservableCollection<Question>(_questions);
                    }
                    else
                    {
                        pack.Questions = null;
                    }
                    RaisePropertyChanged();
                }
            }
        }

    }
}
