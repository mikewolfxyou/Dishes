using Autofac;
using AutofacSerilogIntegration;
using DishesApi.DataAccess;
using DishesApi.DataAccess.Dish;
using DishesApi.DataAccess.Restaurant;
using DishesApi.Entities;
using DishesApi.Infrastructure;
using DishesApi.Repositories;
using DishesApi.Services.Validators;
using Microsoft.Extensions.Configuration;

namespace DishesApi
{
    public static class AutofacConfigurator
    {
        public static void Configure(IConfiguration configuration, ContainerBuilder builder)
        {
            builder.RegisterType<RestaurantDao>().As<IRestaurantDao>().SingleInstance();
            builder.RegisterType<DishDao>().As<IDishDao>().SingleInstance();
            
            builder.RegisterType<RestaurantRepository>().As<IRestaurantRepository>();
            builder.RegisterType<DishRepository>().As<IDishRepository>();
            builder.RegisterType<MenuRepository>().As<IMenuRepository>();
            
            builder.RegisterType<EnumerableToBsonArrayTransformer<DishAvailableMeal>>()
                .As<IEnumerableToBsonArrayTransformer<DishAvailableMeal>>();
            
            builder.RegisterType<EnumerableToBsonArrayTransformer<DishAvailableDay>>()
                .As<IEnumerableToBsonArrayTransformer<DishAvailableDay>>();

            builder.RegisterLogger();

            builder.RegisterType<RestaurantValidator>();
            builder.RegisterType<DishValidator>();
            
            builder.RegisterInstance(configuration).As<IConfiguration>();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().SingleInstance();
        }
    }
}