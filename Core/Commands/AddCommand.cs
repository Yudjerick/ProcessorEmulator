using ProcessorEmulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Commands
{
    public class AddCommand : Command
    {
        public AddCommand(uint binary)
        {
            data = binary;
        }

        public AddCommand(uint operandOne, uint operandTwo)
        {
            Type = CommandType.ADD;
            OperandOne = operandOne;
            OperandTwo = operandTwo;
        }

        public override void Execute(Processor processor)
        {
            uint valueOne = processor.GetOperandValue(AddressingType.Register, OperandOne);
            uint valueTwo = processor.GetOperandValue(AddressingType.Register, OperandTwo);
            uint sum = valueOne + valueTwo;
            processor.SetValueToOperand(AddressingType.Register, OperandOne, sum);
            processor.SignFlag = sum < 0;
            processor.ZeroFlag = sum == 0;
        }
    }
}
