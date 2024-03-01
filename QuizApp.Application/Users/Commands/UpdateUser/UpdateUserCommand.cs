using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizApp.Application.Common.Exceptions;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Managers;
using QuizApp.Application.Common.Models;
using QuizApp.Domain.Enums;
using QuizApp.Domain.Identity;

namespace QuizApp.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<BaseResponseModel<long>>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public IFormFile? ProfilePhoto { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }

        public class Handler : IRequestHandler<UpdateUserCommand, BaseResponseModel<long>>
        {
            private readonly UserManager<User> _userManager;
            private readonly RoleManager<Role> _roleManager;
            private readonly IApplicationContext _context;
            private readonly FileManager _fileManager;
            private readonly ILogger<UpdateUserCommand> _logger;

            public Handler(IApplicationContext context, FileManager fileManager, UserManager<User> userManager, ILogger<UpdateUserCommand> logger, RoleManager<Role> roleManager)
            {
                _context = context;
                _fileManager = fileManager;
                _userManager = userManager;
                _logger = logger;
                _roleManager = roleManager;
            }

            public async Task<BaseResponseModel<long>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    User? entity = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
                    string profilePhoto = entity.ProfilePhoto;
                    if (request.ProfilePhoto != null)
                    {
                        profilePhoto = _fileManager.Upload(request.ProfilePhoto, FileRoot.UserProfile);
                    }

                    entity.FirstName = request.FirstName;
                    entity.Surname = request.Surname;
                    entity.Email = request.Email;
                    entity.Gender = request.Gender;
                    entity.ProfilePhoto = profilePhoto;
                    entity.PhoneNumber = request.Phone;
                    entity.Birthdate = request.Birthdate;
                    
                    await _userManager.UpdateAsync(entity);

                    if (!string.IsNullOrEmpty(request.Password))
                    {
                        var removeResult = await _userManager.RemovePasswordAsync(entity);
                        if (removeResult.Succeeded)
                        {
                            await _userManager.AddPasswordAsync(entity, request.Password);
                        }
                        else
                        {
                            throw new BadRequestException(
                                $"(UUC-0) Kullanıcı güncellenirken şifre değiştirme sırasında hata meydana geldi.");
                        }
                    }
                    
                    return BaseResponseModel<long>.Success(entity.Id,$"Kullanıcı başarıyla oluşturuldu. Username: {request.Email}");
                }
                catch (Exception e)
                {
                    throw new BadRequestException(
                        $"({request.Email}) Kullanıcı oluşturulurken hata meydana geldi. Hata: {e.Message}");
                }
            }
        }
    }
}