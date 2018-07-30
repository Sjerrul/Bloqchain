using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Sjerrul.Bloqchain.Ledger
{
    public class Bloq<T>
    {
        private readonly int index;
        private readonly DateTime timestamp;
        private readonly T data;
        public string PreviousHash { get; private set; }
        public bool IsGenesisBloq => this.PreviousHash == BloqHashing.GetGenesisHash();


        public Bloq(int index, DateTime timestamp, T data)
        {
            if (EqualityComparer<T>.Default.Equals(data, default(T)))
            {
                throw new ArgumentException("The data for this is the default for the type. Use actual data for the bloq", nameof(data));
            }

            this.index = index;
            this.timestamp = timestamp;
            this.data = data;
            this.PreviousHash = BloqHashing.GetGenesisHash();
        }

        public Bloq(int index, DateTime timestamp, T data, string previousHash)
            : this(index, timestamp, data)
        {
            if (string.IsNullOrWhiteSpace(previousHash))
            {
                throw new ArgumentNullException(nameof(previousHash));
            }

            this.PreviousHash = previousHash;
        }

        public string CalculateHash()
        {
            var crypt = new SHA256Managed();

            string stringifiedBloq = Stringify();
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(stringifiedBloq));

            StringBuilder hash = new StringBuilder();
            foreach (byte b in crypto)
            {
                hash.Append(b.ToString("x2"));
            }

            return hash.ToString();
        }

        public void SetPreviousHash(string previousHash)
        {
            if (string.IsNullOrWhiteSpace(previousHash))
            {
                throw new ArgumentNullException(nameof(previousHash));
            }

            this.PreviousHash = previousHash;
        }

        private string Stringify()
        {
            string json = JsonConvert.SerializeObject(this.data);

            return $"{this.index}{this.timestamp.Ticks}{json}{PreviousHash}";
        }
    }
}
