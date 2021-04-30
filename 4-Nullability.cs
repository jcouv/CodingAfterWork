using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

class CNullability : Base
{
    #region 1. existing code
    public string Normalize(string input)
    {
        throw new NotImplementedException();
    }
    #endregion

    // string may be null: string?
    // string may not be null
    // string

    #region 2. annotated code
#nullable enable
    public string Normalize2(string? input)
    {
        // <TreatWarningsAsErrors>nullable</...
        if (input is null)
        {
            return string.Empty;
        }
        return input.Trim();
    }

    public string Legacy()
    {
        string s = Normalize(null);
        return s;
    }
    #endregion

    #region 3. null tests and state updates
    public void MState(object? o, Person p)
    {
        if (o is null)
            throw new Exception();
        o.ToString();

        if (p is { MiddleName: not null })
        {
            _ = p.MiddleName.ToString(); // philosophy
        }

        Person unknownPerson = default;
        string firstName = unknownPerson.FirstName; // assignment warning, default state
        _ = firstName.ToString();

        var maybeLaterNull = ""; // var is special
        maybeLaterNull.ToString();
        maybeLaterNull = null;
        maybeLaterNull.ToString();

        var people = new Person[10]; // gap
        string s = people[0].FirstName;
    }
    #endregion

    #region 4. assertions and suppression
    public void MAssertions(Person p)
    {
        Debug.Assert(p.HasMiddleName);
        {
            //Debug.Assert(p.MiddleName != null);
            _ = p.MiddleName!.ToString(); // suppression
        }

        // attributes (MemberNotNullIf("MiddleName", true))
    }
    #endregion

    #region 5. generics
    public void MGenerics(List<string> list, Dictionary<int, string> dictionary)
    {
        list.Add(null);
        foreach (var item in list)
        {
            item.ToString();
        }

        dictionary.Add(0, null);
        dictionary[0].ToString();

        if (dictionary.TryGetValue(0, out string? s))
        {
            _ = s.ToString();
            // attributes (MaybeNullWhen)
        }
    }
    #endregion

    #region 6. attributes
    public void Initialize([NotNull] ref string? s)
    {
        s = null;
        s = string.Empty;
        return;
    }

    public void CallInitialize()
    {
        string? s = null;
        Initialize(ref s);
        s.ToString();
    }
    #endregion

    #region 6. attributes (part 2)
    public void PossiblyConsume([DisallowNull] ref string? s)
    {
        s.ToString();
        s = null;
    }

    public void CallPossiblyConsume()
    {
        string? maybeNull = "";
        PossiblyConsume(ref maybeNull);
    }
    #endregion

    #region 7. constraints
    public void MConstraint1<T>(T? input, T input2) where T : class
    {
        input.ToString();
    }

    public void MConstraint2<T>(T input) where T : class?
    {
        input.ToString();
    }

    public void MConstraint3<T>(T input) where T : Base
    {
        input.ToString();
    }

    public void MConstraint4<T>(T input) where T : notnull
    {
        input.ToString();
    }
    #endregion

    #region 7. T? and overrides
    //virtual string MOverride<T>(T? t)
    public override string MOverride<T>(T? t) where T : default
    {
        return "";
    }

    public void MStruct<TStruct>(TStruct? t) where TStruct : struct
    {
        if (t.HasValue)
        {
            TStruct value = t.Value;
        }
    }
    #endregion

    #region helpers
    public struct Person
    {
        public string FirstName;
        public string? MiddleName;
        public string LastName;

        public bool HasMiddleName;
    }
    #endregion
}
class Base
{
    public virtual string MOverride<T>(T? t) { return ""; }
    // M<int> int int?=System.Nullable<int>
    // M<string> string?
    // M<string?> string?
}
