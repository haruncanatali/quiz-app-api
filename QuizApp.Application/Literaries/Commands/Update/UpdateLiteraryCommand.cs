using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Exceptions;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.Literaries.Commands.Update;

public class UpdateLiteraryCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long AuthorId { get; set; }
    public long LiteraryCategoryId { get; set; }
    public long PeriodId { get; set; }
    
    public class Handler : IRequestHandler<UpdateLiteraryCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _applicationContext;

        public Handler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<BaseResponseModel<Unit>> Handle(UpdateLiteraryCommand request, CancellationToken cancellationToken)
        {
            Literary? literary = await _applicationContext.Literaries
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (literary == null)
            {
                throw new BadRequestException("Eser bulunamadı.");
            }

            literary.Name = request.Name;
            literary.Description = request.Description;
            literary.AuthorId = request.AuthorId;
            literary.LiteraryCategoryId = request.LiteraryCategoryId;
            literary.PeriodId = request.PeriodId;

            _applicationContext.Literaries.Update(literary);
            await _applicationContext.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Eser başarıyla güncellendi.");
        }
    }
}