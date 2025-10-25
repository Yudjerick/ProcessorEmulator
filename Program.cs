// See https://aka.ms/new-console-template for more information
using ProcessorEmulator;
using ProcessorEmulator.Commands;
using ProcessorEmulator.Compiler;
using System.Diagnostics;

Compiler compiler = new Compiler();
if(compiler.Compile(
    "int vec1 6 -1 5 6 7 -4 3\n" +
    "LOAD r2 [vec1]\n" +
    "LOAD r0 -32768\n" +
    "LOAD r1 vec1\n" +
    "LOAD r15 1\n" +
    "ADD r1 r15\n" +
    "L1:\n" +
    "LOAD r3 [r1]\n" +
    "CMP r3 r0\n" +
    "JL C1\n" +
    "LOAD r0 r3\n" +
    "C1:\n" +
    "ADD r1 r15\n" +
    "DEC r2\n" +
    "JNZ L1\n" +
    "RETURN", out string error))
{
    Processor processor = new Processor();
    compiler.LoadToProcessor(processor);

    processor.ExecuteAll();

    Console.WriteLine();
    Console.WriteLine((int)processor.registers[0]);
}
else
{
    Console.WriteLine(error);
}


