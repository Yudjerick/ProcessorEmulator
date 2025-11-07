using ProcessorEmulator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Core
{
    internal class MultiplyCommand: Command
    {
        public MultiplyCommand(uint binary)
        {
            data = binary;
        }

        public static MultiplyCommand CreateFromOperand(uint register)
        {
            MultiplyCommand command = new MultiplyCommand(0u);
            command.Type = CommandType.MUL;
            command.OperandOne = register;
            return command;
        }

        public override void Execute(Processor processor)
        {
            uint valueOne = (uint)processor.GetOperandValue(AddressingType.Register, OperandOne);
            ulong result = valueOne * processor.GetLong(0);
            processor.SetValueToOperand(AddressingType.Register, 0, (uint)result);
            processor.SetValueToOperand(AddressingType.Register, 1, (uint)(result >> 32));
            //processor.SignFlag = sum < 0;
            processor.ZeroFlag = result == 0;
        }
    }
}
