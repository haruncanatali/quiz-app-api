using System.Globalization;
using FluentValidation;
using QuizApp.Application.Common.Managers;
using QuizApp.Application.Common.Models;

namespace QuizApp.Api.Configs;

public static class SettingsConfig
{
    public static IServiceCollection AddSettingsConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var cultureInfo = new CultureInfo("tr-TR");
        System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
        ValidatorOptions.Global.LanguageManager.Culture = cultureInfo;
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        services.Configure<TokenSetting>(configuration.GetSection("TokenSetting"));
        services.AddTransient<TokenManager>();
        services.AddTransient<FileManager>();
        return services;
    }
}