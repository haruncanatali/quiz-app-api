using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Models;
using QuizApp.Application.CommonValues.Dtos;
using QuizApp.Domain.Enums;
using QuizApp.Domain.Identity;

namespace QuizApp.Application.CommonValues.Queries.GetStatistics;

public class GetStatisticsQueryHandler : IRequestHandler<GetStatisticsQuery, BaseResponseModel<StatisticDto>>
{
    private readonly IApplicationContext _applicationContext;
    private readonly UserManager<User> _userManager;

    public GetStatisticsQueryHandler(IApplicationContext applicationContext, UserManager<User> userManager)
    {
        _applicationContext = applicationContext;
        _userManager = userManager;
    }

    public async Task<BaseResponseModel<StatisticDto>> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
    {
        StatisticDto result = new StatisticDto();

        result.AuthorCount = await _applicationContext.Authors.CountAsync(cancellationToken);
        result.LiteraryCount = await _applicationContext.Literaries.CountAsync(cancellationToken);
        result.LiteraryCategoryCount = await _applicationContext.LiteraryCategories.CountAsync(cancellationToken);
        result.PeriodCount = await _applicationContext.Periods.CountAsync(cancellationToken);

        List<long> userRolesIds = await _applicationContext.Roles
            .Where(c => c.Name!.ToLower() != "admin")
            .Select(c=>c.Id)
            .ToListAsync(cancellationToken);
        List<long> adminRolesIds = await _applicationContext.Roles
            .Where(c => c.Name!.ToLower() == "admin")
            .Select(c => c.Id)
            .ToListAsync(cancellationToken);

        result.AdminCount = await _userManager.Users
            .Where(c => adminRolesIds.Contains(c.Id))
            .CountAsync(cancellationToken);

        result.UserCount = await _userManager.Users
            .Where(c => userRolesIds.Contains(c.Id))
            .CountAsync(cancellationToken);

        var authorsByPeriods = await _applicationContext.Periods
            .GroupBy(c => c.Name)
            .Select(group => new
            {
                Name = group.Key,
                Count = group.SelectMany(c => c.Authors).Count()
            })
            .ToListAsync(cancellationToken);

        ChartDatasetModel authorsByPeriodsChartDatasetModel = new ChartDatasetModel();
        authorsByPeriodsChartDatasetModel.Data = authorsByPeriods
            .Select(c => c.Count)
            .ToList();
        authorsByPeriodsChartDatasetModel.Label = "Sayı";
        authorsByPeriodsChartDatasetModel.BackgroundColor =
            FillColors(ChartTypes.Pie, ColorTypes.Background, authorsByPeriodsChartDatasetModel.Data.Count);
        authorsByPeriodsChartDatasetModel.BorderColor =
            FillColors(ChartTypes.Pie, ColorTypes.Border, authorsByPeriodsChartDatasetModel.Data.Count);
        authorsByPeriodsChartDatasetModel.BorderWidth = 1;
        
        ChartDto authorsByPeriodsChartDto = new ChartDto();
        authorsByPeriodsChartDto.Labels = authorsByPeriods
            .Select(c => c.Name)
            .ToList();
        authorsByPeriodsChartDto.Datasets = new List<ChartDatasetModel>{authorsByPeriodsChartDatasetModel};

        result.AuthorsByPeriods = authorsByPeriodsChartDto;
        
        //////////////////
        
        var literariesByCategories = await _applicationContext.LiteraryCategories
            .GroupBy(c => c.Name)
            .Select(group => new
            {
                Name = group.Key,
                Count = group.SelectMany(c => c.Literaries).Count()
            })
            .ToListAsync(cancellationToken);
        
        ChartDatasetModel literariesByCategoriesChartDatasetModel = new ChartDatasetModel();
        literariesByCategoriesChartDatasetModel.Data = literariesByCategories
            .Select(c => c.Count)
            .ToList();
        literariesByCategoriesChartDatasetModel.Label = "Sayı";
        literariesByCategoriesChartDatasetModel.BackgroundColor =
            FillColors(ChartTypes.Pie, ColorTypes.Background, literariesByCategoriesChartDatasetModel.Data.Count);
        literariesByCategoriesChartDatasetModel.BorderColor =
            FillColors(ChartTypes.Pie, ColorTypes.Border, literariesByCategoriesChartDatasetModel.Data.Count);
        literariesByCategoriesChartDatasetModel.BorderWidth = 1;
        
        ChartDto literariesByCategoriesChartDto = new ChartDto();
        literariesByCategoriesChartDto.Labels = literariesByCategories
            .Select(c => c.Name)
            .ToList();
        literariesByCategoriesChartDto.Datasets = new List<ChartDatasetModel>{literariesByCategoriesChartDatasetModel};

        result.LiterariesByCategories = literariesByCategoriesChartDto;
        
        return BaseResponseModel<StatisticDto>.Success(result,"İstatisk verileri başarıyla getirildi.");
    }

    public List<string> FillColors(ChartTypes chartType, ColorTypes colorType, int count=1)
    {
        List<string> colors = new List<string>();
        Random random = new Random();
        for (int i = 0; i < count; i++)
        {
            var colorTypeStr = colorType == ColorTypes.Background ? "rgba" : "rgb";
            var threshold = colorType == ColorTypes.Background ? 0.2 : 1;
            var red = random.Next(0, 255);
            var green = random.Next(0, 255);
            var blue = random.Next(0, 255);
            colors.Add($"{colorTypeStr}({red},{green},{blue},{threshold.ToString().Split(',','.')})");
        }

        return colors;
    }

    public void UpdateBorders(List<string> backgroundColors)
    {
        return;
    }
}