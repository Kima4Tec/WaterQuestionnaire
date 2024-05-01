using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace WaterQuestionnaire
{
    public class Program
    {
        //Properties
        /// <summary>
        /// Counter
        /// </summary>
        public static int AntalRigtige { get; set; }
        /// <summary>
        /// Counter
        /// </summary>
        public static int AntalForkerte { get; set; }
        /// <summary>
        /// Right place for correct answer
        /// </summary>
        public static int Right { get; set; }

        static void Main(string[] args)
        {
            List<QuestionData> allQuestions = GetQuestions();

            ShowQuestions(allQuestions);

            CheckScore();
        }

        public static void ShowQuestions(List<QuestionData> allQuestions)
        {
            //Creating a list of 6 questions, if the json file consist of more than the needed questions, this choses 6
            //random questions to use
            List<QuestionData> selectedQuestions = allQuestions.GetRange(0, 6);

            //Loop running through the chosen number of questions
            for (int i = 0; i < selectedQuestions.Count; i++)
            {
                QuestionData theQuestion = selectedQuestions[i];
                int rigthAnswerNr = theQuestion.Answer;

                string question = theQuestion.Question;
                string option1 = theQuestion.Options[0];
                string option2 = theQuestion.Options[1];
                string option3 = theQuestion.Options[2];
                string explanation = theQuestion.Explanation;

                int cursorLeft = 4;
                int cursorTop = 3;

                //Showing text and questions on console
                Console.WriteLine($"Danskerne bruger for meget vand når der er tørke. " +
                    $"Bliv klogere på, hvad der bruger mest vand i hjemmet ved at tage denne quiz. " +
                    $"Flyt cursoren med piltasterne, sæt et x for svar og tryk på enter for næste spørgsmål.");

                Console.SetCursorPosition(cursorLeft, cursorTop);
                Console.WriteLine($"{question}");
                Console.SetCursorPosition(cursorLeft, cursorTop + 2);
                Console.WriteLine($"[ ] {option1}");
                Console.SetCursorPosition(cursorLeft, cursorTop + 3);
                Console.WriteLine($"[ ] {option2}");
                Console.SetCursorPosition(cursorLeft, cursorTop + 4);
                Console.WriteLine($"[ ] {option3}");

                Right = rigthAnswerNr + 5;

                FlytCursoren.FlytOpogNed(explanation, 5, 5);

                Console.Clear();
            }
        }

        /// <summary>
        /// Handling Json file and getting the questions.
        /// </summary>
        /// <returns></returns>
        public static List<QuestionData> GetQuestions()
        {
            //Name of json-file into variable
            string jsonFilePath = "QuestionAndAnswers.json";
            string json;

            try
            {
                // Read JSON-data from file and add to the variable json
                json = File.ReadAllText(jsonFilePath);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Fejl: Filen '{jsonFilePath}' blev ikke fundet.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl under læsning af filen: {ex.Message}");
                return null;
            }
            List<QuestionData> allQuestions;

            // Deserialise JSON-data to a list of QuestionData-objects
            allQuestions = JsonConvert.DeserializeObject<List<QuestionData>>(json);

            //Shuffle questions - especially for json with a large amount of questions
            Shuffle(allQuestions);

            return allQuestions;

        }

        public static void CheckScore()
        {
            if (AntalRigtige > 4)
            {
                Skriv(4, 8, "Fantastisk!! Du ved en masse om emnet!");
            }
            else if (AntalRigtige > 2)
            {
                Skriv(4, 8, "Det var flot!");
            }
            else
            {
                Skriv(4, 8, "Du kunne have gjort det bedre.");
            }

            Skriv(4, 10, $"Antal rigtige svar: {Program.AntalRigtige}\tAntal forkerte svar: {Program.AntalForkerte}");
            Console.ReadLine();
        }

        // Fisher-Yates shuffle algorithm
        static void Shuffle<T>(IList<T> list)
        {
            Random random = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        /// <summary>
        /// Set position for closing text
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tekst"></param>
        public static void Skriv(int x, int y, String tekst)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(tekst);
        }

    }
}


