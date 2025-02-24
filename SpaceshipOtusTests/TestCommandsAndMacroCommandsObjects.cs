using SpaceshipOtus.Homework2;

namespace SpaceshipOtusTests
{
    public class TestCommandsAndMacroCommandsObjects
    {
        public class MoveMockUObject : IUObject
        {
            public object GetProperty(string key) => key switch
            {
                "Location" => new Vector(10, 20),
                "Velocity" => new Vector(5, 5),
                _ => throw new ArgumentException("Invalid key")
            };

            public object SetProperty(string key, object newValue) => throw new NotImplementedException();
        }

        public class RotateMockUObject : IUObject
        {
            public object GetProperty(string key) => key switch
            {
                "Angle" => 45,
                "AngularVelocity" => 10,
                _ => throw new ArgumentException("Invalid key")
            };

            public object SetProperty(string key, object newValue) => throw new NotImplementedException();
        }

        public class CheckFuelMockUObject : IUObject
        {
            private int _fuel = 100; 
            private int _fuelConsumption = 50;

            public object GetProperty(string key) => key switch
            {
                "Fuel" => _fuel,
                "FuelConsumption" => _fuelConsumption,
                _ => throw new ArgumentException("Invalid key")
            };

            public object SetProperty(string key, object newValue)
            {
                switch (key)
                {
                    case "Fuel":
                        _fuel = (int)newValue;
                        break;
                    case "FuelConsumption":
                        _fuelConsumption = (int)newValue;
                        break;
                    default:
                        throw new ArgumentException("Invalid key");
                }

                return null;
            }
        }


        public class BurnFuelMockUObject : IUObject
        {
            private int _fuel = 100;

            public object GetProperty(string key) => key switch
            {
                "Fuel" => _fuel,
                "FuelConsumption" => 50,
                _ => throw new ArgumentException("Invalid key")
            };

            public object SetProperty(string key, object newValue)
            {
                if (key == "Fuel")
                    _fuel = (int)newValue;
                return null;
            }
        }

        public class ChangeVelocityMockUObject : IUObject
        {
            private Vector _velocity = new Vector(10, 0);
            private Angle _angle = new Angle(45);

            public object GetProperty(string key) => key switch
            {
                "Velocity" => _velocity,
                "Angle" => _angle,
                _ => throw new ArgumentException("Invalid key")
            };

            public object SetProperty(string key, object newValue)
            {
                switch (key)
                {
                    case "Velocity":
                        _velocity = (Vector)newValue;
                        break;
                    case "Angle":
                        _angle = (Angle)newValue;
                        break;
                    default:
                        throw new ArgumentException("Invalid key");
                }

                return null;
            }
        }
    }
}
