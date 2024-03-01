namespace QuizApp.Application.Common.Interfaces;

public interface ICurrentUserService
{
    long UserId { get; }
    bool IsAuthenticated { get; }
}