namespace SpaceshipOtus.Homework2
{
    public interface IUObject
    {
        object GetProperty(string key);
        object SetProperty(string key, object newValue);
    }

    public interface IMovingObject
    {
        Vector GetLocation();
        Vector GetVelocity();
        void SetLocation(Vector newValue);
    }

    public class Move
    {
        IMovingObject movingObject;

        public Move(IMovingObject obj)
        {
            movingObject = obj;
        }

        public void Execute()
        {
            var location = movingObject.GetLocation();
            var velocity = movingObject.GetVelocity();

            if (location == null)
                throw new InvalidOperationException("Location cannot be null.");

            if (velocity == null)
                throw new InvalidOperationException("Velocity cannot be null.");

            movingObject.SetLocation(Vector.Plus(location, velocity));
        }
    }

    public class MovingObjectAdapter : IMovingObject
    {
        IUObject obj;

        public MovingObjectAdapter(IUObject o)
        {
            obj = o;
        }

        public Vector GetLocation()
        {
            return (Vector)obj.GetProperty("Location");
        }

        public Vector GetVelocity()
        {
            return (Vector)obj.GetProperty("Velocity");
        }

        public void SetLocation(Vector newValue)
        {
            obj.SetProperty("Location", newValue);
        }
    }
}
