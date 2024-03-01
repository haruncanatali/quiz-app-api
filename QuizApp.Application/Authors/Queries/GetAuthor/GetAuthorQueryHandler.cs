using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Authors.Queries.Dtos;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;

namespace QuizApp.Application.Authors.Queries.GetAuthor;

public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, BaseResponseModel<AuthorDto>>
{
    private readonly IApplicationContext _applicationContext;
    private readonly IMapper _mapper;

    public GetAuthorQueryHandler(IApplicationContext applicationContext, IMapper mapper)
    {
        _applicationContext = applicationContext;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<AuthorDto>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
    {
        AuthorDto? author = await _applicationContext.Authors
            .Where(c => c.Id == request.Id)
            .Include(c => c.Period)
            .Include(c => c.Literaries)
            .ProjectTo<AuthorDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        return BaseResponseModel<AuthorDto>.Success(author, "Yazar başarıyla getirildi.");
    }
}