using AutoMapper;
using QuizApp.Application.Common.Helpers;
using QuizApp.Application.Common.Mappings;
using QuizApp.Domain.Identity;

namespace QuizApp.Application.Users.Queries.GetUsersList
{
    public class UserDto : IMapFrom<User>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string ProfilePhoto { get; set; }
        public string FirstAndLastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>()
                .ForMember(dest => dest.FirstAndLastName,
                    opt => opt.MapFrom(x => string.Concat(x.FirstName, " ", x.Surname)))
                .ForMember(dest => dest.Gender,
                    opt => opt.MapFrom(x => x.Gender.GetDescription()));
        }
    }
}