using MediatR;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.LiteraryCategories.Commands.Create;

public class CreateLiteraryCategoryCommand : IRequest<BaseResponseModel<Unit>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public class Handler : IRequestHandler<CreateLiteraryCategoryCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _applicationContext;

        public Handler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<BaseResponseModel<Unit>> Handle(CreateLiteraryCategoryCommand request, CancellationToken cancellationToken)
        {
            await _applicationContext.LiteraryCategories.AddAsync(new LiteraryCategory
            {
                Name = request.Name,
                Description = request.Description
            });

            await _applicationContext.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Eser kategorisi başarıyla eklendi.");
        }
    }
}