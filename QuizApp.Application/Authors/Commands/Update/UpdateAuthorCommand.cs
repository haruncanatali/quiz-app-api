using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Exceptions;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Managers;
using QuizApp.Application.Common.Models;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Enums;

namespace QuizApp.Application.Authors.Commands.Update;

public class UpdateAuthorCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Bio { get; set; }
    public long PeriodId { get; set; }
    public IFormFile? Photo { get; set; }
    
    public class Handler : IRequestHandler<UpdateAuthorCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _applicationContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly FileManager _fileManager;

        public Handler(IApplicationContext applicationContext, ICurrentUserService currentUserService, FileManager fileManager)
        {
            _applicationContext = applicationContext;
            _currentUserService = currentUserService;
            _fileManager = fileManager;
        }

        public async Task<BaseResponseModel<Unit>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            Author? author = await _applicationContext.Authors
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (author == null)
            {
                throw new BadRequestException("Yazar bulunamadı.");
            }

            author.Name = request.Name;
            author.Surname = request.Surname;
            author.Bio = request.Bio;
            author.PeriodId = request.PeriodId;

            if (request.Photo != null)
            {
                await _applicationContext.CreateBeginTransactionForFileAsync(_applicationContext.Authors, author,
                    request.Photo, FileRoot.AuthorProfile, _currentUserService, _fileManager,EntityState.Modified);
            }
            else
            {
                _applicationContext.Authors.Update(author);
                await _applicationContext.SaveChangesAsync(cancellationToken);
            }
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Yazar başarıyla güncellendi.");
        }
    }
}