using MediatR;
using QuizApp.Application.Common.Helpers;
using QuizApp.Application.Common.Models;
using QuizApp.Application.CommonValues.Dtos;
using QuizApp.Domain.Enums;

namespace QuizApp.Application.CommonValues.Queries.GetGenders;

public class GetGendersQueryHandler : IRequestHandler<GetGendersQuery, BaseResponseModel<List<CommonValue>>>
{
    public async Task<BaseResponseModel<List<CommonValue>>> Handle(GetGendersQuery request, CancellationToken cancellationToken)
    {
        List<CommonValue> result = Enum.GetValues<Gender>().Select(c => new CommonValue
        {
            Label = c.GetDescription(),
            Value = ((int)c)
        }).ToList();
        
        return BaseResponseModel<List<CommonValue>>.Success(result, "Cinsiyet seçenekleri başarıyla getirildi.");
    }
}