using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Application.Literaries.Queries.Dtos;

namespace QuizApp.Application.Literaries.Queries.GetLiterary;

public class GetLiteraryQueryHandler : IRequestHandler<GetLiteraryQuery, BaseResponseModel<LiteraryDto>>
{
    private readonly IApplicationContext _applicationContext;
    private readonly IMapper _mapper;

    public GetLiteraryQueryHandler(IApplicationContext applicationContext, IMapper mapper)
    {
        _applicationContext = applicationContext;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<LiteraryDto>> Handle(GetLiteraryQuery request, CancellationToken cancellationToken)
    {
        LiteraryDto? literary = await _applicationContext.Literaries
            .Where(c => c.Id == request.Id)
            .Include(c => c.Author)
            .Include(c => c.Period)
            .Include(c => c.LiteraryCategory)
            .ProjectTo<LiteraryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        return BaseResponseModel<LiteraryDto>.Success(literary, "Eser başarıyla getirildi.");
    }
}