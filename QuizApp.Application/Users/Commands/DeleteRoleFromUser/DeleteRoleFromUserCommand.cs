using MediatR;
using Microsoft.AspNetCore.Identity;
using QuizApp.Application.Common.Exceptions;
using QuizApp.Application.Common.Models;
using QuizApp.Domain.Identity;

namespace QuizApp.Application.Users.Commands.DeleteRoleFromUser;

public class DeleteRoleFromUserCommand : IRequest<BaseResponseModel<Unit>>
{
    public long UserId { get; set; }
    public long RoleId { get; set; }
    
    public class Handler : IRequestHandler<DeleteRoleFromUserCommand, BaseResponseModel<Unit>>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public Handler(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeleteRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
            {
                throw new BadRequestException(
                    $"İlgili rolden silinmek istenen kullanıcı bulunamadı. ID:{request.UserId}");
            }

            Role? role = await _roleManager.FindByIdAsync(request.RoleId.ToString());

            if (role == null)
            {
                throw new BadRequestException(
                    $"Rolden silinmek istenen kullanıcı için silinecek rol bulunamadı. ID:{request.RoleId}");
            }

            await _userManager.RemoveFromRoleAsync(user, role.Name!);
            return BaseResponseModel<Unit>.Success(Unit.Value,$"Kullanıcı başarıyla {role.Name} rolünden silindi. USERID:{request.UserId}");
        }
    }
}