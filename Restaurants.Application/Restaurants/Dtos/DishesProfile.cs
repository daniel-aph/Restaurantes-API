using AutoMapper;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class DishesProfile : Profile
{
    public DishesProfile() 
    {
        CreateMap<Dish, DishDto>();
    }
}
