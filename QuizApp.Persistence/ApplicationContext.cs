using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using QuizApp.Application.Common.Exceptions;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Application.Common.Managers;
using QuizApp.Application.CommonValues.Dtos;
using QuizApp.Domain.Base;
using QuizApp.Domain.Base.Abstract;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Enums;
using QuizApp.Domain.Identity;

namespace QuizApp.Persistence
{
    public class ApplicationContext : IdentityDbContext<User, Role, long, IdentityUserClaim<long>,
            UserRole, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>,
        IApplicationContext
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        #region Entities

        public DbSet<Author> Authors { get; set; }
        public DbSet<Literary> Literaries { get; set; }
        public DbSet<LiteraryCategory> LiteraryCategories { get; set; }
        public DbSet<Period> Periods { get; set; }

        #endregion

        #region Identity User Tables

        public DbSet<User> Users
        {
            get { return base.Users; }
            set { }
        }

        public DbSet<Role> Roles
        {
            get { return base.Roles; }
            set { }
        }

        public DbSet<UserRole> UserRoles
        {
            get { return base.UserRoles; }
            set { }
        }

        #endregion

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            if(_currentUserService!= null)
            {
				foreach (var entry in ChangeTracker.Entries<BaseEntity>())
				{
					switch (entry.State)
					{
						case EntityState.Added:
							entry.Entity.CreatedBy = _currentUserService.UserId;
							entry.Entity.CreatedAt = DateTime.Now;
							break;
						case EntityState.Modified:
							entry.Entity.UpdatedBy = _currentUserService.UserId;
							entry.Entity.UpdatedAt = DateTime.Now;
							break;
					}
				}
			}

    

            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
            base.OnModelCreating(builder);
        }

        public async Task CreateBeginTransactionForFileAsync<T,TEntity>(T table, TEntity entity, IFormFile file, FileRoot root,
            ICurrentUserService currentUserService, FileManager fileManager, EntityState state) where TEntity:class,IEntityWithPhoto where T:DbSet<TEntity>
        {
            using (IDbContextTransaction transaction = await this.Database.BeginTransactionAsync())
            {
                try
                {
                    string photoUrl = fileManager.Upload(file, root);
                    entity.Photo = photoUrl;
                    table.Entry(entity).State = state;
                    await this.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw new BadRequestException($"Hata meydana geldi. Hata mesajı :\n {e.Message}");
                }
            }
        }
    }
}