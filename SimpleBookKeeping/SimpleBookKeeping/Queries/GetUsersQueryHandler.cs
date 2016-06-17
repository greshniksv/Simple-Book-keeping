﻿using System.Collections.Generic;
using MediatR;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Models;

namespace SimpleBookKeeping.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IList<UserModel>>
    {
        /// <summary>Handles a request</summary>
        /// <param name="message">The request message</param>
        /// <returns>Response from the request</returns>
        public IList<UserModel> Handle(GetUsersQuery message)
        {
            IList<UserModel> userModels;
            using (var session = Db.Session)
            {
                var users = session.QueryOver<User>().List();
                userModels = AutoMapperConfig.Mapper.Map<IList<UserModel>>(users);
            }

            return userModels;
        }
    }
}