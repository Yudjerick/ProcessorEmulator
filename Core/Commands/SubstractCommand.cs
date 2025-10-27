using ProcessorEmulator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Core
{
    internal class SubstractCommand: Command
    {
        public SubstractCommand(uint binary)
        {
            data = binary;
        }

        public SubstractCommand(uint operandOne, uint operandTwo)
        {
            Type = CommandType.SUB;
            OperandOne = operandOne;
            OperandTwo = operandTwo;
        }

        public override void Execute(Processor processor)
        {
            int valueOne = (int)processor.GetOperandValue(AddressingType.Register, OperandOne);
            int valueTwo = (int)processor.GetOperandValue(AddressingType.Register, OperandTwo);
            int sum = valueOne - valueTwo;
            processor.SetValueToOperand(AddressingType.Register, OperandOne, (uint)sum);
            processor.SignFlag = sum < 0;
            processor.ZeroFlag = sum == 0;
        }
    }
}
