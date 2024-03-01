using MediatR;

namespace QuizApp.Application.Users.Queries.GetUsersList
{
    public class GetUserListQuery : IRequest<UserListVm>
    {
        public string? FullName { get; set; }
    }
}
