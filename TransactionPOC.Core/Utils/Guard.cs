using System;
using System.Linq.Expressions;

namespace TransactionPOC.Core.Utils
{
    public static class Guard
    {
        public static void NotNull<T>(Expression<Func<T>> fn, T val)
        {
            if (val == null)
            {
                throw new ArgumentNullException("Cannot be null.", GetArguymentName(fn));
            }
        }

        private static string GetArguymentName<T>(Expression<Func<T>> fn)
        {
            var me = (MemberExpression)fn.Body;
            return me.Member.Name;
        }
    }
}