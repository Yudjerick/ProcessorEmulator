using ProcessorEmulator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Compiler.CommandCompilers
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
                AddressingType.RegisterIndirect,
                AddressingType.RegisterIndirect,
                AddressingType.Immediate
            }
        };

        public override bool TryCompile(string[] operands, out Command? command, out string errorMsg)
        {
            errorMsg = "";
            command = null;
            if(!CheckOperandCount(operands, out errorMsg))
            {
                return false;
            }
            if(!TryParseAllOperands(operands, out List<ParsedOperand> parsedOperands, out errorMsg))
            {
                return false;
            }
            if(!CheckOperandAddressingTypes(parsedOperands.Select(x=>x.addressing).ToList(), out errorMsg))
            {
                return false;
            }
            command = new LoadCommand(parsedOperands[1].addressing, parsedOperands[0].value, parsedOperands[1].value);
            return true;
        }
    }
}
