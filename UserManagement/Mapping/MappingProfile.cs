using AutoMapper;


namespace UserManagement.Mapping
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<DTO.CreateUserRequest, Model.User>();
            CreateMap<Model.User, DTO.UserResponse>();
        }
        
    }
}
