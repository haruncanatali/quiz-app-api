using MediatR;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.Literaries.Commands.Create;

public class CreateLiteraryCommand : IRequest<BaseResponseModel<Unit>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public long AuthorId { get; set; }
    public long LiteraryCategoryId { get; set; }
    public long PeriodId { get; set; }
    
    public class Handler : IRequestHandler<CreateLiteraryCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _applicationContext;

        public Handler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<BaseResponseModel<Unit>> Handle(CreateLiteraryCommand request, CancellationToken cancellationToken)
        {
            await _applicationContext.Literaries.AddAsync(new Literary
            {
                Name = request.Name,
                AuthorId = request.AuthorId,
                LiteraryCategoryId = request.LiteraryCategoryId,
                PeriodId = request.PeriodId,
                Description = request.Description
            }, cancellationToken);

            await _applicationContext.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Eser başarıyla eklendi.");
        }
    }
}