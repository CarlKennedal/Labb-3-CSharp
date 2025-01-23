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
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows.Input;
using Labb_3_CSharp.Views;
using System.Text.Json.Serialization;
using Labb_3_CSharp.Commands;

namespace Labb_3_CSharp.ViewModel
{
    internal class MainWindomViewModel : ViewModelBase
    {
        public DelegateCommand CreateNewPackCommand { get; }
        public DelegateCommand SelectPackCommand { get; }
        public DelegateCommand DeletePackCommand { get; }
        public DelegateCommand OpenNewPackCommand { get; }
        public DelegateCommand ExitAppCommand { get; }
        public DelegateCommand PlayButtonCommand { get; }
        public DelegateCommand LoadPacksCommand { get; }
        public DelegateCommand SavePacksCommand { get; }
        public DelegateCommand MenuViewFullScreenCommand { get; }

        public ObservableCollection<QuestionPackViewModel>? Packs { get; set; }
        public PlayerViewModel PlayerViewModel { get; }
        public ConfigurationViewModel ConfigurationViewModel { get; }
        public QuestionPackViewModel? _activePack;
        public static readonly string FilePath = Path.Combine(
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName,
            "carls_question_packs.json"); public DifficultyConverter? _difficultyConverter;
        public DifficultyConverter DifficultyConverter
        {
            get => _difficultyConverter;
            set
            {
                _difficultyConverter = value;
                RaisePropertyChanged();
            }
        }
        private string _packName;
        public string PackName
        {
            get => _packName; set
            {
                _packName = value;
                RaisePropertyChanged();
            }
        }
        private Difficulty _difficulty;
        public Difficulty Difficulty
        {
            get => _difficulty; set
            {
                _difficulty = value;
                RaisePropertyChanged();
            }
        }
        private int _timeLimitInSeconds;
        public int TimeLimitInSeconds
        {
            get => _timeLimitInSeconds; set
            {
                _timeLimitInSeconds = value;
                RaisePropertyChanged();
            }
        }
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
            DifficultyConverter = new DifficultyConverter();
            CreateNewPackCommand = new DelegateCommand(CreateNewPack);
            DeletePackCommand = new DelegateCommand(DeleteSelectedPack, CanRemove);
            SelectPackCommand = new DelegateCommand(ExecuteSelectPack);
            ExitAppCommand = new DelegateCommand(ExitApp);
            PlayButtonCommand = new DelegateCommand(PlayButton);
            OpenNewPackCommand = new DelegateCommand(OpenNewPackWindow);
            MenuViewFullScreenCommand = new DelegateCommand(FullscreenToggle);
            SavePacksCommand = new DelegateCommand(SavePacks);
            LoadPacksCommand = new DelegateCommand(LoadPacks, CanLoad);
            LoadPacksCommand.Execute(this);
            ActivePack = Packs[0];
        }

        private bool CanLoad(object? arg)
        {
            string jsonContent = File.ReadAllText(FilePath);
            return jsonContent != null;
        }

        private void LoadPacks(object obj)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            string jsonContent = File.ReadAllText(FilePath);
            ObservableCollection<QuestionPack> LoadedPacks = JsonSerializer.Deserialize<ObservableCollection<QuestionPack>>(jsonContent, options);
            foreach (var pack in LoadedPacks)
            {
                var LoadedPack = new QuestionPackViewModel(pack);
                Packs.Add(LoadedPack);
            }
        }

        public void SaveToFile()
        {
            ObservableCollection<QuestionPack> existingData = File.Exists(FilePath)
                ? JsonSerializer.Deserialize<ObservableCollection<QuestionPack>>(File.ReadAllText(FilePath))
                : new ObservableCollection<QuestionPack>();

            foreach (var packViewModel in Packs)
            {
                var pack = new QuestionPack(
                    packViewModel.Name,
                    packViewModel.Difficulty,
                    packViewModel.TimeLimitInSeconds,
                    packViewModel.Questions);

                var existingPack = existingData.FirstOrDefault(qp => qp.Name == pack.Name);

                if (existingPack == null)
                {
                    existingData.Add(pack);
                }
                else
                {
                    bool hasChanges =
                        existingPack.Difficulty != pack.Difficulty ||
                        existingPack.TimeLimitInSeconds != pack.TimeLimitInSeconds ||
                        pack.Questions.Any(q =>
                            !existingPack.Questions.Any(eq =>
                                eq.Query == q.Query &&
                                eq.CorrectAnswer == q.CorrectAnswer &&
                                eq.IncorrectAnswers.SequenceEqual(q.IncorrectAnswers))) ||
                        existingPack.Questions.Count != pack.Questions.Count;

                    if (hasChanges)
                    {
                        existingPack.Difficulty = pack.Difficulty;
                        existingPack.TimeLimitInSeconds = pack.TimeLimitInSeconds;

                        existingPack.Questions.Clear();
                        foreach (var question in pack.Questions)
                        {
                            existingPack.Questions.Add(question);
                        }
                    }
                }
            }

            string json = JsonSerializer.Serialize(existingData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        public void SavePacks(object obj)
        {
            SaveToFile();
        }

        private void PlayButton(object obj)
        {
            var playerView = new PlayerView();
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
        public void OpenNewPackWindow(object parameter)
        {
            var newQuestionPackView = new NewQuestionPack();
            newQuestionPackView.DataContext = this;
            newQuestionPackView.ShowDialog();
        }
        public void CreateNewPack(object parameter)
        {
            var newQuestionPack = new QuestionPack(PackName, Difficulty, TimeLimitInSeconds)
            {
                Name = PackName,
                Difficulty = Difficulty,
                TimeLimitInSeconds = TimeLimitInSeconds
            };
            var newQuestionPackViewModel = new QuestionPackViewModel(newQuestionPack);
            Packs.Add(newQuestionPackViewModel);
            ActivePack = newQuestionPackViewModel;
        }
        public void ExecuteSelectPack(object parameter)
        {
            ActivePack = parameter as QuestionPackViewModel;
            RaisePropertyChanged();
        }
        private void DeleteSelectedPack(object obj)
        {
            if (ActivePack == null)
                return;

            Packs.Remove(ActivePack);

            ObservableCollection<QuestionPack> existingData = File.Exists(FilePath)
                ? JsonSerializer.Deserialize<ObservableCollection<QuestionPack>>(File.ReadAllText(FilePath))
                : new ObservableCollection<QuestionPack>();

            var packToRemove = existingData.FirstOrDefault(qp => qp.Name == ActivePack.Name);
            if (packToRemove != null)
            {
                existingData.Remove(packToRemove);
            }

            string json = JsonSerializer.Serialize(existingData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);

            RaisePropertyChanged();
        }
        private bool CanRemove(object parameter)
        {
            return ActivePack != null;
        }

        public void FullscreenToggle(object obj)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            FullscreenToggler.ToggleFullScreen(mainWindow);
        }
    }
}
