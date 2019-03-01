namespace FluentFiles.Core.Extensions
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    internal static class ExpressionExtensions
    {
        public static MemberInfo GetMemberInfo<T, TMember>(this Expression<Func<T, TMember>> expression)
        {
            return expression.GetMemberExpression().Member;
        }

        public static MemberExpression GetMemberExpression<T, TMember>(this Expression<Func<T, TMember>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            return GetMemberExpression(expression.Body);
        }

        private static MemberExpression GetMemberExpression(Expression body)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            MemberExpression memberExpression = null;
            if (body.NodeType == ExpressionType.Convert)
            {
                var unaryExpression = (UnaryExpression)body;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else if (body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = body as MemberExpression;
            }

            if (memberExpression == null)
                throw new ArgumentException("Expression is not a member access.");

            return memberExpression;
        }
    }
}