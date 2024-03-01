using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Managers;
using QuizApp.Application.Common.Models;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Enums;

namespace QuizApp.Application.Authors.Commands.Create;

public class CreateAuthorCommand : IRequest<BaseResponseModel<Unit>>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Bio { get; set; }
    public long PeriodId { get; set; }
    public IFormFile Photo { get; set; }
    
    public class Handler : IRequestHandler<CreateAuthorCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _applicationContext;
        private readonly FileManager _fileManager;
        private readonly ICurrentUserService _currentUserService;

        public Handler(IApplicationContext applicationContext, FileManager fileManager, ICurrentUserService currentUserService)
        {
            _applicationContext = applicationContext;
            _fileManager = fileManager;
            _currentUserService = currentUserService;
        }

        public async Task<BaseResponseModel<Unit>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var entity = new Author
            {
                Name = request.Name,
                Surname = request.Surname,
                Bio = request.Bio,
                PeriodId = request.PeriodId
            };
            
            await _applicationContext.CreateBeginTransactionForFileAsync(_applicationContext.Authors, entity, request.Photo,
                FileRoot.AuthorProfile, _currentUserService, _fileManager,EntityState.Added);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Yazar başarıyla eklendi.");
        }
    }
}