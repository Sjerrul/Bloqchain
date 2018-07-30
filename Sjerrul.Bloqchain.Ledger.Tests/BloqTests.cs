using System;
using Xunit;

namespace Sjerrul.Bloqchain.Ledger.TEsts
{
    public class BloqTests
    {
        [Fact]
        public void Ctor_DefaultHashIsGenesisHash()
        {
            // Arrange
            int index = 3;
            DateTime timestamp = DateTime.UtcNow;
            string data = "Hello, I'm some data";

            // Act
            var bloq = new Bloq<string>(index, timestamp, data);

            // Assert
            Assert.Equal("0000000000000000000000000000000000000000000000000000000000000000", bloq.PreviousHash);
        }

        [Fact]
        public void Ctor_DefaultData_String_Throws()
        {
            // Arrange
            int index = 3;
            DateTime timestamp = DateTime.UtcNow;
            string data = null;

            //  Act and Assert
            Assert.Throws<ArgumentException>(() => new Bloq<string>(index, timestamp, data));
        }

        [Fact]
        public void Ctor_PreviousHashNull_Throws()
        {
            // Arrange
            int index = 3;
            DateTime timestamp = DateTime.UtcNow;
            string data = null;
            string previousHash = null;

            //  Act and Assert
            Assert.Throws<ArgumentException>(() => new Bloq<string>(index, timestamp, data, previousHash));
        }

        [Fact]
        public void Ctor_EmptyHashNull_Throws()
        {
            // Arrange
            int index = 3;
            DateTime timestamp = DateTime.UtcNow;
            string data = null;
            string previousHash = string.Empty;

            //  Act and Assert
            Assert.Throws<ArgumentException>(() => new Bloq<string>(index, timestamp, data, previousHash));
        }

        [Fact]
        public void CalculateHash_AllSameDataInBloq_GivesSameHash()
        {
            // Arrange
            int index = 3;
            DateTime timestamp = DateTime.UtcNow;
            string data = "Hello, I'm some data";
            string previousHash = "41aa65db55b7bfd4af15c2945d3943c202ee77728719fed39703a2f03855c703";

            var bloq1 = new Bloq<string>(index, timestamp, data, previousHash);
            var bloq2 = new Bloq<string>(index, timestamp, data, previousHash);

            // Act
            string hashOfBLoq1 = bloq1.CalculateHash();
            string hashOfBLoq2 = bloq2.CalculateHash();

            // Assert
            Assert.Equal(hashOfBLoq1, hashOfBLoq2);
        }

        [Fact]
        public void CalculateHash_AllSameData_ExceptIndex_GivesDifferentSameHash()
        {
            // Arrange
            DateTime timestamp = DateTime.UtcNow;
            string data = "Hello, I'm some data";
            string previousHash = "41aa65db55b7bfd4af15c2945d3943c202ee77728719fed39703a2f03855c703";

            var bloq1 = new Bloq<string>(1, timestamp, data, previousHash);
            var bloq2 = new Bloq<string>(2, timestamp, data, previousHash);

            // Act
            string hashOfBLoq1 = bloq1.CalculateHash();
            string hashOfBLoq2 = bloq2.CalculateHash();

            // Assert
            Assert.NotEqual(hashOfBLoq1, hashOfBLoq2);
        }

        [Fact]
        public void CalculateHash_AllSameData_ExceptTimestamp_GivesDifferentSameHash()
        {
            // Arrange
            int index = 5;
            string data = "Hello, I'm some data";
            string previousHash = "41aa65db55b7bfd4af15c2945d3943c202ee77728719fed39703a2f03855c703";

            var bloq1 = new Bloq<string>(index, DateTime.UtcNow.AddDays(1), data, previousHash);
            var bloq2 = new Bloq<string>(index, DateTime.UtcNow.AddDays(-1), data, previousHash);

            // Act
            string hashOfBLoq1 = bloq1.CalculateHash();
            string hashOfBLoq2 = bloq2.CalculateHash();

            // Assert
            Assert.NotEqual(hashOfBLoq1, hashOfBLoq2);
        }

        [Fact]
        public void CalculateHash_AllSameData_ExceptData_GivesDifferentSameHash()
        {
            // Arrange
            int index = 5;
            DateTime timestamp = DateTime.UtcNow;
            string previousHash = "41aa65db55b7bfd4af15c2945d3943c202ee77728719fed39703a2f03855c703";

            var bloq1 = new Bloq<string>(index, timestamp, "Hello, I'm some data", previousHash);
            var bloq2 = new Bloq<string>(index, timestamp, "Hello, I'm some other data", previousHash);

            // Act
            string hashOfBLoq1 = bloq1.CalculateHash();
            string hashOfBLoq2 = bloq2.CalculateHash();

            // Assert
            Assert.NotEqual(hashOfBLoq1, hashOfBLoq2);
        }

        [Fact]
        public void CalculateHash_AllSameData_ExceptPreviousHash_GivesDifferentSameHash()
        {
            // Arrange
            int index = 5;
            string data = "Hello, I'm some data";
            DateTime timestamp = DateTime.UtcNow;

            var bloq1 = new Bloq<string>(index, timestamp, data, "Some Hash");
            var bloq2 = new Bloq<string>(index, timestamp, data, "Some Other Hash");

            // Act
            string hashOfBLoq1 = bloq1.CalculateHash();
            string hashOfBLoq2 = bloq2.CalculateHash();

            // Assert
            Assert.NotEqual(hashOfBLoq1, hashOfBLoq2);
        }
    }
}
