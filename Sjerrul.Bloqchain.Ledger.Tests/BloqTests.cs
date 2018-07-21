using System;
using Xunit;

namespace Sjerrul.Bloqchain.Ledger.TEsts
{
    public class BloqTests
    {
        [Fact]
        public void CalculateHash_AllSameDataInBloq_GivesSameHash()
        {
            // Arrange
            int index = 3;
            DateTime timestamp = DateTime.UtcNow;
            string data = "Hello, I'm some data";
            string previousHash = string.Empty;

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
            string previousHash = string.Empty;

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
            string previousHash = string.Empty;

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
            string previousHash = string.Empty;

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
