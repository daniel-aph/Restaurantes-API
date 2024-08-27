using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteAllForRestaurant;

public class DeleteAllForRestaurantCommand(int restaurantId) : IRequest
{
    public int RestaurantId { get; } = restaurantId;
}
