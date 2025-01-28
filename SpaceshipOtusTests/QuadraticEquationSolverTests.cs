using Microsoft.AspNetCore.Routing;
using SpaceshipOtus.Drafts;

namespace SpaceshipOtusTests
{
    public class QuadraticEquationSolverTests
    {
        [Theory(DisplayName = "[001]Equation (x^2 + 1 = 0) - There are no roots (empty array is returned)")]
        [InlineData(1, 0, 1)] //для уравнения x^2+1 = 0 корней нет (возвращается пустой массив)
        public void Solve_QuadraticEquation_ReturnEmptyArray(double a, double b, double c)
        {
            // Arrange
            var solver = new QuadraticEquationSolver();

            // Act
            var result = solver.Solve(a, b, c);

            // Assert
            Assert.Empty(result);
        }

        [Theory(DisplayName = "[002]Equation (x^2 - 1 = 0) - There are two roots (x1=1, x2=-1)")]
        [InlineData(1, 0, -1)] //для уравнения x^2-1 = 0 есть два корня кратности 1 (x1=1, x2=-1)
        public void Solve_QuadraticEquation_ReturnTwoRootsMultiplicity(double a, double b, double c)
        {
            // Arrange
            var solver = new QuadraticEquationSolver();

            // Act
            var result = solver.Solve(a, b, c);

            // Assert
            Assert.Equal([1,-1], result);
        }

        [Theory(DisplayName = "[003]Equation (x^2 + 2x + 1 = 0) - There is one root (x1=x2=-1)")]
        [InlineData(1, 2, 1)] //для x^2+2x+1=0 есть один корень кратности 2 (x1= x2 = -1)
        [InlineData(1, 2, 1 - 1e-11)] //дискриминант отличный от нуля, но меньше заданного эпсилон
        public void Solve_QuadraticEquation_ReturnOneRootMultiplicityTwo(double a, double b, double c)
        {
            // Arrange
            var solver = new QuadraticEquationSolver();

            // Act
            var result = solver.Solve(a, b, c);

            // Assert
            Assert.Equal([-1, -1], result);
        }

        [Theory(DisplayName = "[004]Parametr 'a' a close to zero - exception is thrown")]
        [InlineData(1e-12, 2, 3)] // a близко к нулю Solve выбрасывает исключение
        public void Solve_ParametrAIsZero_ThrowsException(double a, double b, double c)
        {
            // Arrange
            var solver = new QuadraticEquationSolver();

            // Act
            var exception = Assert.Throws<ArgumentException>(() => solver.Solve(a, b, c));

            //Assert
            Assert.Equal("Parametr 'a' cannot be equal to zero", exception.Message);
        }

        [Theory(DisplayName = "[005]Parametr is NaN - exception is thrown")]
        [InlineData(double.NaN, 1, 1)] // a = NaN
        [InlineData(1, double.NaN, 1)] // b = NaN
        [InlineData(1, 1, double.NaN)] // c = NaN
        public void Solve_ParametrIsNaN_ThrowsException(double a, double b, double c)
        {
            // Arrange
            var solver = new QuadraticEquationSolver();

            // Act
            var exception = Assert.Throws<ArgumentException>(() => solver.Solve(a, b, c));

            // Assert
            Assert.Equal("Parametrs cannot be NaN", exception.Message);
        }

        [Theory(DisplayName = "[006]Parametr is Infinity - exception is thrown")]
        [InlineData(double.PositiveInfinity, 1, 1)] // a = +Infinity
        [InlineData(1, double.PositiveInfinity, 1)] // b = +Infinity
        [InlineData(1, 1, double.PositiveInfinity)] // c = +Infinity
        [InlineData(double.NegativeInfinity, 1, 1)] // a = -Infinity
        [InlineData(1, double.NegativeInfinity, 1)] // b = -Infinity
        [InlineData(1, 1, double.NegativeInfinity)] // c = -Infinity
        public void Solve_ParametrIsInfinity_ThrowsException(double a, double b, double c)
        {
            // Arrange
            var solver = new QuadraticEquationSolver();

            // Act
            var exception = Assert.Throws<ArgumentException>(() => solver.Solve(a, b, c));

            // Assert
            Assert.Equal("Parametrs cannot be Infinity", exception.Message);
        }
    }
}