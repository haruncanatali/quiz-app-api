using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Authors.Queries.Dtos;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.Authors.Queries.GetAuthors;

public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, BaseResponseModel<List<AuthorDto>>>
{
    private readonly IApplicationContext _applicationContext;
    private readonly IMapper _mapper;

    public GetAuthorsQueryHandler(IApplicationContext applicationContext, IMapper mapper)
    {
        _applicationContext = applicationContext;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<List<AuthorDto>>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        List<AuthorDto> authors = await _applicationContext.Authors
            .Where(c => (request.Name == null || c.Name.ToLower().Contains(request.Name.ToLower())) &&
                        (request.Surname == null || c.Surname.ToLower().Contains(request.Surname.ToLower())) &&
                        (request.PeriodId == null || c.PeriodId == request.PeriodId))
            .Include(c => c.Period)
            .Include(c => c.Literaries)
            .ProjectTo<AuthorDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return BaseResponseModel<List<AuthorDto>>.Success(authors, "Yazarlar başarıyla getirildi.");
    }
}