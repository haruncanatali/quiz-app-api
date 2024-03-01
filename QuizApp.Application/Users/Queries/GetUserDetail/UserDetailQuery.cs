using MediatR;

namespace QuizApp.Application.Users.Queries.GetUserDetail
{
    public class UserDetailQuery : IRequest<UserDetailDto>
    {
        public long Id { get; set; }
    }
}