namespace SpaceshipOtus.Homework2
{
    public class Vector
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector Plus(Vector point, Vector vector)
        {
            return new Vector(point.X + vector.X, point.Y + vector.Y);
        }
    }

    public class Angle
    {
        public int d { get; set; } // Угол в градусах

        public Angle(int degrees)
        {
            d = degrees;
        }
    }
}
