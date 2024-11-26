using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3_CSharp.Model
{
    enum Difficulty {Easy, Medium, Hard};

    internal class QuestionPack : INotifyPropertyChanged
    {
        public QuestionPack(string name, Difficulty difficulty = Difficulty.Medium, int timeLimitInSeconds = 30)
        {
            Name = name;
            Difficulty = difficulty;
            TimeLimitInSeconds = timeLimitInSeconds;
            Questions = new List<Question>();
        }

        public string Name { get; set; }
        private Difficulty _difficulty;


        public Difficulty Difficulty {
            get => _difficulty;
            set 
            {
                _difficulty = value;
                RaisePropertyChanged();
            } 
        }

        public int TimeLimitInSeconds { get; set; }

        public List<Question> Questions { get; set; }
        public IEnumerable<int> Difficulties => Enum.GetValues(typeof(Difficulty)).Cast<int>();

        public event PropertyChangedEventHandler? PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
