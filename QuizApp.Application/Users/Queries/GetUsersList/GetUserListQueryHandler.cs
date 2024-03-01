using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizApp.Application.Common.Interfaces;
using QuizApp.Domain.Identity;

namespace QuizApp.Application.Users.Queries.GetUsersList
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, UserListVm>
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserListQueryHandler> _logger;

        public GetUserListQueryHandler(IApplicationContext context, IMapper mapper, ILogger<GetUserListQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserListVm> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var usersQuery = _context.Users
                .Where(c => (request.FullName == null || c.FullName.ToLower().Contains(request.FullName.ToLower())));

            var totalCount = usersQuery.Count();
            List<UserDto> users = await usersQuery
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            List<long> userIds = users.Select(x => x.Id).ToList();
            List<UserRole> userRoles =
                await _context.UserRoles.Where(x => userIds.Contains(x.UserId)).ToListAsync(cancellationToken);
            List<long> roleIds = userRoles.Select(x => x.RoleId).ToList();
            List<Role> roles = await _context.Roles.Where(x => roleIds.Contains(x.Id)).ToListAsync(cancellationToken);

            foreach (var item in users)
            {
                var rolesId = userRoles.Where(x => x.UserId == item.Id).Select(x => x.RoleId).ToList();
                var roleNames = roles.Where(x => rolesId.Contains(x.Id)).Select(x => x.Name).ToList();
                item.Role = string.Join(',',roleNames);
            }

            var vm = new UserListVm
            {
                Users = users,
                Count = users.Count
            };
            return vm;
        }
    }
}