class MenuBuilder : IMenuBuilder
{
    public MenuBuilder(IMenuFactory menuFactory)
    {
        this.MenuFactory = menuFactory;
        this.Menu = this.MenuFactory.Create();
    }

    private IMenu Menu { get; set; }
    private IMenuFactory MenuFactory { get; }

    public IMenu GetResult()
    {
        var result = this.Menu;
        this.Reset();
        return result;
    }
    public void Reset()
    {
        this.Menu = this.MenuFactory.Create();
    }
    public void SetMenuOptions(List<MenuOption> menuOptions)
    {
        this.Menu.MenuOptions = menuOptions;
    }

    public void SetDelimiters(Dictionary<DelimiterType, string> delimiters)
    {
        this.Menu.Delimiters = delimiters;
    }

    public void SetIntroMessage(string introMessage)
    {
        this.Menu.IntroMessage = introMessage;
    }

    public void SetPromptMessage(string promptMessage)
    {
        this.Menu.PromptMessage = promptMessage;
    }

    public void SetWrongOptionMessage(string wrongOptionMessage)
    {
        this.Menu.WrongOptionMessage = wrongOptionMessage;
    }
}