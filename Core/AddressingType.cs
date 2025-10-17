using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator
{
    public enum AddressingType : ushort
    {
        Immediate = 0b_00,
        Direct = 0b_01,
        Register = 0b_10,
        RegisterIndirect = 0b_11
    }
}
