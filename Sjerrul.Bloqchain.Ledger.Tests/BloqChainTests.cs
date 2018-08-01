using System;
using Xunit;

namespace Sjerrul.Bloqchain.Ledger.TEsts
{
    public class BloqChainTests
    {
        [Fact]
        public void Ctor_StartsWithOnlyGenesisBloq()
        {
            // Act
            var chain = new BloqChain<string>();

            // Assert
            Assert.Equal(1, chain.Length);
        }

        [Fact]
        public void AddBloq_NullBloq_Throws()
        {
            // Act and Assert
            var chain = new BloqChain<string>();
            Assert.Throws<ArgumentException>(() => chain.AddBloq(null));
        }

        [Fact]
        public void AddBloq_ValidBloq_AddsToChain()
        {
            // Arrange
            string data = "Some data";

            // Act
            var chain = new BloqChain<string>();
            chain.AddBloq(data);

            // Assert
            Assert.Equal(2, chain.Length);
        }

        [Fact]
        public void AddBloq_DefaultData_Throws()
        { 
            // Act And Assert
            var chain = new BloqChain<bool>();
            Assert.Throws<ArgumentException>(() => chain.AddBloq(default(bool)));
        }

        [Fact]
        public void Indexer_ValidIndex_GetsBloq()
        {
            // Arrange
            string data = "Some data";

            var chain = new BloqChain<string>();
            chain.AddBloq(data);

            // Act
            var bloq0 = chain[0];
            var bloq1 = chain[1];

            // Assert
            Assert.NotNull(bloq0);
            Assert.Equal(BloqHashing.GetGenesisHash(), bloq0.PreviousHash);

            Assert.NotNull(bloq1);
            Assert.Equal(data, bloq1.Data);
        }

        [Fact]
        public void Indexer_IndexOutOfRange_Throws()
        {
            // Arrange
            var chain = new BloqChain<string>();
            chain.AddBloq("Some data");

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => chain[5]);
        }

        [Fact]
        public void IsValid_UntamperedInitializedChain_ReturnsTrue()
        {
            // Arrange
            var chain = new BloqChain<string>();

            // Act
            bool result = chain.IsValid;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValid_TamperedDataWithoutRemine_ReturnsFalse()
        {
            // Arrange
            string data = "Some data";

            var chain = new BloqChain<string>();
            chain.AddBloq(data);

            // Act
            bool result = chain.IsValid;

            // Assert
            Assert.True(result);
        }
    }
}
