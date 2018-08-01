using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Sjerrul.Bloqchain.Ledger
{
    public class Bloq<T>
    {
        public int Index { get; set; }
        public DateTime Timestamp { get; set; }
        public T Data { get; set; }
        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public bool IsGenesisBloq => this.PreviousHash == BloqHashing.GetGenesisHash();

        /// <summary>
        /// Initializes a new instance of the <see cref="Bloq{T}"/> class as a GenesisBloq.
        /// </summary>
        public Bloq()
        {
            this.Index = 0;
            this.Timestamp = DateTime.UtcNow;
            this.Data = default(T);
            this.PreviousHash = BloqHashing.GetGenesisHash();
            this.Hash = this.CalculateHash();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bloq{T}"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="timestamp">The timestamp.</param>
        /// <param name="data">The data.</param>
        /// <param name="previousHash">The previous hash.</param>
        public Bloq(int index, DateTime timestamp, T data, string previousHash)
            : this()
        {
            if (index <= 0)
            {
                throw new ArgumentOutOfRangeException($"Index cannot be 0 or negative, but is {index}");
            }

            if (string.IsNullOrWhiteSpace(previousHash))
            {
                throw new ArgumentNullException(nameof(previousHash));
            }

            this.Index = index;
            this.Timestamp = timestamp;
            this.Data = data;
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

        private string Stringify()
        {
            string json = JsonConvert.SerializeObject(this.Data);

            return $"{this.Index}{this.Timestamp.Ticks}{json}{this.PreviousHash}";
        }
    }
}
