using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;
using QuizApp.Domain.Enums;

namespace QuizApp.Domain.Identity;

public class User : IdentityUser<long>
{
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public string ProfilePhoto { get; set; }
    public DateTime Birthdate { get; set; }
    
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiredTime { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long CreatedBy { get; set; }
    public long? UpdatedBy { get; set; }

    [IgnoreDataMember]
    public string FullName
    {
        get { return $"{FirstName} {Surname}"; }
    }
}