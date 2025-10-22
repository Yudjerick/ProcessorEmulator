using ProcessorEmulator.Commands;
using ProcessorEmulator.Compiler.CommandCompilers;
using ProcessorEmulator.Compiler.CommandCompilers.Specific;
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

        public bool TryCompileLine(string line, bool expectOnlyCommand, out string errorMessage)
        {
            errorMessage = string.Empty;
            line = line.Split(';')[0];
            line = line.Trim();
            line = Regex.Replace(line, "[ ]{2,}", " ");
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
                return false;
            }
            else if (expectOnlyCommand)
            {
                errorMessage = "Command expected after label";
                return false;
            }
            else if (line[^1] == ':')
            {
                string pattern = @"^[a-zA-Z]\w*\s*:$";
                if(Regex.IsMatch(line, pattern))
                {
                    string label = line.Substring(line.Length - 1).Trim();
                    Context.labels.Add(label, Context.compiledCommands.Count);
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
                case CommandType.ADD:
                    compiler = new AddCommandCompiler();
                    break;
                case CommandType.CMP:
                    compiler = new CompareCommandCompiler();
                    break;
                case CommandType.DEC:
                    compiler = new DecrementCommandCompiler();
                    break;
                case CommandType.INC:
                    compiler = new IncrementCommandCompiler();
                    break;
                case CommandType.JG:
                    compiler = new JumpGreaterCommandCompiler();
                    break;
                case CommandType.JL:
                    compiler = new JumpLessCommandCompiler();
                    break;
                case CommandType.JNZ:
                    compiler = new JumpNotZeroCommandCompiler();
                    break;
                case CommandType.JZ:
                    compiler = new JumpZeroCommandCompiler();
                    break;
                case CommandType.LOAD:
                    compiler = new LoadCommandCompiler();
                    break;
                case CommandType.RETURN:
                    compiler = new ReturnCommandCompiler();
                    break;
                case CommandType.STORE:
                    compiler = new StoreCommandCompiler();
                    break;
                default:
                    command = null;
                    errorMessage = $"Command type {commandType} not implemented";
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
