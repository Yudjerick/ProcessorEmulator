using ProcessorEmulator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Compiler
{
    
    internal class Compiler
    {
        public List<Command> compiledCommands = new List<Command>();
        public uint[] dataMemory = new uint[1024];

        private Dictionary<string, int> labels;
        private Dictionary<string, int> variables;

        

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
                        errorMessage = "Load commad expects two args";
                        return false;
                    }
                    break;
            }
            return true;
        }

    }
}
