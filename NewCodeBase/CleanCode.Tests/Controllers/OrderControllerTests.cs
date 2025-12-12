using AutoMapper;
using CleanCode.Api.Controllers;
using CleanCode.Application.Dtos;
using CleanCode.Application.Interfaces;
using CleanCode.Application.Models;
using CleanCode.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CleanCode.Tests.Controllers
{
    public class OrderControllerTests
    {
        private readonly Mock<IServiceFacade> _mockServiceFacade;
        private readonly Mock<IMapper> _mockMapper;
        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            _mockServiceFacade = new Mock<IServiceFacade>();
            _mockMapper = new Mock<IMapper>();
            _controller = new OrderController(_mockServiceFacade.Object, _mockMapper.Object);
        }


        [Fact]
        public async Task CreateOrder_WithValidToken_ReturnsOkResultWithOrderResponse()
        {
            // Arrange
            const string token = "valid-token";
            const int userId = 1;
            var user = new User { Id = userId, Username = "testuser", Email = "test@example.com" };
            var orderResponse = new OrderResponse { OrderId = 1, OrderTotal = 100.00m };

            _mockServiceFacade.Setup(s => s.UserService.GetByTokenAsync(token, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            _mockServiceFacade.Setup(s => s.OrderService.CreateOrderForUserAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(orderResponse);

            // Act
            var result = await _controller.CreateOrder(token, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(orderResponse, okResult.Value);

            _mockServiceFacade.Verify(s => s.UserService.GetByTokenAsync(token, It.IsAny<CancellationToken>()), Times.Once);
            _mockServiceFacade.Verify(s => s.OrderService.CreateOrderForUserAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
        }


        [Fact]
        public async Task CreateOrder_WithInvalidToken_ReturnsUnauthorized()
        {
            // Arrange
            const string invalidToken = "invalid-token";

            _mockServiceFacade.Setup(s => s.UserService.GetByTokenAsync(invalidToken, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _controller.CreateOrder(invalidToken, CancellationToken.None);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result.Result);
            Assert.Equal(401, unauthorizedResult.StatusCode);

            _mockServiceFacade.Verify(s => s.UserService.GetByTokenAsync(invalidToken, It.IsAny<CancellationToken>()), Times.Once);
            _mockServiceFacade.Verify(s => s.OrderService.CreateOrderForUserAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
        }


        [Fact]
        public async Task CreateOrder_WithEmptyToken_ReturnsUnauthorized()
        {
            // Arrange
            string emptyToken = string.Empty;

            _mockServiceFacade.Setup(s => s.UserService.GetByTokenAsync(emptyToken, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _controller.CreateOrder(emptyToken, CancellationToken.None);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result.Result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }


        [Fact]
        public async Task CreateOrder_CallsOrderServiceWithCorrectUserId()
        {
            // Arrange
            const string token = "valid-token";
            const int userId = 42;
            var user = new User { Id = userId, Username = "testuser", Email = "test@example.com" };
            var orderResponse = new OrderResponse { OrderId = 10, OrderTotal = 250.50m };

            _mockServiceFacade.Setup(s => s.UserService.GetByTokenAsync(token, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            _mockServiceFacade.Setup(s => s.OrderService.CreateOrderForUserAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(orderResponse);

            // Act
            await _controller.CreateOrder(token, CancellationToken.None);

            // Assert
            _mockServiceFacade.Verify(
                s => s.OrderService.CreateOrderForUserAsync(userId, It.IsAny<CancellationToken>()),
                Times.Once);
        }


        [Fact]
        public async Task GetById_WithValidTokenAndOrder_ReturnsOkResultWithOrderDto()
        {
            // Arrange
            const string token = "valid-token";
            const int userId = 1;
            const int orderId = 5;

            var user = new User { Id = userId, Username = "testuser", Email = "test@example.com" };
            var order = new Order { Id = orderId, UserId = userId, CreatedAt = DateTime.UtcNow, Total = 150.00m };
            var orderDto = new OrderDto { Id = orderId, UserId = userId, CreatedAt = order.CreatedAt, Total = 150.00m };

            _mockServiceFacade.Setup(s => s.UserService.GetByTokenAsync(token, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            _mockServiceFacade.Setup(s => s.OrderService.GetByIdAsync(orderId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(order);
            _mockMapper.Setup(m => m.Map<OrderDto>(order)).Returns(orderDto);

            // Act
            var result = await _controller.GetById(orderId, token, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(orderDto, okResult.Value);

            _mockServiceFacade.Verify(s => s.UserService.GetByTokenAsync(token, It.IsAny<CancellationToken>()), Times.Once);
            _mockServiceFacade.Verify(s => s.OrderService.GetByIdAsync(orderId, It.IsAny<CancellationToken>()), Times.Once);
            _mockMapper.Verify(m => m.Map<OrderDto>(order), Times.Once);
        }


        [Fact]
        public async Task GetById_WithInvalidToken_ReturnsUnauthorized()
        {
            // Arrange
            const string invalidToken = "invalid-token";
            const int orderId = 5;

            _mockServiceFacade.Setup(s => s.UserService.GetByTokenAsync(invalidToken, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _controller.GetById(orderId, invalidToken, CancellationToken.None);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result.Result);
            Assert.Equal(401, unauthorizedResult.StatusCode);

            _mockServiceFacade.Verify(s => s.UserService.GetByTokenAsync(invalidToken, It.IsAny<CancellationToken>()), Times.Once);
            _mockServiceFacade.Verify(s => s.OrderService.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
        }


        [Fact]
        public async Task GetById_WithValidTokenButNonExistentOrder_ReturnsNotFound()
        {
            // Arrange
            const string token = "valid-token";
            const int userId = 1;
            const int orderId = 999;

            var user = new User { Id = userId, Username = "testuser", Email = "test@example.com" };

            _mockServiceFacade.Setup(s => s.UserService.GetByTokenAsync(token, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            _mockServiceFacade.Setup(s => s.OrderService.GetByIdAsync(orderId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Order?)null);

            // Act
            var result = await _controller.GetById(orderId, token, CancellationToken.None);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);

            _mockServiceFacade.Verify(s => s.OrderService.GetByIdAsync(orderId, It.IsAny<CancellationToken>()), Times.Once);
        }


        [Fact]
        public async Task GetById_WithEmptyToken_ReturnsUnauthorized()
        {
            // Arrange
            string emptyToken = string.Empty;
            const int orderId = 5;

            _mockServiceFacade.Setup(s => s.UserService.GetByTokenAsync(emptyToken, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User?)null);

            // Act
            var result = await _controller.GetById(orderId, emptyToken, CancellationToken.None);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result.Result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }
    }
}
