using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


BuildMainConsoleMenu().Execute();


IMenu BuildMainConsoleMenu()
{
    IMenuFactory consoleMenuFactory = new ConsoleMenuFactory();
    IMenuBuilder mainConsoleMenuBuilder = new MenuBuilder(consoleMenuFactory);
    mainConsoleMenuBuilder.SetPromptMessage("Input: ");
    mainConsoleMenuBuilder.SetIntroMessage("Choose option from the menu below:");
    mainConsoleMenuBuilder.SetWrongOptionMessage("Invalid option! Please, try again");
    mainConsoleMenuBuilder.SetMenuOptions(GetMainConsoleMenuOptions());
    return mainConsoleMenuBuilder.GetResult();
}


IMenu BuildCalculatorOperatorConsoleMenu()
{
    List<MenuOption> operatorMenuOptions = Enum.GetNames<OperatorType>().ToList().Select(operationName => new MenuOption() { description = operationName, handler = () => operationName }).ToList();
    IMenuFactory menuFactory = new ConsoleMenuFactory();
    IMenuBuilder menuBuilder = new MenuBuilder(menuFactory);
    menuBuilder.SetWrongOptionMessage("Invalid operation! Try again");
    menuBuilder.SetIntroMessage("Choose operation type from the menu below:");
    menuBuilder.SetMenuOptions(operatorMenuOptions);
    return menuBuilder.GetResult();
}

T? ParseValueUntilCorrect<T>(Func<T> readDelegate, Predicate<T> predicate, String errorMessage)
{
    var isParseSuccess = false;
    var value = default(T);

    while (!isParseSuccess)
    {
        try
        {
            value = readDelegate();
            isParseSuccess = predicate(value);
        }
        catch
        {
            isParseSuccess = false;
        }
        if (!isParseSuccess)
        {
            Console.WriteLine(errorMessage);
        }
    }
    return value;

}

void PrintSubstring(String str)
{
    Console.Write($"Input length (max. {str.Length}): ");
    var length = ParseValueUntilCorrect(() => Int32.Parse(Console.ReadLine()), (index) => index >= 0 && index <= str.Length, "Invalid length! Try again");
    Console.WriteLine("Printing substring...");
    Console.WriteLine(str.Substring(0, length));
};

String ReadString()
{
    Console.WriteLine("Input file path to read:");
    var path = ParseValueUntilCorrect(Console.ReadLine, (path) => File.Exists(path), "Invalid path! Try again");
    return File.ReadAllText(path);
};

OperatorType ChooseOperator()
{
    var menuResult = BuildCalculatorOperatorConsoleMenu().Execute();
    if (Enum.TryParse<OperatorType>(menuResult.ToString(), out OperatorType chosenOperator))
    {
        return chosenOperator;
    }
    Console.WriteLine("Invalid operator");
    throw new InvalidOperationException("Invalid operator");
}

Double CalculateResult(Double firstOperand, OperatorType operatorType, Double? secondOperand)
{
    if (operatorType != OperatorType.Increment && operatorType != OperatorType.Decrement && !secondOperand.HasValue)
    {
        throw new ArgumentNullException("For this operation, second operand cannot be null");
    }
    switch (operatorType)
    {
        case OperatorType.Increment: return firstOperand + 1;
        case OperatorType.Decrement: return firstOperand - 1;
        case OperatorType.Plus: return firstOperand + secondOperand ?? 0;
        case OperatorType.Minus: return firstOperand - secondOperand ?? 0;
        case OperatorType.Multiply: return firstOperand * secondOperand ?? 0;
        case OperatorType.Divide: return firstOperand / secondOperand ?? 0;
        case OperatorType.Modulo: return firstOperand % secondOperand ?? 0;
        case OperatorType.Power: return Math.Pow(firstOperand, secondOperand ?? 0);
        default:
            throw new InvalidOperationException("Invalid operation");
    }
}


List<MenuOption> GetMainConsoleMenuOptions()
{
    Func<Object> printStringOption = () =>
    {
        var str = ReadString();
        PrintSubstring(str);
        return String.Empty;
    };
    Func<Object> calculateOption = () =>
    {
        Console.WriteLine("Input first operand number: ");
        var firstOperand = ParseValueUntilCorrect(() => Double.Parse(Console.ReadLine()), (rawNumber) => true, "Invalid number! Try again");
        Double? secondOperand = null;
        var chosenOperator = ChooseOperator();
        if (chosenOperator != OperatorType.Increment && chosenOperator != OperatorType.Decrement)
        {
            Console.WriteLine("Input second operand number: ");
            secondOperand = ParseValueUntilCorrect(() => Double.Parse(Console.ReadLine()), (rawNumber) => true, "Invalid number! Try again");
        }
        Console.Write("Result: ");
        var result = CalculateResult(firstOperand, chosenOperator, secondOperand);
        Console.WriteLine(result);
        return result;
    };
    var options = new List<MenuOption>();
    options.Add(new MenuOption { description = "Choose substring of 'Lorem Ipsum'", handler = printStringOption });
    options.Add(new MenuOption { description = "Use calculator", handler = calculateOption });
    return options;
}
