using System;
using System.Collections.Generic;
using MediatR;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetUsersQuery : IRequest<IList<UserModel>>
    {
        public IList<Guid> UsersId { get; set; }
    }
}