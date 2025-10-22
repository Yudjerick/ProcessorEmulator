using ProcessorEmulator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Compiler.CommandCompilers.Specific
{
    internal class ReturnCommandCompiler: CommandCompiler
    {
        public override CommandType CommandType => CommandType.RETURN;

        protected override List<List<AddressingType>> ExpectedOperandsTypes => new List<List<AddressingType>>
        {
        };

        protected override Command GetSpecificCommand(List<ParsedOperand> operands)
        {
            return new ReturnCommand();
        }
    }
}
