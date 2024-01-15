public class ConsoleMenuFactory : IMenuFactory
{
    public IMenu Create() => new ConsoleMenu();
}
