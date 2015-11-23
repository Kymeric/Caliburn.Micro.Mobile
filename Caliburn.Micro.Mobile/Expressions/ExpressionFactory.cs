using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Caliburn.Micro.Mobile.Expressions
{
    public static class ExpressionFactory
    {
        public static Expression<Action<TProperty>> SetProperty<TTarget, TProperty>(TTarget target,
                                                                        Expression<Func<TTarget, TProperty>> property)
        {
            var propInfo = property.GetMemberInfo() as PropertyInfo;
            return SetProperty<TTarget, TProperty>(target, propInfo);
        }

        public static Expression<Action<TProperty>> SetProperty<TTarget, TProperty>(TTarget target, PropertyInfo property)
        {
            if (!property.CanWrite)
                return null;
            var parameter = Expression.Parameter(typeof(TProperty), "value");

            return Expression.Lambda<Action<TProperty>>(
                Expression.Assign(
                    Expression.MakeMemberAccess(Expression.Constant(target), property),
                    parameter), parameter);
        }

        public static Expression<Func<TTarget, TProperty>> GetProperty<TTarget, TProperty>(TTarget target, PropertyInfo property)
        {
            if (!property.CanRead)
                return null;
            return Expression.Lambda<Func<TTarget, TProperty>>(
                Expression.Property(Expression.Constant(target), property));
        } 
    }
}
