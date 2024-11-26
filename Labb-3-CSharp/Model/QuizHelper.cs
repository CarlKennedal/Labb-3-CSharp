using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3_CSharp.Model
{
    public static class QuizHelper
    {
        public static List<string> GetShuffledAnswers(Question question) 
        {
            var Answers = new List<string>(question.IncorrectAnswers)
            {
                question.CorrectAnswer
            };

            Random rand = new Random();
            return Answers.OrderBy(a => rand.Next()).ToList();
        }
    }
}
