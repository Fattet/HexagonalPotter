using HexagonalPotter.Application;
using HexagonalPotter.Domain;
using System.Diagnostics.CodeAnalysis;

namespace HexagonalPotter.Tests
{
    public class HexagonalPotterTests
    {
        #pragma warning disable CA1859 // Using interface to follow hexagonal architecture
        private readonly IBookPriceCalculator _calculator = new PotterPriceCalculator();
        #pragma warning restore CA1859

        [Fact]
        public void TestBasics()
        {
            Assert.Equal(0, _calculator.Price([]));
            Assert.Equal(8, _calculator.Price([1]));
            Assert.Equal(8, _calculator.Price([2]));
            Assert.Equal(8, _calculator.Price([3]));
            Assert.Equal(8, _calculator.Price([4]));
            Assert.Equal(8 * 3, _calculator.Price([1, 1, 1]));
        }

        [Fact]
        public void TestSimpleDiscounts()
        {
            Assert.Equal(8 * 2 * 0.95, _calculator.Price([0, 1]));
            Assert.Equal(8 * 3 * 0.9, _calculator.Price([0, 2, 4]));
            Assert.Equal(8 * 4 * 0.8, _calculator.Price([0, 1, 2, 4]));
            Assert.Equal(8 * 5 * 0.75, _calculator.Price([0, 1, 2, 3, 4]));
        }

        [Fact]
        public void TestSeveralDiscounts()
        {
            Assert.Equal(8 + (8 * 2 * 0.95), _calculator.Price([0, 0, 1]));
            Assert.Equal(2 * (8 * 2 * 0.95), _calculator.Price([0, 0, 1, 1]));
            Assert.Equal((8 * 4 * 0.8) + (8 * 2 * 0.95), _calculator.Price([0, 0, 1, 2, 2, 3]));
            Assert.Equal(8 + (8 * 5 * 0.75), _calculator.Price([0, 1, 1, 2, 3, 4]));
        }

        [Fact]
        public void TestEdgeCases()
        {
            Assert.Equal(2 * (8 * 4 * 0.8), _calculator.Price([0, 0, 1, 1, 2, 2, 3, 4]));
            Assert.Equal(3 * (8 * 5 * 0.75) + 2 * (8 * 4 * 0.8),
                _calculator.Price([
                    0, 0, 0, 0, 0,
                    1, 1, 1, 1, 1,
                    2, 2, 2, 2,
                    3, 3, 3, 3, 3,
                    4, 4, 4, 4
                ])
            );
        }
    }
}