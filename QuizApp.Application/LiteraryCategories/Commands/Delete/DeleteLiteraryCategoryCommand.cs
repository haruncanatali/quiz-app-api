using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Exceptions;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.LiteraryCategories.Commands.Delete;

public class DeleteLiteraryCategoryCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<DeleteLiteraryCategoryCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _applicationContext;

        public Handler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeleteLiteraryCategoryCommand request, CancellationToken cancellationToken)
        {
            LiteraryCategory? literaryCategory = await _applicationContext.LiteraryCategories
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (literaryCategory == null)
            {
                throw new BadRequestException("Eser kategorisi bulunamadı.");
            }

            List<Literary> literaries = await _applicationContext.Literaries
                .Where(c => c.LiteraryCategoryId == request.Id)
                .ToListAsync(cancellationToken);
            
            _applicationContext.Literaries.RemoveRange(literaries);
            await _applicationContext.SaveChangesAsync(cancellationToken);

            _applicationContext.LiteraryCategories.Remove(literaryCategory);
            await _applicationContext.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Eser dönemi başarıyla silindi.");
        }
    }
}