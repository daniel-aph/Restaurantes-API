using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantCommandHandlerTests
{
    private Mock<ILogger<CreateRestaurantCommandHandler>> _loggerMock { get; set; }
    private Mock<IMapper> _mapperMock { get; set; }
    private Mock<IUserContext> _userContextMock { get; set; }
    private Mock<IRestaurantsRepository> _restaurantRepositoryMock { get; set; }

    public CreateRestaurantCommandHandlerTests()
    {
        _loggerMock = new ();
        _mapperMock = new ();
        _userContextMock = new ();
        _restaurantRepositoryMock = new ();
    }

    [Fact()]
    public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
    {
        // Arrange
        var command = new CreateRestaurantCommand();
        var restaurant = new Restaurant();
        _mapperMock.Setup(m => m.Map<Restaurant>(command)).Returns(restaurant);

        _restaurantRepositoryMock
            .Setup(repo => repo.Create(It.IsAny<Restaurant>()))
            .ReturnsAsync(1);

        var currentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);
        _userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

        var commandHandler = new CreateRestaurantCommandHandler(
            _loggerMock.Object,
            _mapperMock.Object,
            _restaurantRepositoryMock.Object,
            _userContextMock.Object);

        // Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(1);
        restaurant.OwnerId.Should().Be(currentUser.Id);
        _restaurantRepositoryMock.Verify(r => r.Create(restaurant), Times.Once);
    }
}