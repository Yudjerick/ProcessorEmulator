using ProcessorEmulator;
using ProcessorEmulator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator
{
    public class Processor
    {
        public static Processor Instance;

        public Processor()
        {
            Instance = this;
        }

        public uint[] commandMemory = new uint[256];
        public uint[] dataMemory = new uint[256];

        public uint[] registers = new uint[16];

        public uint currentCommandRegister = 0u;
        public uint commandPointer = 0u;
        public uint flagsRegister = 0u;

        public bool SignFlag { get => GetBit(flagsRegister, 0); set => SetBit(ref flagsRegister, 0, value); }
        public bool ZeroFlag { get => GetBit(flagsRegister, 1); set => SetBit(ref flagsRegister, 1, value); }

        public bool processFinished = false;

        public bool ExecuteCommand()
        {
            currentCommandRegister = commandMemory[commandPointer];
            commandPointer++;
            Command command = CommandBuilder.BuildFromBinary(currentCommandRegister);
            command.Execute(this);

            return processFinished;
        }

        public void ExecuteAll()
        {
            while (!processFinished) 
            {
                ExecuteCommand();
            }
        }

        public void SetValueToOperand(AddressingType addressing, uint operand, uint value)
        {
            switch (addressing)
            {
                case AddressingType.Direct:
                    dataMemory[operand] = value;
                    break;
                case AddressingType.Register:
                    registers[operand] = value;
                    break;
                case AddressingType.RegisterIndirect:
                    dataMemory[registers[operand]] = value;
                    break;
            }
        }
        public int GetOperandValue(AddressingType addressing, uint operand)
        {
            switch (addressing)
            {
                case AddressingType.Immediate: 
                    return (short)operand;
                case AddressingType.Direct:
                    return (int)dataMemory[operand];
                case AddressingType.Register:
                    return (int)registers[operand];
                case AddressingType.RegisterIndirect:
                    return (int)dataMemory[registers[operand]];
                default: return 0;
            }
        }

        private void SetBit(ref uint targetRegister, int leftOffset, bool value)
        {
            uint mask = 1u << (31 - leftOffset);
            if(value)
            {
                targetRegister |= mask;
            }
            else
            {
                targetRegister &= ~mask;
            }
        }

        private bool GetBit(uint targetRegister, int leftOffset)
        {
            uint mask = 1u << (31 - leftOffset);
            return (targetRegister & mask) != 0u;
        }
    }
}
