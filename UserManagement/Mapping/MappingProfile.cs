using AutoMapper;


namespace UserManagement.Mapping
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<DTO.CreateUserRequest, Model.User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src =>  src.Id == Guid.Empty ? Guid.NewGuid() : src.Id));
            CreateMap<Model.User, DTO.UserResponse>();
        }
        
    }
}
