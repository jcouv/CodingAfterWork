using System.Collections.Generic;

class CTuples
{
    public int MSingle(int i1, int i2)
    {
        return 0;
    }

    public (int, int) MMultiple(int i1, int i2)
    {
        return (0, 1);
    }

    public void MCaller()
    {
        (int, int) t = MMultiple(0, 0);
        t.Item2.ToString();

        var t2 = MMultiple(0, 0);
        t2.Item1.ToString();

        (int first, int second) t3 = MMultiple(0, 0); // named tuple
        t3.second.ToString();
    }

    public (string firstName, string lastName) GetName()
    {
        return (firstName: "Julien", lastName: "Couvreur");
    }

    #region deconstruction
    public void MDeconstruction()
    {
        (var first, var second) = MMultiple(0, 0); // typed deconstruction
        first.ToString();
        second.ToString();

        var (one, two) = MMultiple(0, 0); // var deconstruction
        one.ToString();
        two.ToString();

        var list = new List<(int, string)>();
        foreach (var (i, s) in list) // deconstruction in foreach
        {
            i.ToString();
            s.ToString();
        }
    }
    #endregion
}
