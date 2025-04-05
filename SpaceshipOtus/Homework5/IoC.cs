using SpaceshipOtus.Homework4;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace SpaceshipOtus.Homework5
{
    public static class IoC
    {
        // Хранилище скоупов
        private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, Func<object[], object>>> _scopes = new();

        // Текущий scope для каждого потока
        private static readonly ThreadLocal<string> _currentScope = new(() => "root");

        static IoC()
        {
            // Инициализация скоупа
            _scopes["root"] = new ConcurrentDictionary<string, Func<object[], object>>();

            // Регистрация команд
            Register("IoC.Register", args => new ActionCommand(() =>
                _scopes.GetOrAdd((string)args[1], _ => new ConcurrentDictionary<string, Func<object[], object>>())
                       [(string)args[0]] = (Func<object[], object>)args[2]));

            Register("Scopes.New", args => new ActionCommand(() =>
                _scopes.GetOrAdd((string)args[0], _ => new ConcurrentDictionary<string, Func<object[], object>>())));

            Register("Scopes.Current", args => new ActionCommand(() =>
                _currentScope.Value = (string)args[0]));
        }

        public static T Resolve<T>(string key, params object[] args)
        {
            // Пытаемся найти зависимость в текущем scope
            if (_scopes.TryGetValue(_currentScope.Value, out var scope) && scope.TryGetValue(key, out var factory))
                return (T)factory(args);

            // Если не нашли, пробуем в root scope
            if (_currentScope.Value != "root" && _scopes["root"].TryGetValue(key, out factory))
                return (T)factory(args);

            throw new InvalidOperationException($"Dependency {key} not found");
        }

        private static void Register(string key, Func<object[], object> factory, string scopeId = "root")
        {
            var scope = _scopes.GetOrAdd(scopeId, _ => new ConcurrentDictionary<string, Func<object[], object>>());
            scope[key] = factory;
        }

        private class ActionCommand : CommandAbstract
        {
            private readonly Action _action;
            public ActionCommand(Action action) => _action = action;
            public override void Execute() => _action();
        }
    }
}