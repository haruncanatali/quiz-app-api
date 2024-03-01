using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Application.CommonValues.Dtos;

namespace QuizApp.Application.CommonValues.Queries.GetLiteraryCategoriesPartial;

public class GetLiteraryCategoriesPartialQueryHandler : IRequestHandler<GetLiteraryCategoriesPartialQuery, BaseResponseModel<List<CommonValue>>>
{
    private readonly IApplicationContext _applicationContext;

    public GetLiteraryCategoriesPartialQueryHandler(IApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }

    public async Task<BaseResponseModel<List<CommonValue>>> Handle(GetLiteraryCategoriesPartialQuery request, CancellationToken cancellationToken)
    {
        List<CommonValue> result = await _applicationContext.LiteraryCategories
            .Select(c => new CommonValue
            {
                Value = c.Id,
                Label = c.Name
            })
            .ToListAsync(cancellationToken);
        
        return BaseResponseModel<List<CommonValue>>.Success(result, "Eser kategorileri başarıyla getirildi.");
    }
}