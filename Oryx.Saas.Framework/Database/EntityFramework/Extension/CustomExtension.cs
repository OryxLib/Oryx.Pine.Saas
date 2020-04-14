using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Oryx.Saas.Framework.Database.EntityFramework.Extension
{
    public static class CustomExtension
    {
        public static IQueryable Query(this DbContext context, string entityName) =>
            context.Query(context.Model.FindEntityType(entityName).ClrType);

        static readonly MethodInfo SetMethod = typeof(DbContext).GetMethod(nameof(DbContext.Set));

        public static IQueryable Query(this DbContext context, Type entityType) =>
            (IQueryable)SetMethod.MakeGenericMethod(entityType).Invoke(context, null);

        //public static IQueryable Query(this DbContext context, Type entityType) =>
        //    (IQueryable)((IDbSetCache)context).GetOrAddSet(context.GetDependencies().SetSource, entityType); 

    }
} 
