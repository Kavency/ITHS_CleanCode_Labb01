using CleanCode.Application.Interfaces;
using CleanCode.Application.Services;
using CleanCode.Core.Entities;
using CleanCode.Core.Interfaces;
using Moq;
using Xunit;

namespace CleanCode.Tests.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task IncreaseStockAsync_ProductExists_IncreasesStockAndSaves()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var mockUow = new Mock<IUnitOfWork>();

            var product = new Product { Id = 1, Name = "Widget", Price = 10.0m, Stock = 5 };

            mockRepo.Setup(r => r.GetByIdAsync(product.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            mockUow.SetupGet(u => u.Products).Returns(mockRepo.Object);
            mockUow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var svc = new ProductService(mockUow.Object);

            // Act
            var result = await svc.IncreaseStockAsync(product.Id, 3, CancellationToken.None);

            // Assert
            Assert.True(result);
            Assert.Equal(8, product.Stock);
            mockUow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }


        [Fact]
        public async Task DecreaseStockAsync_NotEnoughStock_ReturnsFalseAndDoesNotSave()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var mockUow = new Mock<IUnitOfWork>();

            var product = new Product { Id = 2, Name = "Gadget", Price = 20.0m, Stock = 2 };

            mockRepo.Setup(r => r.GetByIdAsync(product.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            mockUow.SetupGet(u => u.Products).Returns(mockRepo.Object);

            var svc = new ProductService(mockUow.Object);

            // Act
            var result = await svc.DecreaseStockAsync(product.Id, 5, CancellationToken.None);

            // Assert
            Assert.False(result);
            Assert.Equal(2, product.Stock); // unchanged
            mockUow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }


        [Fact]
        public async Task GetAllAsync_ReturnsAllProducts_FromStubRepository()
        {
            // Arrange: use a stub repository to return deterministic data
            var stub = new StubProductRepository(new[] {
                new Product { Id = 1, Name = "A", Price = 5m, Stock = 1 },
                new Product { Id = 2, Name = "B", Price = 15m, Stock = 2 }
            });

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.SetupGet(u => u.Products).Returns(stub);

            var svc = new ProductService(mockUow.Object);

            // Act
            var list = await svc.GetAllAsync(CancellationToken.None);

            // Assert
            Assert.Equal(2, list.Count);
            Assert.Contains(list, p => p.Name == "A");
            Assert.Contains(list, p => p.Name == "B");
        }


        [Fact]
        public async Task SearchAsync_WithQueryAndMaxPrice_ReturnsFiltered_FromStub()
        {
            // Arrange
            var stub = new StubProductRepository(new[] {
                new Product { Id = 1, Name = "Apple", Price = 2.5m, Stock = 10 },
                new Product { Id = 2, Name = "Orange", Price = 3.0m, Stock = 5 },
                new Product { Id = 3, Name = "ExpensiveApple", Price = 50m, Stock = 1 }
            });

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.SetupGet(u => u.Products).Returns(stub);

            var svc = new ProductService(mockUow.Object);

            // Act: query "apple" with maxPrice 10 should return only the cheap apple
            var results = await svc.SearchAsync("apple", 10m, CancellationToken.None);

            // Assert
            Assert.Single(results);
            Assert.Equal("Apple", results[0].Name);
        }


        [Fact]
        public async Task AddAsync_CallsAddAndSaves_OnRepository()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var mockUow = new Mock<IUnitOfWork>();

            mockUow.SetupGet(u => u.Products).Returns(mockRepo.Object);
            mockUow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var svc = new ProductService(mockUow.Object);
            var newProduct = new Product { Id = 10, Name = "New", Price = 7m, Stock = 0 };

            // Act
            await svc.AddAsync(newProduct, CancellationToken.None);

            // Assert
            mockRepo.Verify(r => r.Add(newProduct), Times.Once);
            mockUow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }


        private class StubProductRepository : IProductRepository
        {
            private readonly List<Product> _items;

            public StubProductRepository(IEnumerable<Product> items)
            {
                _items = new List<Product>(items);
            }

            public Task<List<Product>> GetAllAsync(CancellationToken ct)
            {
                return Task.FromResult(_items.ToList());
            }

            public Task<Product?> GetByIdAsync(int id, CancellationToken ct)
            {
                return Task.FromResult(_items.FirstOrDefault(p => p.Id == id));
            }

            public Task<List<Product>> SearchAsync(string? query, decimal? maxPrice, CancellationToken ct)
            {
                IEnumerable<Product> q = _items;
                if (!string.IsNullOrWhiteSpace(query))
                {
                    var lower = query!.ToLowerInvariant();
                    q = q.Where(p => p.Name.ToLowerInvariant().Contains(lower));
                }

                if (maxPrice.HasValue)
                {
                    q = q.Where(p => p.Price <= maxPrice.Value);
                }

                return Task.FromResult(q.ToList());
            }

            public void Add(Product product)
            {
                _items.Add(product);
            }

            public void Update(Product product)
            {
                var idx = _items.FindIndex(p => p.Id == product.Id);
                if (idx >= 0) _items[idx] = product;
            }

            public void Remove(Product product)
            {
                _items.RemoveAll(p => p.Id == product.Id);
            }
        }
    }
}
