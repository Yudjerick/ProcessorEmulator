using ProcessorEmulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Commands
{
    public class CompareCommand: Command
    {
        public CompareCommand(uint binary)
        {
            data = binary;
        }

        public CompareCommand(uint operandOne, uint operandTwo)
        {
            Type = CommandType.CMP;
            OperandOne = operandOne;
            OperandTwo = operandTwo;
        }

        public override void Execute(Processor processor)
        {
            uint valueOne = processor.GetOperandValue(AddressingType.Register, OperandOne);
            uint valueTwo = processor.GetOperandValue(AddressingType.Register, OperandTwo);
            processor.SignFlag =  valueOne < valueTwo;
            processor.ZeroFlag = valueOne == valueTwo;
        }
    }
}
