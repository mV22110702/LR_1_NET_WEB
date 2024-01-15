interface IMenuBuilder
{
    void SetPromptMessage(String promptMessage);
    void SetIntroMessage(String introMessage);
    void SetWrongOptionMessage(String WrongOptionMessage);
    void SetDelimiters(Dictionary<DelimiterType, String> delimiters);
    void SetMenuOptions(List<MenuOption> menuOptions);
    IMenu GetResult();
    void Reset();
}