using ProcessorEmulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorEmulator.Commands
{
    public class Command
    {
        public uint data;

        public CommandType Type
        {
            get
            {
                return (CommandType)GetBits(0, 8);
            }
            protected set
            {
                SetBits(0, 10, (uint)value);
            }
        }

        public AddressingType AddressingTwo
        {
            get
            {
                return (AddressingType)GetBits(10, 2);
            }
            set
            {
                SetBits(10, 2, (uint)value);
            }
        }

        public uint OperandOne
        {
            get
            {
                return GetBits(12, 4);
            }
            set
            {
                SetBits(12, 4, value);
            }
        }

        public uint OperandTwo
        {
            get
            {
                return GetBits(16, 16);
            }
            set
            {
                SetBits(16, 16, value);
            }
        }

        protected void SetBits(int leftOffset, int length, uint value)
        {
            if (length <= 0)
                return;
            var rightOffset = 32 - leftOffset - length;
            var mask = ~(uint.MaxValue >> 32 - length << rightOffset);
            data &= mask;
            data |= value << rightOffset;
        }

        protected uint GetBits(int leftOffset, int length)
        {
            if (length <= 0)
                return 0;
            var rightOffset = 32 - leftOffset - length;
            var mask = uint.MaxValue >> 32 - length << rightOffset;
            return (data & mask) >> rightOffset;
        }

        public virtual void Execute(Processor processor)
        {

        }
    }
}
