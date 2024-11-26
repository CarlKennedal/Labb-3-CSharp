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

namespace Labb_3_CSharp.ViewModel
{
    internal class QuestionPackViewModel : ViewModelBase
    {
        private static readonly string FilePath = "question_packs.json";
        private readonly QuestionPack model;
        public QuestionPackViewModel(QuestionPack model) 
        {
            this.model = model;
            this.Questions = new ObservableCollection<Question>(model.Questions);
            
        }
        public string Name 
        { get => model.Name; 
            set
            {
                model.Name = value;
                RaisePropertyChanged();
                SaveToFile();
            }
        }
        public Difficulty Difficulty
        {
            get => model.Difficulty;
            set
            {
                model.Difficulty = value;
                RaisePropertyChanged();
                SaveToFile();
            }
        }
        public int TimeLimitInSeconds
        {
            get => model.TimeLimitInSeconds;
            set
            {
                model.TimeLimitInSeconds = value;
                RaisePropertyChanged();
                SaveToFile();
            }
        }
        public ObservableCollection<Question> Questions { get; }

        //private QuestionPack ToSerializedState() 
        //{
        //    return new QuestionPack()
        //    {
        //        Name = this.Name,
        //        Difficulty = this.Difficulty,
        //        TimeLimitInSeconds = this.TimeLimitInSeconds,
        //        Questions = new List<Question>(this.Questions),
        //    };
        //}

        public void SaveToFile()
        {
            try
            {
                var existingData = File.Exists(FilePath) ?
                    JsonSerializer.Deserialize<List<QuestionPack>>(File.ReadAllText(FilePath))
                    : new List<QuestionPack>();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
    }
}
