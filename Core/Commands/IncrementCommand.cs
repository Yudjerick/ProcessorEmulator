using ProcessorEmulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Commands
{
    internal class IncrementCommand: Command
    {
        public IncrementCommand(uint binary)
        {
            data = binary;
        }

        public static IncrementCommand CreateFromOperand(uint register)
        {
            IncrementCommand command = new IncrementCommand(0u);
            command.Type = CommandType.INC;
            command.OperandOne = register;
            return command;
        }

        public override void Execute(Processor processor)
        {
            uint value = processor.GetOperandValue(AddressingType.Register, OperandOne);
            value++;
            processor.SetValueToOperand(AddressingType.Register, OperandOne, value);
            processor.ZeroFlag = value == 0;
            processor.SignFlag = value < 0;
        }
    }
}
