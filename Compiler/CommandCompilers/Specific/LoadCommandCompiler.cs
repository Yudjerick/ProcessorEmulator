using ProcessorEmulator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Compiler.CommandCompilers.Specific
{
    internal class LoadCommandCompiler : CommandCompiler
    {
        public override CommandType CommandType => CommandType.LOAD;

        protected override List<List<AddressingType>> ExpectedOperandsTypes => new List<List<AddressingType>>
        {
            new List<AddressingType>
            {
                AddressingType.Register
            },
            new List<AddressingType>
            {
                AddressingType.Register,
                AddressingType.Direct,
                AddressingType.RegisterIndirect,
                AddressingType.Immediate
            }
        };

        protected override Command GetSpecificCommand(List<ParsedOperand> operands)
        {
            return new LoadCommand(operands[1].addressing, operands[0].value, operands[1].value);
        }
    }
}
