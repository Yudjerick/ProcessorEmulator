// See https://aka.ms/new-console-template for more information
using ProcessorEmulator;
using ProcessorEmulator.Commands;

Processor processor = new Processor();
processor.dataMemory[0] = 6;
processor.dataMemory[1] = 1;
processor.dataMemory[2] = 5;
processor.dataMemory[3] = 7;
processor.dataMemory[4] = 3;
processor.dataMemory[5] = 4;
processor.dataMemory[6] = 3;

List<Command> commands = new List<Command>()
{
    new LoadCommand(AddressingType.Direct, 2u, 0u),
    new LoadCommand(AddressingType.Immediate, 0u, 0u),
    new LoadCommand(AddressingType.Immediate, 1u, 0u),
    IncrementCommand.CreateFromOperand(1u),
    new LoadCommand(AddressingType.RegisterIndirect, 3u, 1u),
    new CompareCommand(3u, 0u),
    new JumpLessCommand(AddressingType.Immediate, 8u),
    new LoadCommand(AddressingType.Register, 0u, 3u),
    IncrementCommand.CreateFromOperand(1u),
    DecrementCommand.CreateFromOperand(2u),
    new JumpNotZeroCommand(AddressingType.Immediate, 4u),
    new ReturnCommand()

};

for(int i = 0; i < commands.Count; i++)
{
    processor.commandMemory[i] = commands[i].data;
}


processor.ExecuteAll();

Console.WriteLine();
Console.WriteLine(processor.registers[0]);
