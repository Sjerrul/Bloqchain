using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sjerrul.Bloqchain.Ledger
{
    public class Bloqchain<T>
    {
        private readonly IList<Bloq<T>> chain;

        public int Length => this.chain.Count;

        public Bloqchain()
        {
            this.chain = new List<Bloq<T>>();
        }

        public void AddBloq(Bloq<T> bloqToAdd)
        {
            if (bloqToAdd == null)
            {
                throw new ArgumentNullException(nameof(bloqToAdd));
            }

            if (!this.chain.Any())
            {
                bloqToAdd.SetPreviousHash(BloqHashing.GetGenesisHash());
            }

            this.chain.Add(bloqToAdd);
        }
    }
}
