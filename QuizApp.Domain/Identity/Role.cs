using Microsoft.AspNetCore.Identity;

namespace QuizApp.Domain.Identity;

public class Role : IdentityRole<long>
{
    public Role() : base()
    {

    }

    public Role(string roleName) : base(roleName)
    {

    }
}