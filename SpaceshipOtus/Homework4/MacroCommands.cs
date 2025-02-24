using SpaceshipOtus.Homework2;

namespace SpaceshipOtus.Homework4
{
    public class MacroCommands : CommandAbstract
    {
        private readonly CommandAbstract[] _commands;

        public MacroCommands(CommandAbstract[] commands)
        {
            _commands = commands;
        }

        public override void Execute()
        {
            foreach (var command in _commands)
            {
                try
                {
                    command.Execute();
                }
                catch (Exception)
                {
                    throw new CommandException("MacroCommand execution failed.");
                }
            }
        }
    }

    public class MoveWithFuelCommand : MacroCommands
    {
        public MoveWithFuelCommand(IUObject uObject)
            : base([
                new CheckFuelCommand(uObject), 
                new MoveCommand(uObject), 
                new BurnFuelCommand(uObject)
                ])
        {

        }
    }

    public class RotateWithVelocityCommand : MacroCommands
    {
        public RotateWithVelocityCommand(IUObject uObject)
            : base([
                new RotateCommand(uObject),
                new ChangeVelocityCommand(uObject)
                ])
        {
        }
    }
}
