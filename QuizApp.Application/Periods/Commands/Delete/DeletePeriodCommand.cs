using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Exceptions;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.Periods.Commands.Delete;

public class DeletePeriodCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<DeletePeriodCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _applicationContext;

        public Handler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeletePeriodCommand request, CancellationToken cancellationToken)
        {
            Period? period = await _applicationContext.Periods
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (period == null)
            {
                throw new BadRequestException("Dönem bulunamadı.");
            }

            List<Literary> literaries = await _applicationContext.Literaries
                .Where(c => c.PeriodId == request.Id)
                .ToListAsync(cancellationToken);
            _applicationContext.Literaries.RemoveRange(literaries);
            await _applicationContext.SaveChangesAsync(cancellationToken);

            List<Author> authors = await _applicationContext.Authors
                .Where(c => c.PeriodId == request.Id)
                .ToListAsync(cancellationToken);
            _applicationContext.Authors.RemoveRange(authors);
            await _applicationContext.SaveChangesAsync(cancellationToken);

            _applicationContext.Periods.Remove(period);
            await _applicationContext.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value,"Dönem başarıyla silindi.");
        }
    }
}