﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3_CSharp.Model
{
    public class Question
    {
        public Question(string query, string correctAnswer,
            string incorrectAnswer1, string incorrectAnswer2, string incorrectAnswer3)
        {
            Query = query;
            CorrectAnswer = correctAnswer;
            IncorrectAnswers = new string[3] { incorrectAnswer1, incorrectAnswer2, incorrectAnswer3 };
        }
        public Question()
        {

        }

        public string Query { get; set; }
        public string CorrectAnswer { get; set; }

        public string[] IncorrectAnswers { get; set; }


    }
}
