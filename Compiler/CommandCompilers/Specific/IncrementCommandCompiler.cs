using ProcessorEmulator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProcessorEmulator.Compiler.CommandCompilers.CommandCompiler;

namespace ProcessorEmulator.Compiler.CommandCompilers.Specific
{
    internal class IncrementCommandCompiler: CommandCompiler
    {
        public override CommandType CommandType => CommandType.INC;

        protected override List<List<AddressingType>> ExpectedOperandsTypes => new List<List<AddressingType>>
        {
            new List<AddressingType>
            {
                AddressingType.Register
            }
        };

        protected override Command GetSpecificCommand(List<ParsedOperand> operands)
        {
            return IncrementCommand.CreateFromOperand(operands[0].value);
        }
    }
}
