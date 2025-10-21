using ProcessorEmulator.Commands;
using ProcessorEmulator.Compiler.CommandCompilers;
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
        public CompilerContext Context = new CompilerContext();
        public void Compile(string source)
        {
            var lines = source.Split('\n');
        }

        public bool TryCompileLine(string line, out string errorMessage)
        {
            errorMessage = string.Empty;
            line = line.Split(';')[0];
            var tokens = line.Split(new[] { ' ', '\t' });

            if (Enum.TryParse(tokens[0], true, out CommandType commandType))
            {
                string[] operands = new string[tokens.Length - 1];
                Array.Copy(tokens, 1, operands, 0, operands.Length);

                if (TryCompileCommand(operands, commandType, out Command? command, out errorMessage))
                {
                    Context.compiledCommands.Add(command);
                    return true;
                }
                
            }
            return false;
        }

        public bool TryCompileCommand(string[] operands, CommandType commandType, out Command? command, out string errorMessage)
        {
            errorMessage = string.Empty;
            CommandCompiler compiler;
            switch (commandType)
            {
                case CommandType.LOAD:
                    compiler = new LoadCommandCompiler();
                    break;
                default:
                    command = null;
                    errorMessage = "Command type not implemented";
                    return false;
            }
            compiler.Init(Context);
            if (compiler.TryCompile(operands, out command, out errorMessage))
            {
                return true;
            }
            return false;
        }
    }
}
