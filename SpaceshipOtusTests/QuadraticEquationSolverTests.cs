using Microsoft.AspNetCore.Routing;
using SpaceshipOtus.Drafts;

namespace SpaceshipOtusTests
{
    public class QuadraticEquationSolverTests
    {
        [Theory(DisplayName = "[TS-001]Solve - ��� ��������� x^2 + 1 = 0 ������ ��� (������������ ������ ������)")]
        [InlineData(1, 0, 1)]
        public void Solve_QuadraticEquation_ReturnEmptyArray(double a, double b, double c)
        {
            // Arrange
            var solver = new QuadraticEquationSolver();

            // Act
            var result = solver.Solve(a, b, c);

            // Assert
            Assert.Empty(result);
        }

        [Theory(DisplayName = "[TS-002]Solve - ��� ��������� x^2 - 1 = 0 ���� ��� ����� ��������� 1 (x1=1, x2=-1)")]
        [InlineData(1, 0, -1)]
        public void Solve_QuadraticEquation_ReturnTwoRootsMultiplicity(double a, double b, double c)
        {
            // Arrange
            var solver = new QuadraticEquationSolver();

            // Act
            var result = solver.Solve(a, b, c);

            // Assert
            Assert.Equal([1,-1], result);
        }

        [Theory(DisplayName = "[TS-003] Solve - ��� ��������� x^2 + 2x + 1 = 0 ���� ���� ������ ��������� 2 (x1=x2=-1)")]
        [InlineData(1, 2, 1)]
        public void Solve_QuadraticEquation_ReturnOneRootMultiplicityTwo(double a, double b, double c)
        {
            // Arrange
            var solver = new QuadraticEquationSolver();

            // Act
            var result = solver.Solve(a, b, c);

            // Assert
            Assert.Equal([-1, -1], result);
        }
    }
}