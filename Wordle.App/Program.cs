namespace Wordle.App
{
    public class Program
    {
        public static void Main()
        {
            string? input;

            WordDict.Init(123);
            GameSession.Init(WordDict.Instance.Pick(), 3);
            
            while (true)
            {
                input = Console.ReadLine()?.ToLower();

                if (input?.Length != 5)
                {
                    Console.WriteLine("Некорректный ввод");
                    continue;
                }
            }
        }
    }
}