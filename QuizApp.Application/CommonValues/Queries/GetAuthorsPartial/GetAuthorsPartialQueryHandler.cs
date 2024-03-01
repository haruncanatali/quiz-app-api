using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Application.CommonValues.Dtos;

namespace QuizApp.Application.CommonValues.Queries.GetAuthorsPartial;

public class GetAuthorsPartialQueryHandler : IRequestHandler<GetAuthorsPartialQuery, BaseResponseModel<List<CommonValue>>>
{
    private readonly IApplicationContext _applicationContext;

    public GetAuthorsPartialQueryHandler(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<BaseResponseModel<List<CommonValue>>> Handle(GetAuthorsPartialQuery request, CancellationToken cancellationToken)
    {
        List<CommonValue> result = await _applicationContext.Authors
            .Select(c => new CommonValue
            {
                Value = c.Id,
                Label = c.FullName
            })
            .ToListAsync(cancellationToken);
        
        return BaseResponseModel<List<CommonValue>>.Success(result, "Yazar seçenekleri başarıyla getirildi.");
    }
}