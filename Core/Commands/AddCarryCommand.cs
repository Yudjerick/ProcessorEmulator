using ProcessorEmulator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Core.Commands
{
    internal class AddCarryCommand: Command
    {
        public AddCarryCommand(uint binary)
        {
            data = binary;
        }

        public AddCarryCommand(uint operandOne, uint operandTwo)
        {
            Type = CommandType.ADC;
            OperandOne = operandOne;
            OperandTwo = operandTwo;
        }

        public override void Execute(Processor processor)
        {
            ulong valueOne = (uint)processor.GetOperandValue(AddressingType.Register, OperandOne);
            ulong valueTwo = (uint)processor.GetOperandValue(AddressingType.Register, OperandTwo);
            ulong sum = valueOne + valueTwo;
            if (processor.CarryFlag)
            {
                sum++;
            }
            if (sum > uint.MaxValue)
            {
                processor.CarryFlag = true;
            }
            processor.SetValueToOperand(AddressingType.Register, OperandOne, (uint)sum);
            processor.SignFlag = (int)sum < 0;
            processor.ZeroFlag = sum == 0;
        }
    }
}
