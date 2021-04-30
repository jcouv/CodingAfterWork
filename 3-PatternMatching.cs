using System;

class CPatterns
{
    #region 1. type pattern
    public void MTypeTest(Animal a)
    {
        switch (a)
        {
            case Dog:
                Console.WriteLine("dog");
                break;
            case Cat:
                Console.WriteLine("cat");
                break;
            default:
                Console.WriteLine("other");
                break;
        }
    }
    #endregion

    #region 2. constant pattern
    public void MConstant(object o)
    {
        bool b = o is 1;
        bool b2 = o is "hello";
        bool b3 = o is null;
    }
    #endregion

    #region 3. recursive pattern
    public void MRecursiveTest(object o)
    {
        var point = new Point(1, 2);
        bool b = point is (1, 2); // positional pattern with two constant patterns
        // public void Deconstruct(out int x, out int y)

        var person = new Person { FirstName = "Jessica", LastName = "Engstrom" };
        bool b2 = person is { FirstName: "", LastName: "" }; // property pattern

        if (o is Person jimmy) // property pattern with type and declaration
        {
            _ = jimmy.ToString();
        }

        if (person is { FirstName: var first, LastName: "Engstrom" }) // property pattern with nested declaration
        {
            _ = first.ToString();
        }

        if (point is (int x, 2)) // positional pattern with declaration pattern
        {
            _ = x.ToString();
        }

        // Syntax: Type? (...)? { ... }? identifier?
    }
    #endregion

    #region 4. relational pattern
    public void MRelational(int i)
    {
        switch (i)
        {
            case < 10:
                Console.WriteLine("small");
                break;
            case >= 10:
                Console.WriteLine("big");
                break;
        }
    }
    #endregion

    #region 5. combinators
    public void MCombinators(int i, object o, Point point)
    {
        if (i is > 0 and < 10)
        {
        }

        if (i is < 0 or > 10)
        {
        }

        bool b = o is not null;

        if (point is ( > 0, > 0))
        {
            // top-right quadrant
        }
    }
    #endregion

    #region 6. switch expression
    public void MSwitchExpression(int i, int x, int y, Point point)
    {
        // switch expression, exhaustiveness
        var label = i switch
        {
            < 10 => "small",
            > 10 => "big",
            //_ => "ten"
        };

        Console.WriteLine(label);

        var quadrant = (x, y) switch
        {
            ( >= 0, >= 0) => "top-right",
            ( >= 0, < 0) => "bottom-right",
            ( < 0, >= 0) => "top-left",
            ( < 0, < 0) => "bottom-left",
        };

        Console.WriteLine(quadrant);

        _ = point switch
        {
            ( >= 0, >= 0) => "top-right",
            ( >= 0, < 0) => "bottom-right",
            ( < 0, >= 0) => "top-left",
            ( < 0, < 0) => "bottom-left",
        };
    }

    #endregion
}

#region helpers
class Animal { }
class Dog : Animal { }
class Cat : Animal { }
class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }
}

class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
#endregion
