using ProcessorEmulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Commands
{
    public class StoreCommand: Command
    {
        public StoreCommand(uint binary)
        {
            data = binary;
        }

        public StoreCommand(
            AddressingType addressingTwo,
            uint operandOne,
            uint operandTwo
        )
        {
            Type = CommandType.STORE;
            AddressingTwo = addressingTwo;
            OperandOne = operandOne;
            OperandTwo = operandTwo;
        }

        public override void Execute(Processor processor)
        {
            uint value = (uint)processor.GetOperandValue(AddressingType.Register, OperandOne);
            processor.SetValueToOperand(AddressingTwo, OperandTwo, value);
        }
    }
}
