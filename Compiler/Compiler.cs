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
        private bool expectOnlyCommand = false;
        public void Compile(string source)
        {
            var lines = source.Split('\n');
            for(int i  = 0; i < lines.Length; i++)
            {
                if(!TryCompileLine(lines[i], out string error))
                {
                    Console.WriteLine(error);
                    break;
                }
            }
            for(int i = 0; i < Context.commandCompilers.Count; i++)
            {
                if (Context.commandCompilers[i].TryCompile(out Command command, out string error))
                {
                    Context.compiledCommands.Add(command);
                    
                }
                else{
                    Console.WriteLine(error);
                    break;
                }

            }
        }

        public bool TryCompileLine(string line, out string errorMessage)
        {
            
            errorMessage = string.Empty;
            
            line = line.Split(';')[0];
            line = line.Trim();
            line = Regex.Replace(line, "[ ]{2,}", " ");
            var tokens = line.Split(new[] { ' ', '\t' });

            if (line == string.Empty)
            {
                return true;
            }

            if (Enum.TryParse(tokens[0], true, out CommandType commandType))
            {
                string[] operands = new string[tokens.Length - 1];
                Array.Copy(tokens, 1, operands, 0, operands.Length);

                if (CreateCommandCompiler(operands, commandType, out CommandCompiler? commandCompiler, out errorMessage))
                {
                    Context.commandCompilers.Add(commandCompiler);
                    expectOnlyCommand = false;
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
                expectOnlyCommand = true;
                string pattern = @"^[a-zA-Z]\w*\s*:$";
                if(Regex.IsMatch(line, pattern))
                {
                    string label = line.Substring(0,line.Length - 1).Trim();
                    Context.labels.Add(label, Context.commandCompilers.Count);
                    return true;
                }
                else
                {
                    errorMessage = $"'{line}' is not valid label fromat";
                    return false;
                }

            }
            errorMessage = $"Unknown error in line '{line}'";
            return false;
        }

        public bool CreateCommandCompiler(string[] operands, CommandType commandType, out CommandCompiler? compiler, out string errorMessage)
        {
            errorMessage = string.Empty;
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
                    compiler = null;
                    errorMessage = $"Command type {commandType} not implemented";
                    return false;
            }
            compiler.Init(Context, operands);
            return true;
        }

        
    }
}
