using QuizGame.Models;
using QuizGame.Models.Interfaces;
using QuizGame.Services;
using QuizGame.Services.utils;

namespace QuizGame.UIController
{
    public class UIQuizController
    {
        public static Dictionary<int, Collection> QuizzCollection = new Dictionary<int, Collection>();
        public static Collection? SelectedCollection { get; set; }
        public static Quiz? SelectedQuizz { get; set; }

        private readonly IQuizServices _services;

        public UIQuizController(IQuizServices services)
        {
            _services = services;
        }

        public static void RunGame()
        {
        }
        private static void ShowMenu()
        {
            Console.WriteLine(" __________ ");
            Console.WriteLine("  QUIZZ GAME ");
            Console.WriteLine(" ---------- ");
            Console.WriteLine("Available Quizz Collections");
        }

        //private void InitializeCollection()
        //{
        //    List<Collection> collections = _services.GenerateCollections();
        //    for (int i = 0; i < collections.Count; i++)
        //        QuizzCollection.Add(i + 1, collections[i]);
        //}
        //private static void ShowCollections()
        //{
        //    foreach (var pair in QuizzCollection)
        //        Console.WriteLine($"[{pair.Key}] {pair.Value.Name}");
        //}
        //private static void ShowQuizzes()
        //{
        //    Console.Clear();
        //    Console.WriteLine($"Selected Collection: {SelectedCollection?.Name}");
        //    Console.WriteLine($"Quizzes in {SelectedCollection?.Name}");
        //    for (int i = 0; i < SelectedCollection?.Quizzes?.Count; i++)
        //        Console.WriteLine($"[{i + 1}] {SelectedCollection?.Quizzes[i].QuizName}");
        //}
        //private static void ShowSelectedquizz()
        //{
        //    Console.Clear();
        //    Console.WriteLine(SelectedQuizz!.QuizName!);
        //    Console.WriteLine(SelectedQuizz!.Description!);
        //    Console.WriteLine("-----------");
        //}
        //private static void CollectionSelection()
        //{
        //    InitializeCollection();
        //    ShowMenu();
        //    ShowCollections();
        //    string optionValidated = InputUtils.RequestValue();
        //    int selection = InputUtils.Selection(optionValidated);
        //    var Collection = new Collection();
        //    QuizzCollection.TryGetValue(selection, out Collection);
        //    SelectedCollection = Collection;
        //}
        //private static void QuizSelection()
        //{
        //    ShowQuizzes();
        //    string optionValidated = InputUtils.RequestValue();
        //    int selection = InputUtils.Selection(optionValidated);
        //    SelectedQuizz = SelectedCollection.Quizzes.ElementAt(selection - 1);
        //    ShowSelectedquizz();
        //}
        //private static void AnswerQuestions()
        //{
        //    foreach(IQuestion question in SelectedQuizz!.questions!)
        //    {
        //        question.PrintQuestion();
        //        string answer = InputUtils.RequestValue();
        //        if (typeof(QuestionMultipleChoice) == question.GetType())
        //        {
        //            answer = InputUtils.VerifyAnswerMultipleChoice((QuestionMultipleChoice) question, answer);
        //        }
        //        if (answer == question.CorrectAnswer)
        //        {
        //            Console.WriteLine("Great!");
        //            Console.WriteLine("+" + question.Score + "pts");
        //        }
        //    }
        //}
    }
}