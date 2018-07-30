using System;
using System.Collections.Generic;
using System.Text;

namespace Sjerrul.Bloqchain.Ledger
{
    public static class BloqHashing
    {
        public static string GetGenesisHash()
        {
            return new String('0', 64);
        }
    }
}
