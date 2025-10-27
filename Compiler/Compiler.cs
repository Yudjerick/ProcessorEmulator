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
        private int nextFreeMemoryCell = 0;

        public void LoadToProcessor(Processor processor)
        {
            for (int i = 0; i < Context.compiledCommands.Count; i++)
            {
                processor.commandMemory[i] = Context.compiledCommands[i].data;
            }
            processor.dataMemory = Context.dataMemory;
        }
        public bool Compile(string source, out string errorMsg, out int errorLineNumber)
        {
            errorLineNumber = -1;
            errorMsg = "";
            var lines = source.Split('\n');
            for(int i  = 0; i < lines.Length; i++)
            {
                if(!TryCompileLine(lines[i], i, out errorMsg))
                {
                    errorLineNumber = i;
                    return false;
                }
            }
            for(int i = 0; i < Context.commandCompilers.Count; i++)
            {
                if (Context.commandCompilers[i].TryCompile(out Command command, out errorMsg))
                {
                    Context.compiledCommands.Add(command);
                }
                else
                {
                    errorLineNumber = Context.commandCompilers[i].CreatedOnLine;
                    return false;
                }
            }
            return true;
        }

        public bool TryCompileLine(string line, int lineNum, out string errorMessage)
        {
            
            errorMessage = string.Empty;
            
            line = line.ToLower();
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

                if (CreateCommandCompiler(operands, commandType, lineNum, out CommandCompiler? commandCompiler, out errorMessage))
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
                string pattern = @"^[a-zA-Z_]\w*\s*:$";
                if(Regex.IsMatch(line, pattern))
                {
                    string label = line.Substring(0,line.Length - 1).Trim();
                    if (Context.variables.ContainsKey(label))
                    {
                        errorMessage = $"'{label}' allready denfined as variable";
                        return false;
                    }
                    if (Context.labels.ContainsKey(label))
                    {
                        errorMessage = $"'{label}' allready denfined as label";
                        return false;
                    }
                    Context.labels.Add(label, Context.commandCompilers.Count);
                    return true;
                }
                else
                {
                    errorMessage = $"'{line}' is not valid label fromat";
                    return false;
                }

            }
            else if (tokens[0] == "int")
            {
                if (tokens.Length >= 3)
                {
                    string pattern = @"^[a-zA-Z_][a-zA-Z0-9_]*$";
                    if (Regex.IsMatch(tokens[1], pattern))
                    {
                        if (Context.variables.ContainsKey(tokens[1]))
                        {
                            errorMessage = $"'{tokens[1]}' allready denfined as variable";
                            return false;
                        }
                        if (Context.labels.ContainsKey(tokens[1]))
                        {
                            errorMessage = $"'{tokens[1]}' allready denfined as label";
                            return false;
                        }
                        List<int> variablesToAdd = new List<int>();
                        for(int i = 2; i < tokens.Length; i++)
                        {
                            if (int.TryParse(tokens[i], out int intValue))
                            {
                                variablesToAdd.Add(intValue);
                            }
                            else
                            {
                                errorMessage = $"{tokens[i]} can't be parsed as int";
                                return false;
                            }
                        }
                        
                        Context.variables.Add(tokens[1], nextFreeMemoryCell);
                        foreach (int value in variablesToAdd)
                        {
                            Context.dataMemory[nextFreeMemoryCell] = (uint)value;
                            nextFreeMemoryCell++;
                        }
                        return true;

                    }
                    else
                    {
                        errorMessage = $"'{tokens[1]}' is not a valid variable name";
                        return false;
                    }
                    
                }
            }
            errorMessage = $"Unknown error in line '{line}'";
            return false;
        }

        public bool CreateCommandCompiler(string[] operands, CommandType commandType, int lineNum, 
            out CommandCompiler? compiler, out string errorMessage)
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
                case CommandType.MUL:
                    compiler = new MultiplyCommandCompiler();
                    break;
                case CommandType.RETURN:
                    compiler = new ReturnCommandCompiler();
                    break;
                case CommandType.STORE:
                    compiler = new StoreCommandCompiler();
                    break;
                case CommandType.SUB:
                    compiler = new SubstractCommandCompiler();
                    break;
                default:
                    compiler = null;
                    errorMessage = $"Command type {commandType} not implemented";
                    return false;
            }
            compiler.Init(Context, operands, lineNum);
            return true;
        }

        
    }
}
