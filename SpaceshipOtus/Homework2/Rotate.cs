namespace SpaceshipOtus.Homework2
{
    public interface IRotatingObject
    {
        int GetAngle();
        void SetAngle(int value);
        int GetAngularVelocity();
    }

    public class Rotate
    {
        IRotatingObject rotatingObject;

        public Rotate(IRotatingObject obj)
        {
            rotatingObject = obj;
        }

        public void Execute()
        {
            if (rotatingObject == null)
                throw new ArgumentNullException(nameof(rotatingObject));

            int currentAngle = rotatingObject.GetAngle();
            int angularVelocity = rotatingObject.GetAngularVelocity();

            rotatingObject.SetAngle(currentAngle + angularVelocity);
        }
    }

    public class RotatableAdapter : IRotatingObject
    {
        IUObject obj;

        public RotatableAdapter(IUObject o)
        {
            obj = o;
        }

        public int GetAngle()
        {
            return ((Angle)obj.GetProperty("Angle")).d;
        }

        public void SetAngle(int value)
        {
            obj.SetProperty("Angle", new Angle(value));
        }

        public int GetAngularVelocity()
        {
            return (int)obj.GetProperty("AngularVelocity");
        }
    }
}
