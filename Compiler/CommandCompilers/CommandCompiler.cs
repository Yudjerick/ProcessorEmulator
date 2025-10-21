using ProcessorEmulator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProcessorEmulator.Compiler.CommandCompilers
{
    internal abstract class CommandCompiler
    {
        protected CompilerContext _context;

        public abstract CommandType CommandType { get; }
        protected abstract List<List<AddressingType>> ExpectedOperandsTypes { get; }


        
        public void Init(CompilerContext context)
        {
            _context = context;
        }
        public bool TryParseCommandOperand(string token, out ParsedOperand parsedOperand, out string errorMsg)
        {
            parsedOperand = new ParsedOperand();
            errorMsg = "";

            string pattern = @"^\[\w+\]$";
            if (Regex.IsMatch(token, pattern))
            {

                var tokenWithoutBrackets = token.Substring(1, token.Length - 2);
                if (_context.registersNames.Contains(tokenWithoutBrackets))
                {
                    parsedOperand.addressing = AddressingType.RegisterIndirect;
                    parsedOperand.value = (uint)_context.registersNames.IndexOf(tokenWithoutBrackets);
                }
                else if (_context.variables.Keys.Contains(tokenWithoutBrackets))
                {
                    parsedOperand.addressing = AddressingType.Direct;
                    parsedOperand.value = (uint)_context.variables[tokenWithoutBrackets];
                }
                else if (ushort.TryParse(tokenWithoutBrackets, out ushort literalValue))
                {
                    parsedOperand.addressing = AddressingType.Direct;
                    parsedOperand.value = literalValue;
                }
                else
                {
                    errorMsg = $"{token} is not a valid operand";
                    return false;
                }
            }
            else
            {
                if (_context.registersNames.Contains(token))
                {
                    parsedOperand.addressing = AddressingType.Register;
                    parsedOperand.value = (uint)_context.registersNames.IndexOf(token);
                }
                else if (_context.variables.Keys.Contains(token))
                {
                    parsedOperand.addressing = AddressingType.Immediate;
                    parsedOperand.value = (uint)_context.variables[token];
                }
                else if (ushort.TryParse(token, out ushort literalValue))
                {
                    parsedOperand.addressing = AddressingType.Immediate;
                    parsedOperand.value = literalValue;
                }
                else
                {
                    errorMsg = $"{token} is not a valid operand";
                    return false;
                }
            }
            return true;
        }

        protected bool TryParseAllOperands(string[] operands, out List<ParsedOperand>? parsedOperands, out string errorMsg)
        {
            var result = new List<ParsedOperand>();
            foreach (var operand in operands)
            {
                if(TryParseCommandOperand(operand, out ParsedOperand parsedOperand, out errorMsg)){
                    result.Add(parsedOperand);
                }
                else
                {
                    parsedOperands = null;
                    return false;
                }
            }
            errorMsg = string.Empty;
            parsedOperands = result;
            return true;
        }

        protected bool CheckOperandCount(string[] operands, out string errorMsg)
        {
            if(operands.Length == ExpectedOperandsTypes.Count)
            {
                errorMsg = "";
                return true;
            }
            errorMsg = $"Command {CommandType} expects {ExpectedOperandsTypes.Count} operands, got {operands.Length}";
            return false;
        }

        protected bool CheckOperandAddressingTypes(List<AddressingType> addressings, out string errorMsg)
        {
            for(int i = 0; i < addressings.Count; i++)
            {
                if (!ExpectedOperandsTypes[i].Contains(addressings[i]))
                {
                    errorMsg = $"Operand {i} expected to have {string.Join(" or ",ExpectedOperandsTypes[i])} addressing type, got {addressings[i]}";
                    return false;
                }
                
            }
            errorMsg = "";
            return true;
        }

        public abstract bool TryCompile(string[] operands, out Command? command, out string errorMsg);

        public class ParsedOperand
        {
            public AddressingType addressing;
            public uint value;
        }

    }
}
