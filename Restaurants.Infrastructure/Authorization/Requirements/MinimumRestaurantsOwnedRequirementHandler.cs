using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements;

internal class MinimumRestaurantsOwnedRequirementHandler(ILogger<MinimumRestaurantsOwnedRequirementHandler> logger,
    IUserContext userContext,
    IRestaurantsRepository restaurantsRepository) : AuthorizationHandler<MinimumRestaurantsOwnedRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantsOwnedRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("User: {Email} [{Id}] - Handling MinimumRestaurantsOwnedRequirement",
            currentUser.Email, currentUser.Id);

        var ownedRestaurants = (await restaurantsRepository.GetAllAsync())
            .Count(r => r.OwnerId == currentUser!.Id);

        if (ownedRestaurants >= requirement.MinimumRestaurants)
        {
            logger.LogInformation("Authorization succeeded");
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}
