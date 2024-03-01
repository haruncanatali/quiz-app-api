using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Application.Literaries.Queries.Dtos;

namespace QuizApp.Application.Literaries.Queries.GetLiteraries;

public class GetLiterariesQueryHandler : IRequestHandler<GetLiterariesQuery, BaseResponseModel<List<LiteraryDto>>>
{
    private readonly IApplicationContext _applicationContext;
    private readonly IMapper _mapper;

    public GetLiterariesQueryHandler(IApplicationContext applicationContext, IMapper mapper)
    {
        _applicationContext = applicationContext;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<List<LiteraryDto>>> Handle(GetLiterariesQuery request, CancellationToken cancellationToken)
    {
        List<LiteraryDto> literaries = await _applicationContext.Literaries
            .Where(c => (request.Name == null || c.Name.ToLower().Contains(request.Name.ToLower())))
            .Include(c => c.Author)
            .Include(c => c.Period)
            .Include(c => c.LiteraryCategory)
            .ProjectTo<LiteraryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return BaseResponseModel<List<LiteraryDto>>.Success(literaries, "Eserler başarıyla getirildi.");
    }
}