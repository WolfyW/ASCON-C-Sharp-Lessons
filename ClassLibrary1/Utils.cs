namespace ClassLibrary1
{
    public static class Utils
    {
        public static void PrintGood(string text)
        {
            Print(text, ConsoleColor.Green);
        }

        public static void PrintBad(string text)
        {
            Print(text, ConsoleColor.Red);
        }

        internal static void Print(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
