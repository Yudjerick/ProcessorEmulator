// See https://aka.ms/new-console-template for more information
using ProcessorEmulator;
using ProcessorEmulator.Commands;
using ProcessorEmulator.Compiler;
using System.Diagnostics;

/*Compiler compiler = new Compiler();
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
}*/

using System.IO;

using System;
class Program
{
    static void Main(string[] args)
    {
        // Проверяем, передан ли аргумент с путём к файлу
        if (args.Length == 0)
        {
            Console.WriteLine("Ошибка: не указан путь к файлу.");
            Console.WriteLine("Использование: TextFileReader.exe <путь_к_файлу>");
            return;
        }

        string filePath = args[0];

        // Проверяем, существует ли файл
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Ошибка: файл '{filePath}' не найден.");
            return;
        }

        try
        {
            // Читаем весь текст из файла и выводим в консоль
            string content = File.ReadAllText(filePath);

            Compiler compiler = new Compiler();
            if (compiler.Compile(content,out string error, out int errorLineNum))
            {
                Processor processor = new Processor();
                compiler.LoadToProcessor(processor);

                processor.ExecuteAll();

                Console.WriteLine();
                Console.WriteLine("r0:");
                Console.WriteLine((int)processor.registers[0]);
            }
            else
            {
                Console.WriteLine($"Error on line {errorLineNum}: {error}");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
        }
    }
}

