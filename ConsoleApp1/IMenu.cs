public interface IMenu
{
    Object Execute();

    List<MenuOption> MenuOptions { get; set; }
    String PromptMessage { get; set; }
    String IntroMessage { get; set; }
    String WrongOptionMessage { get; set; }
    Dictionary<DelimiterType, String> Delimiters { get; set; }
}
