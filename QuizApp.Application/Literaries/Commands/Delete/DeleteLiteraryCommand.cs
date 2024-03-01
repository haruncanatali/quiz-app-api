using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Exceptions;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.Literaries.Commands.Delete;

public class DeleteLiteraryCommand : IRequest<BaseResponseModel<Unit>>
{
    public long Id { get; set; }
    
    public class Handler : IRequestHandler<DeleteLiteraryCommand, BaseResponseModel<Unit>>
    {
        private readonly IApplicationContext _applicationContext;

        public Handler(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<BaseResponseModel<Unit>> Handle(DeleteLiteraryCommand request, CancellationToken cancellationToken)
        {
            Literary? literary = await _applicationContext.Literaries
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken );

            if (literary == null)
            {
                throw new BadRequestException("Eser bulunamadı.");
            }

            _applicationContext.Literaries.Remove(literary);
            await _applicationContext.SaveChangesAsync(cancellationToken);
            
            return BaseResponseModel<Unit>.Success(Unit.Value, "Eser başarıyla silindi.");
        }
    }
}