using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace WaterQuestionnaire
{
    public class Program
    {
        static void Main(string[] args)
        {
            QuestionHandler questionHandler = new QuestionHandler();

            List<QuestionData> allQuestions = questionHandler.GetQuestions();

            questionHandler.ShowQuestions(allQuestions);

            questionHandler.CheckScore();
        }
    }
}


