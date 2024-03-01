using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Exceptions;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.Periods.Commands.Update;

public class UpdatePeriodCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public class Handler : IRequestHandler<UpdatePeriodCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _applicationContext;

        public Handler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<BaseResponseModel<Unit>> Handle(UpdatePeriodCommand request, CancellationToken cancellationToken)
        {
            Period? period = await _applicationContext.Periods
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (period == null)
            {
                throw new BadRequestException("Dönem bulunamadı.");
            }

            period.Name = request.Name;
            period.Description = request.Description;

            _applicationContext.Periods.Update(period);
            await _applicationContext.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Dönem başarıyla güncellendi.");
        }
    }
}