using ProcessorEmulator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Compiler.CommandCompilers.Specific
{
    internal class JumpGreaterCommandCompiler: CommandCompiler
    {
        public override CommandType CommandType => CommandType.JG;

        protected override List<List<AddressingType>> ExpectedOperandsTypes => new List<List<AddressingType>>
        {
            new List<AddressingType>
            {
                AddressingType.Immediate
            }
        };

        protected override Command GetSpecificCommand(List<ParsedOperand> operands)
        {
            return new JumpGreaterCommand(operands[0].addressing, operands[0].value);
        }
    }
}
