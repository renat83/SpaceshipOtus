namespace SpaceshipOtus.Drafts
{
    public class QuadraticEquationSolver
    {
        public double[] Solve(double a, double b, double c)
        {
            const double epsilon = 1e-10;

            if (double.IsNaN(a) || double.IsNaN(b) || double.IsNaN(c))
            {
                throw new ArgumentException("Parametrs cannot be NaN");
            }

            if (double.IsInfinity(a) || double.IsInfinity(b) || double.IsInfinity(c))
            {
                throw new ArgumentException("Parametrs cannot be Infinity");
            }

            if (Math.Abs(a) < epsilon)
            {
                throw new ArgumentException("Parametr 'a' cannot be equal to zero");
            }

            double discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                return [];
            }
            else if (Math.Abs(discriminant) < epsilon)
            {
                double root = -b / (2 * a);
                return [root, root];
            }
            else
            {
                double sqrtDiscriminant = Math.Sqrt(discriminant);
                double x1 = (-b + sqrtDiscriminant) / (2 * a);
                double x2 = (-b - sqrtDiscriminant) / (2 * a);

                return [x1, x2];
            }
        }
    }
}
