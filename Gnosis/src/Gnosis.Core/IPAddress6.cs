using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class IPAddress6
        : IIPAddress6
    {
        public IPAddress6(short group1, short group2, short group3, short group4, short group5, short group6, short group7, short group8)
        {
            this.group1 = group1;
            this.group2 = group2;
            this.group3 = group3;
            this.group4 = group4;
            this.group5 = group5;
            this.group6 = group6;
            this.group7 = group7;
            this.group8 = group8;
        }

        private short group1;
        private short group2;
        private short group3;
        private short group4;
        private short group5;
        private short group6;
        private short group7;
        private short group8;

        public short Group1
        {
            get { return group1; }
        }

        public short Group2
        {
            get { return group2; }
        }

        public short Group3
        {
            get { return group3; }
        }

        public short Group4
        {
            get { return group4; }
        }

        public short Group5
        {
            get { return group5; }
        }

        public short Group6
        {
            get { return group6; }
        }

        public short Group7
        {
            get { return group7; }
        }

        public short Group8
        {
            get { return group8; }
        }

        public bool IsLocalhost
        {
            get { return group1 == 0 && group2 == 0 && group3 == 0 && group4 == 0 && group5 == 0 && group6 == 0 && group7 == 0 && group8 == 1; }
        }
    }
}
