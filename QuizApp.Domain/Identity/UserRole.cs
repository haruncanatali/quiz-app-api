using Microsoft.AspNetCore.Identity;

namespace QuizApp.Domain.Identity;

public class UserRole : IdentityUserRole<long>
{
    public UserRole() : base()
    {
            
    }
}