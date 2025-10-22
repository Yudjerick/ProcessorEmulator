using ProcessorEmulator.Commands;
using ProcessorEmulator.Compiler.CommandCompilers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Compiler
{
    internal class CompilerContext
    {
        public List<Command> compiledCommands = new List<Command>();
        public List<CommandCompiler> commandCompilers = new List<CommandCompiler>();
        public uint[] dataMemory = new uint[1024];

        public Dictionary<string, int> labels = new Dictionary<string, int>();
        public Dictionary<string, int> variables = new Dictionary<string, int>();

        public List<string> registersNames = new List<string>()
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
    }
}
