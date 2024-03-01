using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using QuizApp.Application.Common.Exceptions;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Managers;
using QuizApp.Application.Common.Models;
using QuizApp.Domain.Enums;
using QuizApp.Domain.Identity;


namespace QuizApp.Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<BaseResponseModel<long>>
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? RoleName { get; set; }

        public class Handler : IRequestHandler<CreateUserCommand, BaseResponseModel<long>>
        {
            private readonly UserManager<User> _userManager;
            private readonly RoleManager<Role> _roleManager;
            private readonly IApplicationContext _context;
            private readonly FileManager _fileManager;

            public Handler(UserManager<User> userManager, RoleManager<Role> roleManager, FileManager fileManager,
                IApplicationContext context)
            {
                _userManager = userManager;
                _roleManager = roleManager;
                _fileManager = fileManager;
                _context = context;
            }

            public async Task<BaseResponseModel<long>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    bool dublicateControl = _context.Users.Any(x => x.Email == request.Email);
                    if (dublicateControl)
                    {
                        throw new BadRequestException("Eklenmek istenen kullanıcı mükerrer kontrolden geçemedi. Aynı kullanıcı adı veya E-Posta'ya sahip kullanıcı sistemde mevcut görünüyor.");
                    }
                    
                    User entity = new()
                    {
                        FirstName = request.FirstName,
                        Surname = request.Surname,
                        UserName = request.Email,
                        Email = request.Email,
                        Gender = request.Gender,
                        ProfilePhoto = FileRoot.UserProfile + "/default_user.jpeg",
                        Birthdate = request.Birthdate,
                        RefreshToken = String.Empty,
                        RefreshTokenExpiredTime = DateTime.Now,
                        PhoneNumber = request.Phone
                    };

                    var response = await _userManager.CreateAsync(entity, request.Password);

                    if (response.Succeeded)
                    {
                        if (request.RoleName != null)
                        {
                            Role? role = await _roleManager.FindByNameAsync(request.RoleName);
                    
                            if (role != null)
                            {
                                await _userManager.AddToRoleAsync(entity, request.RoleName);
                            }
                            else
                            {
                                await _userManager.AddToRoleAsync(entity, "Normal");
                            }
                        }
                        else
                        {
                            await _userManager.AddToRoleAsync(entity, "Admin");
                        }
                    }
                    else
                    {
                        throw new BadRequestException("Kullanıcı eklenirken hata meydana geldi.");
                    }
                    
                    
                    return BaseResponseModel<long>.Success(entity.Id, $"Kullanıcı başarıyla eklendi. Username:{request.Email}");
                }
                catch (Exception e)
                {
                    throw new BadRequestException($"Kullanıcı ({request.Email}) eklenirken hata meydana geldi. Hata: {e.Message}");
                }
            }
        }
    }
}