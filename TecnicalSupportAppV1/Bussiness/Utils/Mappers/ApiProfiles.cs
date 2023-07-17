using AutoMapper;
using TecnicalSupportAppV1.Api.Models;
using TecnicalSupportAppV1.Api.Models.Dtos;

namespace TecnicalSupportAppV1.Bussiness.Utils.Mappers
{
    public class ApiProfiles : Profile
    {
        public ApiProfiles()
        {
            // Admin
            CreateMap<AdminCreationDto, Admin>();
            CreateMap<AdminDto, Admin>();
            CreateMap<Admin, AdminDto>();
            CreateMap<AdminUpdateDto, Admin>();

            // Stock
            CreateMap<StockCreationDto, Stock>();
            CreateMap<StockDto, Stock>();
            CreateMap<Stock, StockDto>();
            CreateMap<StockUpdateDto, Stock>();
            CreateMap<Stock, StockTechnicianDto>();
            CreateMap<StockTechnicianDto, Stock>();
            CreateMap<NotesCreationDto, Notes>();
            CreateMap<Notes, NotesCreationDto>();

            // Client
            CreateMap<ClientCreationDto, Client>();
            CreateMap<ClientDto, Client>();
            CreateMap<Client, ClientDto>();

            // Technician
            CreateMap<TechnicianCreationDto, Technician>();
            CreateMap<TechnicianDto, Technician>();
            CreateMap<Technician, TechnicianDto>();
            CreateMap<TechnicianUpdateDto, Technician>();
            
            //User
            CreateMap<UserCreationDto, User>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();

            //Office
            CreateMap<OfficeCreationDto, Office>();
            CreateMap<OfficeDto, Office>();
            CreateMap<Office, OfficeDto>();

            //ServiceOrder
            CreateMap<ServiceOrderCreationDto, ServiceOrder>();
            CreateMap<ServiceOrderUpdateDto, ServiceOrder>();
            CreateMap<ServiceOrderDto, ServiceOrder>();
            CreateMap<ServiceOrder, ServiceOrderDto>();

            //Item
            CreateMap<ItemCreationDto, Item>();
            CreateMap<ItemDto, Item>();
            CreateMap<Item, ItemDto>();
            CreateMap<Item, ItemStockDto>();
            CreateMap<ItemStockDto, Item>();


            //Notes
            CreateMap<NotesCreationDto, Notes>();

            //Contact
            CreateMap<ContactInformationCreationDto, ContactInformation>();
            CreateMap<ContactInformationDto, ContactInformation>();
            CreateMap<ContactInformation, ContactInformationDto>();
        }
    }
}
