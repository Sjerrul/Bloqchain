using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sjerrul.Bloqchain.Ledger
{
    public class BloqChain<T> : IEnumerable<Bloq<T>>
    {
        public int Difficulty { get; private set; }

        private readonly IList<Bloq<T>> chain;

        public int Length => this.chain.Count;

        public bool IsValid => CalculateValidity();

        public BloqChain(int difficulty)
        {
            if (difficulty < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(difficulty), "Difficulty must be zero a positive number");
            }

            this.Difficulty = difficulty;

            var genesisBloq = new Bloq<T>();
            genesisBloq.Mine(this.Difficulty);

            this.chain = new List<Bloq<T>>
            {
                genesisBloq
            };
        }

        public void AddBloq(T data)
        {
            if (EqualityComparer<T>.Default.Equals(data, default(T)))
            {
                throw new ArgumentException("The data you are trying to add is the default for the type. Use actual data for the bloq", nameof(data));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            Bloq<T> bloqToAdd = new Bloq<T>(this.Length, 0, DateTime.UtcNow, data, this.chain[this.Length - 1].Hash);
            bloqToAdd.Mine(this.Difficulty);

            this.chain.Add(bloqToAdd);
        }

        private bool CalculateValidity()
        {
            string difficultyMarker = new string('0', this.Difficulty);

            for (int i = 0; i < this.Length; i++)
            {
                Bloq<T> currentBloq = this.chain[i];

                if (!currentBloq.Hash.StartsWith(difficultyMarker))
                {
                    return false;
                }

                if (currentBloq.Hash != currentBloq.CalculateHash())
                {
                    return false;
                }

                if (i < this.Length - 1)
                {
                    Bloq<T> nextBloq = this.chain[i + 1];
                    if (nextBloq.PreviousHash != currentBloq.Hash)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Bloq<T>> GetEnumerator()
        {
            return this.chain.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Gets the <see cref="Bloq{T}"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="Bloq{T}"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns>The block at the requested index</returns>
        public Bloq<T> this[int index]
        {
            get
            {
                return this.chain[index];
            } 

            set
            {
                this.chain[index] = value;
            }
        }
    }
}
