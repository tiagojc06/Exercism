public static class Darts
{
    public static int Score(double x, double y)
    {
        double distance = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

        // if (distance <= 1)
        //     return 10;
        // if (distance <= 5)
        //     return 5;
        // if (distance <= 10)
        //     return 1;
        //
        // return 0;

        return distance switch
        {
            <= 1 => 10,
            <= 5 => 5,
            <= 10 => 1,
            _ => 0
        };

        // if (x is >= -1 and <= 1 && y is >= -1 and <= 1)
        //     return 10;
        // if (x is >= -5 and <= 5 && y is >= -5 and <= 5)
        //     return 5;
        // if (x is >= -10 and <= 10 && y is >= -10 and <= 10)
        //     return 1;
        //
        // return 0;

        // return (x, y) switch
        // {
        //     (>= -1 and <= 1, >= -1 and <= 1) => 10,
        //     (>= -5 and <= 5, >= -5 and <= 5) => 5,
        //     (>= -10 and <= 10, >= -10 and <= 10) => 1,
        //     _ => 0
        // };
    }
}
