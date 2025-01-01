namespace GameRules.Math
{
    internal readonly struct QuadraticFunction
    {
        private readonly double _a, _b, _c;

        internal QuadraticFunction(double a, double b, double c)
        {
            _a = a;
            _b = b;
            _c = c;
        }

        internal double Calculate(double x)
        {
            return (_a * System.Math.Pow(x, 2)) + (_b * x) + _c;
        }
    }
}
