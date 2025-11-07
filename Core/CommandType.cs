using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator
{
    public enum CommandType: uint
    {
        LOAD = 0,
        STORE = 1,
        CMP = 2,
        ADD = 3,
        SUB = 4,
        INC = 5,
        DEC = 6,
        JNZ = 7,
        JZ = 8,
        JL = 9,
        JG = 10,
        MUL = 11,
        RETURN = 12,
        ADC = 13
    }
}
