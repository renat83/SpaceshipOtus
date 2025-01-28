namespace SpaceshipOtus.Drafts
{
    public class QuadraticEquationSolver
    {
        public double[] Solve(double a, double b, double c)
        {
            const double epsilon = 1e-10;

            if (Math.Abs(a) < epsilon)
            {
                throw new ArgumentException("Коэффициент 'a' не может быть равен нулю.");
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
