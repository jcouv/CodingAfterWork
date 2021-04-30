using System;
using System.Collections.Immutable;
using System.IO;
using System.Threading;

#region 6. top-level statements
Console.WriteLine(getString());

string getString()
{
    return "hello world";
}

// also works for async code
#endregion

class CMiscFeatures
{
    #region 1. default
    void M(CancellationToken c = default) // can be simplified
    {
    }
    #endregion

    #region 2. out variables
    private bool TryFind(int key, out ImmutableDictionary<string, string> result)
    {
        result = default; // can be simplified
        return false;
    }

    public void MOutVariable(int key)
    {
        // can be inlined and use `var`
        if (TryFind(key, out var data))
        {
            data.ToString();
        }
    }
    #endregion

    #region 3. discards
    public void MDiscards(int key)
    {
        if (TryFind(key, out _))
        {
        }

        ProcessNotifications((s, _, _) => Console.WriteLine(s)); // discards in lambda and return
    }

    private int ProcessNotifications(Action<string, int, int> a)
        => 0;
    #endregion

    #region 4. non-trailing argument names
    public void PrintName(string first, string last)
    {
        PrintName(first, middle: "", last);
    }

    private void PrintName(string first, string middle, string last)
    {
    }
    #endregion

    #region 5. using declarations
    public void MUsing(string path, byte[] data)
    {
        using var f = new FileStream(path, FileMode.Create, FileAccess.Write);
        // use resource
        f.Write(data, offset: 0, data.Length);
    }
    #endregion
}

