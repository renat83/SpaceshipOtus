using SpaceshipOtus.Homework2;

namespace SpaceshipOtusTests
{
    public class TestObject : IUObject
    {
        private Dictionary<string, object> _properties = new Dictionary<string, object>();

        public object GetProperty(string key)
        {
            if (!_properties.ContainsKey(key))
                throw new ArgumentException($"Property '{key}' is not set.");
            return _properties[key];
        }

        public object SetProperty(string key, object newValue)
        {
            _properties[key] = newValue;
            return newValue;
        }
    }

    public class TestMoveObjectWithSetLocationFailure : IUObject
    {
        private Dictionary<string, object> _properties = new Dictionary<string, object>();
        private bool _allowSetLocation = true;

        public object GetProperty(string key)
        {
            if (!_properties.ContainsKey(key))
                throw new ArgumentException($"Property '{key}' is not set.");
            return _properties[key];
        }

        public object SetProperty(string key, object newValue)
        {
            if (key == "Location" && !_allowSetLocation)
                throw new ArgumentException("Location cannot be set.");

            _properties[key] = newValue;
            return newValue;
        }

        public void DisallowSettingLocation()
        {
            _allowSetLocation = false;
        }
    }

    public class TestRotateObjectWithSetAngleFailure : IUObject
    {
        private Dictionary<string, object> _properties = new Dictionary<string, object>();
        private bool _allowSetAngle = true;

        public object GetProperty(string key)
        {
            if (!_properties.ContainsKey(key))
                throw new ArgumentException($"Property '{key}' is not set.");
            return _properties[key];
        }

        public object SetProperty(string key, object newValue)
        {
            if (key == "Angle" && !_allowSetAngle)
                throw new ArgumentException("Angle cannot be set.");

            _properties[key] = newValue;
            return newValue;
        }

        public void DisallowSettingAngle()
        {
            _allowSetAngle = false;
        }
    }
}
