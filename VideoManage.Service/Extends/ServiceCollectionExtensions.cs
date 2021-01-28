using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace VideoManage.Service.Extends
{
    public static class ServiceCollectionExtensions
    {
        public static void AddScopeServices(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.DefinedTypes
                .Where(x => x.IsClass && !x.IsAbstract && !x.IsGenericType && !x.IsNested && x.Name.EndsWith("Service"))
                .ToArray();
            foreach (var type in types)
            {
                services.AddScoped(type);
            }
        }
    }
}
