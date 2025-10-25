using ProcessorEmulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Commands
{
    public class LoadCommand : Command
    {
        public LoadCommand(uint binary)
        {
            data = binary;
        }

        public LoadCommand(
            AddressingType addressingTwo,
            uint operandOne,
            uint operandTwo
        )
        {
            Type = CommandType.LOAD;
            AddressingTwo = addressingTwo;
            OperandOne = operandOne;
            OperandTwo = operandTwo;
        }

        public override void Execute(Processor processor)
        {
            base.Execute(processor);
            uint value = (uint)processor.GetOperandValue(AddressingTwo, OperandTwo);
            processor.SetValueToOperand(AddressingType.Register, OperandOne, value);
        }
    }
}
