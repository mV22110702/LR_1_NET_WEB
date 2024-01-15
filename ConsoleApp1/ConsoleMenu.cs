public class ConsoleMenu : IMenu
{
    public ConsoleMenu() { }
    public ConsoleMenu(List<MenuOption> menuOptions)
    {
        this.MenuOptions = menuOptions;
    }
    public List<MenuOption> MenuOptions { get; set; } = new List<MenuOption>();
    public String PromptMessage { get; set; } = String.Empty;
    public String IntroMessage { get; set; } = String.Empty;
    public String WrongOptionMessage { get; set; } = String.Empty;
    public Dictionary<DelimiterType, String> Delimiters { get; set; } = new Dictionary<DelimiterType, string> {
        { DelimiterType.StartEndMenuDelimiter, "================" },
        { DelimiterType.IndexDelimiter, " | " }
    };
    protected void PrintMenu()
    {

        Console.WriteLine(this.Delimiters[DelimiterType.StartEndMenuDelimiter]);
        for (int i = 0; i < this.MenuOptions.Count; i++)
        {
            Console.WriteLine((i + 1) + this.Delimiters[DelimiterType.IndexDelimiter] + this.MenuOptions[i].description);
        }
        Console.WriteLine(this.Delimiters[DelimiterType.StartEndMenuDelimiter]);
    }
    public Object Execute()
    {
        Int32 chosenOption = -1;
        var isParseSuccess = false;
        var isOptionIncorrect = true;

        Console.WriteLine(this.IntroMessage);
        while (isOptionIncorrect)
        {
            PrintMenu();
            isParseSuccess = Int32.TryParse(Console.ReadLine(), out chosenOption);
            isOptionIncorrect = !isParseSuccess || chosenOption < 0 || (chosenOption > this.MenuOptions.Count);
            if (isOptionIncorrect)
            {
                Console.WriteLine();
                Console.WriteLine(this.WrongOptionMessage);
            }
        }
        Console.WriteLine();
        return this.MenuOptions[chosenOption - 1].handler();
    }
}
