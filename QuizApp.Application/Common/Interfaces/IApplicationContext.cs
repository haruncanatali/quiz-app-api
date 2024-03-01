using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuizApp.Application.Common.Managers;
using QuizApp.Domain.Base.Abstract;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Enums;
using QuizApp.Domain.Identity;

namespace QuizApp.Application.Common.Interfaces;

public interface IApplicationContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Literary> Literaries { get; set; }
    public DbSet<LiteraryCategory> LiteraryCategories { get; set; }
    public DbSet<Period> Periods { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    Task CreateBeginTransactionForFileAsync<T, TEntity>(T table, TEntity entity, IFormFile file, FileRoot root,
        ICurrentUserService currentUserService, FileManager fileManager, EntityState state) where TEntity : class, IEntityWithPhoto
        where T : DbSet<TEntity>;
}