using SpaceshipOtus.Homework2;

namespace SpaceshipOtusTests
{
    public class TestCommandsAndMacroCommandsObjects
    {
        public class MoveMockUObject : IUObject
        {
            private readonly Dictionary<string, object> _properties = new();

            public object GetProperty(string key) => key switch
            {
                "Location" => _properties.TryGetValue(key, out var location) ? location : new Vector(10, 20),
                "Velocity" => _properties.TryGetValue(key, out var velocity) ? velocity : new Vector(5, 5),
                _ => throw new ArgumentException("Invalid key")
            };

            public object SetProperty(string key, object newValue)
            {
                _properties[key] = newValue;
                return null;
            }
        }

        public class RotateMockUObject : IUObject
        {
            private readonly Dictionary<string, object> _properties = new();

            public object GetProperty(string key) => key switch
            {
                "Angle" => _properties.TryGetValue(key, out var angle) ? angle : 45,
                "AngularVelocity" => _properties.TryGetValue(key, out var angularVelocity) ? angularVelocity : 10,
                _ => throw new ArgumentException("Invalid key")
            };

            public object SetProperty(string key, object newValue)
            {
                _properties[key] = newValue;
                return null;
            }
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

        public class ChangeVelocityMockUObject : IUObject
        {
            private Vector _velocity = new Vector(10, 0);
            private int _angle = 45;

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
                        _angle = (int)newValue;
                        break;
                    default:
                        throw new ArgumentException("Invalid key");
                }

                return null;
            }
        }

        public class UniversalMockUObject : IUObject
        {
            private readonly Dictionary<string, object> _properties = new();

            public object GetProperty(string key)
            {
                if (_properties.TryGetValue(key, out var value))
                    return value;

                // Возвращаем значения по умолчанию для отсутствующих свойств
                return key switch
                {
                    "Location" => new Vector(10, 20),
                    "Velocity" => new Vector(5, 5),
                    "Angle" => _properties.TryGetValue(key, out var angle) ? angle : 45,
                    "AngularVelocity" => _properties.TryGetValue(key, out var angularVelocity) ? angularVelocity : 10,
                    "Fuel" => 100,
                    "FuelConsumption" => 50,
                    _ => throw new ArgumentException("Invalid key")
                };
            }

            public object SetProperty(string key, object newValue)
            {
                _properties[key] = newValue;
                return null;
            }
        }
    }
}
