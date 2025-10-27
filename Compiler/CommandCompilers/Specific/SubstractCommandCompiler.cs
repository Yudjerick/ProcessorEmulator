using ProcessorEmulator.Commands;
using ProcessorEmulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Compiler.CommandCompilers.Specific
{
    internal class SubstractCommandCompiler: CommandCompiler
    {
        public override CommandType CommandType => CommandType.SUB;

        protected override List<List<AddressingType>> ExpectedOperandsTypes => new List<List<AddressingType>>
        {
            new List<AddressingType>
            {
                AddressingType.Register
            },
            new List<AddressingType>
            {
                AddressingType.Register
            }
        };

        protected override Command GetSpecificCommand(List<ParsedOperand> operands)
        {
            return new SubstractCommand(operands[0].value, operands[1].value);
        }
    }
}
