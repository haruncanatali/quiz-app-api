using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using QuizApp.Application.Common.Exceptions;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Domain.Identity;

namespace QuizApp.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<BaseResponseModel<Unit>>
    {
        public long Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteUserCommand, BaseResponseModel<Unit>>
        {
            private readonly IApplicationContext _context;
            private readonly ICurrentUserService _currentUserService;
            private readonly UserManager<User> _userManager;

            public DeleteCategoryCommandHandler(IApplicationContext context, ICurrentUserService currentUserService, UserManager<User> userManager)
            {
                _context = context;
                _currentUserService = currentUserService;
                _userManager = userManager;
            }

            public async Task<BaseResponseModel<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Users
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new BadRequestException($"Silinmek istenen kullanıcı bulunamadı. ID:{request.Id}");
                }

                await _userManager.DeleteAsync(entity);
                
                await _context.SaveChangesAsync(cancellationToken);
                return BaseResponseModel<Unit>.Success(Unit.Value, $"Kullanıcı başarıyla silindi. ID:{request.Id}");
            }
        }
    }
}
