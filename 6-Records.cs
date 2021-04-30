using System;

namespace Records
{
    record Point(int X, int Y)
    {
        public int Distance(Point other) 
            => throw new NotImplementedException();
    }

    #region generated API surface
    class C
    {
        public void M()
        {
            var point = new Point(1, 2); // construction
            point.X.ToString();
            point.Y.ToString();

            var (x, y) = point; // deconstruction

            var point2 = new Point(3, 4);
            if (point == point2) // equality
            {
            }
        }
    }
    #endregion
}
