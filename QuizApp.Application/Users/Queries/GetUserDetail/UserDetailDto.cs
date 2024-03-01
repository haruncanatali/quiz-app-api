using AutoMapper;
using QuizApp.Application.Common.Mappings;
using QuizApp.Domain.Enums;
using QuizApp.Domain.Identity;

namespace QuizApp.Application.Users.Queries.GetUserDetail
{
    public class UserDetailDto : IMapFrom<User>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string ProfilePhoto { get; set; }
        public string PhoneNumber { get; set; }
        public string Roles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDetailDto>();
        }
    }
}