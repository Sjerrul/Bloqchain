using System;
using Xunit;

namespace Sjerrul.Bloqchain.Ledger.TEsts
{
    public class BloqChainTests
    {
        [Fact]
        public void Ctor_StartsWithEmptyChain()
        {
            // Act
            var chain = new Bloqchain<string>();

            // Assert
            Assert.Equal(0, chain.Length);
        }

        [Fact]
        public void AddBloq_NullBloq_Throws()
        {
            // Act and Assert
            var chain = new Bloqchain<string>();
            Assert.Throws<ArgumentNullException>(() => chain.AddBloq(null));
        }

        [Fact]
        public void AddBloq_ValidBloq_AddsToChain()
        {
            // Arrange
            var bloq = new Bloq<string>(1, DateTime.UtcNow, "Some data");

            // Act
            var chain = new Bloqchain<string>();
            chain.AddBloq(bloq);

            // Assert
            Assert.Equal(1, chain.Length);
        }
    }
}
