using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Exceptions;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.LiteraryCategories.Commands.Update;

public class UpdateLiteraryCategoryCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public class Handler : IRequestHandler<UpdateLiteraryCategoryCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _applicationContext;

        public Handler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<BaseResponseModel<Unit>> Handle(UpdateLiteraryCategoryCommand request, CancellationToken cancellationToken)
        {
            LiteraryCategory? literaryCategory = await _applicationContext.LiteraryCategories
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (literaryCategory == null)
            {
                throw new BadRequestException("Güncellenecek eser kategorisi bulunamadı.");
            }

            literaryCategory.Name = request.Name;
            literaryCategory.Description = request.Description;

            _applicationContext.LiteraryCategories.Update(literaryCategory);
            await _applicationContext.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Eser kategorisi başarıyla güncellendi.");
        }
    }
}