using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Application.Periods.Queries.Dtos;

namespace QuizApp.Application.Periods.Queries.GetPeriods;

public class GetPeriodsQueryHandler : IRequestHandler<GetPeriodsQuery, BaseResponseModel<List<PeriodDto>>>
{
    private readonly IApplicationContext _applicationContext;
    private readonly IMapper _mapper;

    public GetPeriodsQueryHandler(IApplicationContext applicationContext, IMapper mapper)
    {
        _applicationContext = applicationContext;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<List<PeriodDto>>> Handle(GetPeriodsQuery request, CancellationToken cancellationToken)
    {
        List<PeriodDto> periods = await _applicationContext.Periods
            .Where(c => (request.Name == null || c.Name.ToLower().Contains(request.Name.ToLower())))
            .Include(c => c.Authors)
            .Include(c => c.Literaries)
            .ProjectTo<PeriodDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return BaseResponseModel<List<PeriodDto>>.Success(periods,"Veriler başarıyla çekildi.");
    }
}