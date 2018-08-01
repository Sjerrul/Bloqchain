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
            var chain = new BloqChain<string>(difficulty: 0);

            // Assert
            Assert.Equal(1, chain.Length);
        }

        [Fact]
        public void AddBloq_NullBloq_Throws()
        {
            // Act and Assert
            var chain = new BloqChain<string>(difficulty: 0);
            Assert.Throws<ArgumentException>(() => chain.AddBloq(null));
        }

        [Fact]
        public void AddBloq_ValidBloq_AddsToChain()
        {
            // Arrange
            string data = "Some data";

            // Act
            var chain = new BloqChain<string>(difficulty: 0);
            chain.AddBloq(data);

            // Assert
            Assert.Equal(2, chain.Length);
        }

        [Fact]
        public void AddBloq_DefaultData_Throws()
        { 
            // Act And Assert
            var chain = new BloqChain<bool>(difficulty: 0);
            Assert.Throws<ArgumentException>(() => chain.AddBloq(default(bool)));
        }

        [Fact]
        public void Indexer_ValidIndex_GetsBloq()
        {
            // Arrange
            string data = "Some data";

            var chain = new BloqChain<string>(difficulty: 0);
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
            var chain = new BloqChain<string>(difficulty: 0);
            chain.AddBloq("Some data");

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => chain[5]);
        }

        [Fact]
        public void IsValid_UntamperedInitializedChain_ReturnsTrue()
        {
            // Arrange
            var chain = new BloqChain<string>(difficulty: 0);

            // Act
            bool result = chain.IsValid;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValid_UntamperedChainWithBLoqAdded_ReturnsTrue()
        {
            // Arrange
            string data = "Some data";

            var chain = new BloqChain<string>(difficulty: 0);
            chain.AddBloq(data);

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

            var chain = new BloqChain<string>(difficulty: 0);
            chain.AddBloq(data);

            // Act
            chain[1].Data = "I've changed";
            bool result = chain.IsValid;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_TamperedHashWithoutRemine_ReturnsFalse()
        {
            // Arrange
            string data = "Some data";

            var chain = new BloqChain<string>(difficulty: 0);
            chain.AddBloq(data);

            // Act
            chain[1].PreviousHash = "I'm changed";
            bool result = chain.IsValid;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_TamperedBloqAddedWithRemine_ReturnsTrue()
        {
            // Arrange
            string data = "Some data";

            var chain = new BloqChain<string>(difficulty: 0);
            chain.AddBloq(data);

            // Act
            chain[1].Data = "I'm changed";
            chain[1].Mine(difficulty: 0);

            bool result = chain.IsValid;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValid_TamperedBloqAddedWithRemineWithDifferentDifficulty_ReturnsFalse()
        {
            // Arrange
            string data = "Some data";

            var chain = new BloqChain<string>(difficulty: 1);
            chain.AddBloq(data);

            // Act
            chain[1].Data = "I'm changed";
            chain[1].Mine(difficulty: 0);

            bool result = chain.IsValid;

            // Assert
            Assert.False(result);
        }
    }
}
