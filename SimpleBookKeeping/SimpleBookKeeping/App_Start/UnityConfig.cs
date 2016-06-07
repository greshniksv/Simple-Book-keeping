using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MediatR;
using Microsoft.Practices.Unity;
using SimpleBookKeeping.Authentication;
using SimpleBookKeeping.Commands;
using SimpleBookKeeping.Unility;
using SimpleBookKeeping.Unility.Interfaces;

namespace SimpleBookKeeping
{
    public class UnityConfig
    {
        private static readonly IEnumerable<Type> MediatorTypes = new[]
        {
            typeof(IRequestHandler<,>),
            typeof(IAsyncRequestHandler<,>)
        };

        public static void RegisterTypes(UnityContainer container)
        {
            container
                .RegisterType<IHashCalculator, Md5HashCalculator>()
                .RegisterType<IAuthentication, CustomAuthentication>();


            // MediatR
            container.RegisterType<IMediator, Mediator>();

            container.RegisterTypes(
                AllClasses.FromAssemblies(typeof(AddUpdatePlanCommand).Assembly)
                    .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                            MediatorTypes.Contains(i.GetGenericTypeDefinition()))),
                WithMappings.FromAllInterfaces);

            container.RegisterInstance<SingleInstanceFactory>(t => container.Resolve(t));
            container.RegisterInstance<MultiInstanceFactory>(t => container.ResolveAll(t));
        }

    }
}