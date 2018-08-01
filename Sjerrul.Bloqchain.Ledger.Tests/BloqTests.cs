using System;
using Xunit;

namespace Sjerrul.Bloqchain.Ledger.TEsts
{
    public class BloqTests
    {
        [Fact]
        public void Ctor_DefaultHashIsGenesisHash()
        {
            // Act
            var bloq = new Bloq<string>();

            // Assert
            Assert.Equal("0000000000000000000000000000000000000000000000000000000000000000", bloq.PreviousHash);
        }

        [Fact]
        public void Ctor_NegativeIndex_Throws()
        {
            // Arrange
            int index = -1;
            int nonce = 0;
            DateTime timestamp = DateTime.UtcNow;
            string data = "Hello, I'm some data";

            // Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Bloq<string>(index, nonce, timestamp, data, "PreviousHash"));
        }

        [Fact]
        public void Ctor_PreviousHashNull_Throws()
        {
            // Arrange
            int index = 3;
            int nonce = 0;
            DateTime timestamp = DateTime.UtcNow;
            string data = null;
            string previousHash = null;

            //  Act and Assert
            Assert.Throws<ArgumentNullException>(() => new Bloq<string>(index, nonce, timestamp, data, previousHash));
        }

        [Fact]
        public void Ctor_EmptyHashNull_Throws()
        {
            // Arrange
            int index = 3;
            int nonce = 0;
            DateTime timestamp = DateTime.UtcNow;
            string data = null;
            string previousHash = string.Empty;

            //  Act and Assert
            Assert.Throws<ArgumentNullException>(() => new Bloq<string>(index, nonce, timestamp, data, previousHash));
        }

        [Fact]
        public void CalculateHash_AllSameDataInBloq_GivesSameHash()
        {
            // Arrange
            int index = 3;
            int nonce = 0;
            DateTime timestamp = DateTime.UtcNow;
            string data = "Hello, I'm some data";
            string previousHash = "41aa65db55b7bfd4af15c2945d3943c202ee77728719fed39703a2f03855c703";

            var bloq1 = new Bloq<string>(index, nonce, timestamp, data, previousHash);
            var bloq2 = new Bloq<string>(index, nonce, timestamp, data, previousHash);

            // Act
            int difficulty = 0;

            bloq1.Mine(difficulty);
            bloq2.Mine(difficulty);

            string hashOfBLoq1 = bloq1.Hash;
            string hashOfBLoq2 = bloq2.Hash;

            // Assert
            Assert.Equal(hashOfBLoq1, hashOfBLoq2);
        }

        [Fact]
        public void CalculateHash_AllSameData_ExceptIndex_GivesDifferentSameHash()
        {
            // Arrange
            int nonce = 0;
            DateTime timestamp = DateTime.UtcNow;
            string data = "Hello, I'm some data";
            string previousHash = "41aa65db55b7bfd4af15c2945d3943c202ee77728719fed39703a2f03855c703";

            var bloq1 = new Bloq<string>(1, nonce, timestamp, data, previousHash);
            var bloq2 = new Bloq<string>(2, nonce, timestamp, data, previousHash);

            // Act
            int difficulty = 0;

            bloq1.Mine(difficulty);
            bloq2.Mine(difficulty);

            string hashOfBLoq1 = bloq1.Hash;
            string hashOfBLoq2 = bloq2.Hash;

            // Assert
            Assert.NotEqual(hashOfBLoq1, hashOfBLoq2);
        }

        [Fact]
        public void CalculateHash_AllSameData_ExceptTimestamp_GivesDifferentSameHash()
        {
            // Arrange
            int index = 5;
            int nonce = 0;
            string data = "Hello, I'm some data";
            string previousHash = "41aa65db55b7bfd4af15c2945d3943c202ee77728719fed39703a2f03855c703";

            var bloq1 = new Bloq<string>(index, nonce, DateTime.UtcNow.AddDays(1), data, previousHash);
            var bloq2 = new Bloq<string>(index, nonce, DateTime.UtcNow.AddDays(-1), data, previousHash);

            // Act
            int difficulty = 0;

            bloq1.Mine(difficulty);
            bloq2.Mine(difficulty);

            string hashOfBLoq1 = bloq1.Hash;
            string hashOfBLoq2 = bloq2.Hash;

            // Assert
            Assert.NotEqual(hashOfBLoq1, hashOfBLoq2);
        }

        [Fact]
        public void CalculateHash_AllSameData_ExceptData_GivesDifferentSameHash()
        {
            // Arrange
            int index = 5;
            int nonce = 0;
            DateTime timestamp = DateTime.UtcNow;
            string previousHash = "41aa65db55b7bfd4af15c2945d3943c202ee77728719fed39703a2f03855c703";

            var bloq1 = new Bloq<string>(index, nonce, timestamp, "Hello, I'm some data", previousHash);
            var bloq2 = new Bloq<string>(index, nonce, timestamp, "Hello, I'm some other data", previousHash);

            // Act
            int difficulty = 0;

            bloq1.Mine(difficulty);
            bloq2.Mine(difficulty);

            string hashOfBLoq1 = bloq1.Hash;
            string hashOfBLoq2 = bloq2.Hash;

            // Assert
            Assert.NotEqual(hashOfBLoq1, hashOfBLoq2);
        }

        [Fact]
        public void CalculateHash_AllSameData_ExceptPreviousHash_GivesDifferentSameHash()
        {
            // Arrange
            int index = 5;
            int nonce = 0;
            string data = "Hello, I'm some data";
            DateTime timestamp = DateTime.UtcNow;

            var bloq1 = new Bloq<string>(index, nonce, timestamp, data, "Some Hash");
            var bloq2 = new Bloq<string>(index, nonce, timestamp, data, "Some Other Hash");

            // Act
            int difficulty = 0;

            bloq1.Mine(difficulty);
            bloq2.Mine(difficulty);

            string hashOfBLoq1 = bloq1.Hash;
            string hashOfBLoq2 = bloq2.Hash;

            // Assert
            Assert.NotEqual(hashOfBLoq1, hashOfBLoq2);
        }

        [Fact]
        public void IsGenesisBloq_PreviousHashIsGenesisHash_ReturnsTrue()
        {
            // Arrange
            int index = 5;
            int nonce = 0;
            string data = "Hello, I'm some data";
            DateTime timestamp = DateTime.UtcNow;

            var bloq = new Bloq<string>(index, nonce, timestamp, data, "Some Hash");
            bloq.PreviousHash = BloqHashing.GetGenesisHash();

            // Act
            bool result = bloq.IsGenesisBloq;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsGenesisBloq_PreviousHashIsNotGenesisHash_ReturnsTrue()
        {
            // Arrange
            int index = 5;
            int nonce = 0;
            string data = "Hello, I'm some data";
            DateTime timestamp = DateTime.UtcNow;

            var bloq = new Bloq<string>(index, nonce, timestamp, data, "Some Hash");
            bloq.PreviousHash = "Some Hash";

            // Act
            bool result = bloq.IsGenesisBloq;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Mine_NegativeDifficulty_Throws()
        {
            // Arrange
            int index = 5;
            int nonce = 0;
            string data = "Hello, I'm some data";
            DateTime timestamp = DateTime.UtcNow;

            var bloq = new Bloq<string>(index, nonce, timestamp, data, "Some Hash");
            bloq.PreviousHash = "Some Hash";

            // Act and Assert
            int difficulty = -3;
            Assert.Throws<ArgumentOutOfRangeException>(() => bloq.Mine(difficulty));
        }
    }
}
