using ProcessorEmulator;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Commands
{
    internal class DecrementCommand: Command
    {
        public DecrementCommand(uint binary)
        {
            data = binary;
        }

        public static DecrementCommand CreateFromOperand(uint register)
        {
            DecrementCommand command = new DecrementCommand(0u);
            command.Type = CommandType.DEC;
            command.OperandOne = register;
            return command;
        }

        public override void Execute(Processor processor)
        {
            int value = (int)processor.GetOperandValue(AddressingType.Register, OperandOne);
            value--;
            processor.SetValueToOperand(AddressingType.Register, OperandOne, (uint)value);
            processor.ZeroFlag = value == 0;
            processor.SignFlag = value < 0;
        }
    }
}
