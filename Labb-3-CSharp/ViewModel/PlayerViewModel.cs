﻿using Labb_3_CSharp.Command;
using Labb_3_CSharp.Dialogs;
using Labb_3_CSharp.Model;
using Labb_3_CSharp.Views;
using System.Windows.Threading;

namespace Labb_3_CSharp.ViewModel
{
    internal class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindomViewModel? mainWindomViewModel;
        public QuestionPackViewModel? ActivePack { get => mainWindomViewModel?.ActivePack; }
        private Question _currentQuestion;
        public DispatcherTimer timer;
        public int QuestionAmount { get; set; } = 0;
        public int CurrentQuestionIndex { get; set; } = 0;
        public int ScoreKeeper { get; set; }
        private string _questionAmountDisplay;
        public string QuestionAmountDisplay
        {
            get => _questionAmountDisplay;
            set
            {
                _questionAmountDisplay = value;
                RaisePropertyChanged();
            }
        }
        private string _scoreKeeperDisplay;
        public string ScoreKeeperDisplay
        {
            get => _scoreKeeperDisplay;
            set
            {
                _scoreKeeperDisplay = value;
                RaisePropertyChanged();
            }
        }
        public Question CurrentQuestion
        {
            get => _currentQuestion;
            set
            {
                _currentQuestion = value;
                RaisePropertyChanged();
            }
        }
        private List<string> _shuffledAnswers;
        public List<string> ShuffledAnswers
        {
            get => _shuffledAnswers;
            set
            {
                _shuffledAnswers = value;
                RaisePropertyChanged();
            }
        }

        private string _testData;
        public string TestData
        {
            get => _testData;
            private set
            {
                _testData = value;
                RaisePropertyChanged();
            }
        }
        public DelegateCommand UpdateButtonCommand { get; }
        public DelegateCommand AnswerButtonCommand { get; }

        public PlayerViewModel(MainWindomViewModel? mainWindomViewModel)
        {
            this.mainWindomViewModel = mainWindomViewModel;

            TestData = "Start value: ";

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            //timer.Start();

            UpdateButtonCommand = new DelegateCommand(UpdateButton, CanUpdateButton);
            AnswerButtonCommand = new DelegateCommand(AnswerButton);
        }

        public void LoadQuestion()
        {
            if (CurrentQuestionIndex >= ActivePack.Questions.Count)
            {
                CurrentQuestionIndex = 0;
                ScoreKeeper = 0;
                this.mainWindomViewModel.EndGame();
                RaisePropertyChanged();
            }
            if (CurrentQuestionIndex < ActivePack.Questions.Count)
            {
                QuestionAmountDisplay = $"Question: {CurrentQuestionIndex + 1} of {ActivePack.Questions.Count}";
                QuestionAmount = ActivePack.Questions.Count;
                CurrentQuestion = ActivePack.Questions[CurrentQuestionIndex];
                ShuffledAnswers = QuizHelper.GetShuffledAnswers(CurrentQuestion);
                RaisePropertyChanged();
            }
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
        private void AnswerButton(object obj)
        {
            if (obj is string)
            {
                if (obj as String == CurrentQuestion.CorrectAnswer)
                {
                    ScoreKeeper = +1;
                    ScoreKeeperDisplay = $"Score: {ScoreKeeper} out of {QuestionAmount}";
                    CurrentQuestionIndex++;
                    RaisePropertyChanged();
                    LoadQuestion();
                }
                else
                {
                    CurrentQuestionIndex++;
                    RaisePropertyChanged();
                    LoadQuestion();
                }
            }
        }
    }
}
