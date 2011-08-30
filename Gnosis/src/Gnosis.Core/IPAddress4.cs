using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class IPAddress4
        : IIPAddress4
    {
        public IPAddress4(byte octet1, byte octet2, byte octet3, byte octet4)
        {
            this.octet1 = octet1;
            this.octet2 = octet2;
            this.octet3 = octet3;
            this.octet4 = octet4;
        }

        private readonly byte octet1;
        private readonly byte octet2;
        private readonly byte octet3;
        private readonly byte octet4;

        public byte Octet1
        {
            get { return octet1; }
        }

        public byte Octet2
        {
            get { return octet2; }
        }

        public byte Octet3
        {
            get { return octet3; }
        }

        public byte Octet4
        {
            get { return octet4; }
        }

        public bool IsLocalhost
        {
            get { return octet1 == 127 && octet2 == 0 && octet3 == 0 && octet4 == 1; }
        }
    }
}
