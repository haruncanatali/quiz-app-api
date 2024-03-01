namespace QuizApp.Application.CommonValues.Dtos;

public class StatisticDto
{
    public long AuthorCount { get; set; }
    public long LiteraryCount { get; set; }
    public long PeriodCount { get; set; }
    public long LiteraryCategoryCount { get; set; }
    public long AdminCount { get; set; }
    public long UserCount { get; set; }

    public ChartDto AuthorsByPeriods { get; set; }
    public ChartDto LiterariesByCategories { get; set; }
}

public class ChartDto
{
    public List<string> Labels { get; set; }
    public List<ChartDatasetModel> Datasets { get; set; }
}

public class ChartDatasetModel
{
    public string Label { get; set; }
    public List<int> Data { get; set; }
    public List<string> BackgroundColor { get; set; }
    public List<string> BorderColor { get; set; }
    public short BorderWidth { get; set; }
}