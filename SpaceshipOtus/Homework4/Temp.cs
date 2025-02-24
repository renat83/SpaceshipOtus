
namespace SpaceshipOtus.Homework4
{
    public class ArithmeticUnit
    {
        internal void Run(char v, int operand)
        {
            throw new NotImplementedException();
        }
    }

    abstract public class Command
    {
        protected ArithmeticUnit unit;
        protected int operand;

        public abstract void Execute();
        public abstract void UnExecute();
    }

    public class Add : Command
    {
        public Add(ArithmeticUnit unit, int operand) 
        {
            this.unit = unit;
            this.operand = operand;
        }
        public override void Execute()
        {
            unit.Run('+', operand);
        }

        public override void UnExecute()
        {
            unit.Run('-', operand);
        }
    }
}
 