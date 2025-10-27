using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessorEmulator.Commands;
using ProcessorEmulator.Core;

namespace ProcessorEmulator
{
    public class CommandBuilder
    {
        public static Command BuildFromBinary(uint binary)
        {
            CommandType cmdType = (CommandType)(binary >> 22);
            Command result;
            Console.WriteLine(cmdType.ToString());
            switch (cmdType)
            {
                case CommandType.LOAD:
                    result = new LoadCommand(binary);
                    break;
                case CommandType.STORE:
                    result = new StoreCommand(binary);
                    break;
                case CommandType.CMP:
                    result = new CompareCommand(binary);
                    break;
                case CommandType.ADD:
                    result = new AddCommand(binary);
                    break;
                case CommandType.SUB:
                    result = new SubstractCommand(binary);
                    break;
                case CommandType.INC:
                    result = new IncrementCommand(binary);
                    break;
                case CommandType.DEC:
                    result = new DecrementCommand(binary);
                    break;
                case CommandType.MUL:
                    result = new MultiplyCommand(binary);
                    break;
                case CommandType.JNZ:
                    result = new JumpNotZeroCommand(binary);
                    break;
                case CommandType.JZ:
                    result = new JumpZeroCommand(binary);
                    break;
                case CommandType.JL:
                    result = new JumpLessCommand(binary);
                    break;
                case CommandType.JG:
                    result = new JumpGreaterCommand(binary);
                    break;
                case CommandType.RETURN:
                    result = new ReturnCommand();
                    break;
                default:
                    throw new Exception("Command type not implemented");
            }
            return result;
        }
    }
}
