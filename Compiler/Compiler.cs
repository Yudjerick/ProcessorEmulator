using ProcessorEmulator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProcessorEmulator.Compiler
{
    
    internal class Compiler
    {
        public List<Command> compiledCommands = new List<Command>();
        public uint[] dataMemory = new uint[1024];

        public Dictionary<string, int> labels = new Dictionary<string, int>();
        public Dictionary<string, int> variables = new Dictionary<string, int>();

        private List<string> registersNames = new List<string>()
        {
            "r0",
            "r1",
            "r2",
            "r3",
            "r4",
            "r5",
            "r6",
            "r7",
            "r8",
            "r9",
            "r10",
            "r11",
            "r12",
            "r13",
            "r14",
            "r15",
            "r16"
        };
        

        public void Compile(string source)
        {
            var lines = source.Split('\n');
        }

        private void CompileLine(string line)
        {
            line = line.Split(';')[0];
            var tokens = line.Split(new[] { ' ', '\t' });

            if (Enum.TryParse(tokens[0], true, out CommandType commandType))
            {
                CompileCommand(tokens, commandType, out string errorMessage);
            }

        }

        private bool CompileCommand(string[] tokens, CommandType commandType, out string errorMessage) 
        {
            errorMessage = "";
            switch (commandType)
            {
                case CommandType.LOAD:
                    if(tokens.Length != 3)
                    {
                        errorMessage = "Load commad expects two operands";
                        return false;
                    }
                    
                    break;
            }
            return true;
        }

        public bool TryParseCommandOperand(string token, out AddressingType addressing, out uint value, out string errorMsg)
        {
            addressing = 0;
            value = 0;
            errorMsg = "";

            string pattern = @"^\[\w+\]$";
            if(Regex.IsMatch(token, pattern))
            {
                
                var tokenWithoutBrackets = token.Substring(1, token.Length - 2);
                Console.WriteLine(tokenWithoutBrackets);
                if (registersNames.Contains(tokenWithoutBrackets)){
                    addressing = AddressingType.RegisterIndirect;
                    value = (uint)registersNames.IndexOf(tokenWithoutBrackets);
                }
                else if (variables.Keys.Contains(tokenWithoutBrackets))
                {
                    addressing = AddressingType.Direct;
                    value = (uint)variables[tokenWithoutBrackets];
                }
                else
                {
                    errorMsg = "Invalid operand";
                    return false;
                }
            }
            else
            {
                if (registersNames.Contains(token))
                {
                    addressing = AddressingType.Register;
                    value = (uint)registersNames.IndexOf(token);
                }
                else if (variables.Keys.Contains(token))
                {
                    addressing = AddressingType.Immediate;
                    value = (uint)variables[token];
                }
                else if (ushort.TryParse(token, out ushort literalValue))
                {
                    addressing = AddressingType.Immediate;
                    value = literalValue;
                }
                else
                {
                    errorMsg = "Invalid operand";
                    return false;
                }
            }
            return true;
        }

    }

    
}
