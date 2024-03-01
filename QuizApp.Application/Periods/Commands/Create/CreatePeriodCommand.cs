using MediatR;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.Periods.Commands.Create;

public class CreatePeriodCommand : IRequest<BaseResponseModel<Unit>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public class Handler : IRequestHandler<CreatePeriodCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _applicationContext;

        public Handler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<BaseResponseModel<Unit>> Handle(CreatePeriodCommand request, CancellationToken cancellationToken)
        {
            await _applicationContext.Periods.AddAsync(new Period
            {
                Name = request.Name,
                Description = request.Description
            });

            await _applicationContext.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value,"Dönem başarıyla eklendi.");
        }
    }
}