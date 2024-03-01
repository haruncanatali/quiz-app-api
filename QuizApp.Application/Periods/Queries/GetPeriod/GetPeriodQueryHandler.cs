using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Application.Periods.Queries.Dtos;

namespace QuizApp.Application.Periods.Queries.GetPeriod;

public class GetPeriodQueryHandler : IRequestHandler<GetPeriodQuery, BaseResponseModel<PeriodDto>>
{
    private readonly IApplicationContext _applicationContext;
    private readonly IMapper _mapper;

    public GetPeriodQueryHandler(IApplicationContext applicationContext, IMapper mapper)
    {
        _applicationContext = applicationContext;
        _mapper = mapper;
    }

    public async Task<BaseResponseModel<PeriodDto>> Handle(GetPeriodQuery request, CancellationToken cancellationToken)
    {
        PeriodDto? period = await _applicationContext.Periods
            .Where(c => c.Id == request.Id)
            .Include(c => c.Authors)
            .Include(c => c.Literaries)
            .ProjectTo<PeriodDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        return BaseResponseModel<PeriodDto>.Success(period,"Veri başarıyla getirildi.");
    }
}