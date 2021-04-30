using System;
using System.Collections.Generic;
using System.Linq;

class CLocalFunctions
{
    #region 1. captures, static, shadowing
    public List<int> MLocalFunctions(IEnumerable<int> input)
    {
        List<int> found = null;
        foreach (var item in input)
        {
            if (isInteresting(item))
            {
                getOrInitializeList().Add(item);
            }
        }

        return found;

        List<int> getOrInitializeList() // capture
        {
            found ??= new List<int>(); // ??=
            return found;
        }

        static bool isInteresting(int input) // shadowing, static
        {
            return input > 0;
        }
    }
    #endregion

    #region 2. conversions to delegate type
    public void MConversions(IEnumerable<int> input)
    {
        _ = input.Select(isInteresting);
        // Enumerable.Select(input, new Func<int, bool>(<M2>g__isInteresting|0_0));

        static bool isInteresting(int input)
        {
            return input > 0;
        }
    }
    #endregion
}
