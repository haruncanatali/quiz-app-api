using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Application.LiteraryCategories.Queries.Dtos;

namespace QuizApp.Application.LiteraryCategories.Queries.GetLiteraryCategories;

public class GetLiteraryCategoriesQueryHandler : IRequestHandler<GetLiteraryCategoriesQuery, BaseResponseModel<List<LiteraryCategoryDto>>>
{
    private readonly IApplicationContext _applicationContext;
    private readonly IMapper _mapper;

    public GetLiteraryCategoriesQueryHandler(IApplicationContext applicationContext, IMapper mapper)
    {
        _applicationContext = applicationContext;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<List<LiteraryCategoryDto>>> Handle(GetLiteraryCategoriesQuery request, CancellationToken cancellationToken)
    {
        List<LiteraryCategoryDto> literaryCategories = await _applicationContext.LiteraryCategories
            .Where(c => (request.Name == null || c.Name.ToLower().Contains(request.Name.ToLower())))
            .Include(c => c.Literaries)
            .ProjectTo<LiteraryCategoryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return BaseResponseModel<List<LiteraryCategoryDto>>.Success(literaryCategories, "Veriler başarıyla çekildi.");
    }
}