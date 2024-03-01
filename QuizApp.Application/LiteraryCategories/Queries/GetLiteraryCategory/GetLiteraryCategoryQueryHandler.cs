using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Application.LiteraryCategories.Queries.Dtos;

namespace QuizApp.Application.LiteraryCategories.Queries.GetLiteraryCategory;

public class GetLiteraryCategoryQueryHandler : IRequestHandler<GetLiteraryCategoryQuery, BaseResponseModel<LiteraryCategoryDto>>
{
    private readonly IApplicationContext _applicationContext;
    private readonly IMapper _mapper;

    public GetLiteraryCategoryQueryHandler(IApplicationContext applicationContext, IMapper mapper)
    {
        _applicationContext = applicationContext;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<LiteraryCategoryDto>> Handle(GetLiteraryCategoryQuery request, CancellationToken cancellationToken)
    {
        LiteraryCategoryDto? literaryCategory = await _applicationContext.LiteraryCategories
            .Where(c => c.Id == request.Id)
            .Include(c => c.Literaries)
            .ProjectTo<LiteraryCategoryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        return BaseResponseModel<LiteraryCategoryDto>.Success(literaryCategory, "Veri başarıyla çekildi.");
    }
}