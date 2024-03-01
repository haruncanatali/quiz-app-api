using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Exceptions;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.Authors.Commands.Delete;

public class DeleteAuthorCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<DeleteAuthorCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _applicationContext;

        public Handler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            Author? author = await _applicationContext.Authors
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (author == null)
            {
                throw new BadRequestException("Yazar bulunamadı.");
            }

            List<Literary> literaries = await _applicationContext.Literaries
                .Where(c => c.AuthorId == request.Id)
                .ToListAsync(cancellationToken);
            _applicationContext.Literaries.RemoveRange(literaries);
            await _applicationContext.SaveChangesAsync(cancellationToken);

            _applicationContext.Authors.Remove(author);
            await _applicationContext.SaveChangesAsync(cancellationToken);
            
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Yazar başarıyla silindi.");
        }
    }
}