using SpaceshipOtus.Homework2;

namespace SpaceshipOtus.Homework4
{
    abstract public class CommandAbstract
    {
        public abstract void Execute();
    }

    public class MoveCommand : CommandAbstract
    {
        private readonly IUObject _uObject;

        public MoveCommand(IUObject uObject)
        {
            _uObject = uObject;
        }

        public override void Execute()
        {
            Vector location = (Vector)_uObject.GetProperty("Location");
            Vector velocity = (Vector)_uObject.GetProperty("Velocity");

            if (location == null)
                throw new InvalidOperationException("Location cannot be null.");

            if (velocity == null)
                throw new InvalidOperationException("Velocity cannot be null.");
        }
    }

    public class RotateCommand : CommandAbstract
    {
        private readonly IUObject _uObject;

        public RotateCommand(IUObject uObject)
        {
            _uObject = uObject;
        }

        public override void Execute()
        {
            int angle = (int)_uObject.GetProperty("Angle");
            int angularVelocity = (int)_uObject.GetProperty("AngularVelocity");
        }
    }

    public class CheckFuelCommand : CommandAbstract
    {
        private readonly IUObject _uObject;

        public CheckFuelCommand(IUObject uObject)
        {
            _uObject = uObject;
        }

        public override void Execute()
        {
            int fuel = (int)_uObject.GetProperty("Fuel");
            int fuelConsumption = (int)_uObject.GetProperty("FuelConsumption");

            if (fuel < fuelConsumption)
                throw new CommandException("Not enough fuel to perform the operation.");
        }
    }

    public class BurnFuelCommand : CommandAbstract
    {
        private readonly IUObject _uObject;

        public BurnFuelCommand(IUObject uObject)
        {
            _uObject = uObject;
        }

        public override void Execute()
        {
            int fuel = (int)_uObject.GetProperty("Fuel");
            int fuelConsumption = (int)_uObject.GetProperty("FuelConsumption");

            _uObject.SetProperty("Fuel", fuel - fuelConsumption);
        }
    }

    public class ChangeVelocityCommand : CommandAbstract
    {
        private readonly IUObject _uObject;

        public ChangeVelocityCommand(IUObject uObject)
        {
            _uObject = uObject;
        }

        public override void Execute()
        {
            Vector velocity = (Vector)_uObject.GetProperty("Velocity");
            int angle = ((Angle)_uObject.GetProperty("Angle")).d;

            // Пример изменения скорости на основе угла поворота
            velocity.X = (int)(velocity.X * Math.Cos(angle * Math.PI / 180));
            velocity.Y = (int)(velocity.Y * Math.Sin(angle * Math.PI / 180));

            _uObject.SetProperty("Velocity", velocity);
        }
    }
}
