using AutoMapper;
using EventPlanning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPlanning.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(config => {
                config.CreateMap<EventViewModel, MyEvent>();
                config.CreateMap<MyEvent, EventViewModel>();

                config.CreateMap<MyEventViewModel, MyEvent>();
                config.CreateMap<MyEvent, MyEventViewModel>();

                config.CreateMap<MyEventViewModel, EventViewModel>();
                config.CreateMap<EventViewModel, MyEventViewModel>();
                //config.CreateMap<Product, ProductViewModel>();
                //config.CreateMap<ProductViewModel, Product>();

                //config.CreateMap<Category, CategoryViewModel>();
                //config.CreateMap<CategoryViewModel, Category>();

                //config.CreateMap<Brand, BrandViewModel>();
                //config.CreateMap<BrandViewModel, Brand>();

                //config.CreateMap<BasketListViewModel, IEnumerable<Basket>>();
                //config.CreateMap<IEnumerable<Basket>, BasketListViewModel>();

                //config.CreateMap<BasketListViewModel, IEnumerable<Order>>();
                //config.CreateMap<IEnumerable<Order>, BasketListViewModel>();

                //config.CreateMap<Product, BasketProductViewModel>();
                //config.CreateMap<BasketProductViewModel, Product>();

                //config.CreateMap<Order, Basket>();
                //config.CreateMap<Basket, Order>();

                //config.CreateMap<List<Order>, List<Basket>>();
                //config.CreateMap<List<Basket>, List<Order>>();

                //config.CreateMap<ShippingDetailsViewModel, ShippingDetails>();
                //config.CreateMap<ShippingDetails, ShippingDetailsViewModel>();


                ////identity

                //config.CreateMap<LoginViewModel, UserDTO>();
                //config.CreateMap<UserDTO, LoginViewModel>();

                //config.CreateMap<UserProfile, UserDTO>();
                //config.CreateMap<UserDTO, UserProfile>();

                //config.CreateMap<ApplicationUser, UserDTO>();
                //config.CreateMap<UserDTO, ApplicationUser>();

                //config.CreateMap<RegisterModel, UserDTO>();
                //config.CreateMap<UserDTO, RegisterModel>();

                //config.CreateMap<ShippingDetails, UserDTO>();
                //config.CreateMap<UserDTO, ShippingDetails>();

                //config.CreateMap<ShippingDetailsViewModel, UserDTO>();
                //config.CreateMap<UserDTO, ShippingDetailsViewModel>();

                //config.CreateMap<ShippingDetailsViewModel, ApplicationUser>();
                //config.CreateMap<ApplicationUser, ShippingDetailsViewModel>();

                //config.CreateMap<ShippingDetailsViewModel, UserProfile>();
                //config.CreateMap<UserProfile, ShippingDetailsViewModel>();







                //config.CreateMap<NomenclatureDTO, NomenclatureViewModel>()
                //.ForMember("Category", x => x.MapFrom(z => z.Category.Name))
                //.ForMember("Manufacturer", x => x.MapFrom(z => z.Manufacturer.Name))
                //.ForMember("Characteristic", x => x.MapFrom(z => Characteristic.Deserialization(z.Characteristic)))
                //.ForMember("OrderCharacteristic", x => x.MapFrom(z => Characteristic.Deserialization(z.OrderCharacteristic)));
                //config.CreateMap<NomenclatureViewModel, NomenclatureDTO>()
                //.ForMember("Category", x => x.MapFrom(z => new CategoryNomenclatureDTO z.Category))
                //.ForMember("Manufacturer", x => x.MapFrom(z => z.Manufacturer))
                //.ForMember("Characteristic", x => x.MapFrom(z => Characteristic.Serialization(z.Characteristic)))
                //.ForMember("OrderCharacteristic", x => x.MapFrom(z => Characteristic.Serialization(z.OrderCharacteristic)));
            });
        }
    }
}