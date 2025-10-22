using ProcessorEmulator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Compiler.CommandCompilers.Specific
{
    internal class JumpLessCommandCompiler: CommandCompiler
    {
        public override CommandType CommandType => CommandType.JL;

        protected override List<List<AddressingType>> ExpectedOperandsTypes => new List<List<AddressingType>>
        {
            new List<AddressingType>
            {
                AddressingType.Immediate
            }
        };

        protected override Command GetSpecificCommand(List<ParsedOperand> operands)
        {
            return new JumpLessCommand(operands[0].addressing, operands[0].value);
        }
    }
}
