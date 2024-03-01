using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Application.CommonValues.Dtos;

namespace QuizApp.Application.CommonValues.Queries.GetPeriodsPartial;

public class GetPeriodsPartialQueryHandler : IRequestHandler<GetPeriodsPartialQuery, BaseResponseModel<List<CommonValue>>>
{
    private readonly IApplicationContext _applicationContext;

    public GetPeriodsPartialQueryHandler(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<BaseResponseModel<List<CommonValue>>> Handle(GetPeriodsPartialQuery request, CancellationToken cancellationToken)
    {
        List<CommonValue> result = await _applicationContext.Periods
            .Select(c => new CommonValue
            {
                Value = c.Id,
                Label = c.Name
            })
            .ToListAsync(cancellationToken);
        
        return BaseResponseModel<List<CommonValue>>.Success(result, "Dönemler başarıyla getirildi.");
    }
}