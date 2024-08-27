using Microsoft.AspNetCore.Authorization;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class MinimumRestaurantsOwnedRequirement(int minimumRestaurants) : IAuthorizationRequirement
{
    public int MinimumRestaurants { get; } = minimumRestaurants;
}
