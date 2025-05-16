using LegyenOnIsMilliomos;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var fileAction = new FileAction();
        List<Question> normQuestions = await fileAction.ReadFileAsync("kerdes");
        List<Question> sorQuestions = await fileAction.ReadFileAsync("sorkerdes");

        System.Console.WriteLine(normQuestions[0]);
        System.Console.WriteLine(normQuestions[1]);

        ColoredWrite.Line(sorQuestions[5].Ques, ConsoleColor.Green, ConsoleColor.Black);
        ColoredWrite.Line("Hello", ConsoleColor.Yellow, ConsoleColor.Blue);
        System.Console.WriteLine(sorQuestions[0]);
        System.Console.WriteLine(sorQuestions[1]);


        System.Console.WriteLine(normQuestions[0].IsItCorrect(['B'])?"mukodk":"nem");
        System.Console.WriteLine(sorQuestions[1].IsItCorrect(['B','c','a','d'])?"mukodk":"nem");
    }
}