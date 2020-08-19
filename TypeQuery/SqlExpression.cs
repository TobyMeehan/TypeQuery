using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TypeQuery.Extensions;

namespace TypeQuery
{
    public class SqlExpression : SqlClause
    {
        public SqlExpression(Expression expression)
        {
            Expression = expression;

            try
            {
                Parse();
            }
            catch (ArgumentOutOfRangeException ex) when (ex.ParamName == nameof(Expression))
            {
                throw new ArgumentOutOfRangeException(nameof(expression), ex.Message);
            }
            catch (ArgumentNullException ex) when (ex.ParamName == nameof(Expression))
            {
                throw new ArgumentNullException(nameof(expression), ex.Message);
            }
            catch (ArgumentException ex) when (ex.ParamName == nameof(Expression))
            {
                throw new ArgumentException(ex.Message, nameof(expression));
            }
        }

        public Expression Expression { get; set; }

        private void Parse()
        {
            if (Expression is LambdaExpression lambda)
            {
                Clauses.Add(new SqlExpression(lambda.Body));
            }

            if (Expression is UnaryExpression unary)
            {
                Clauses.AddRange(GetNodeClause(unary.NodeType), new SqlExpression(unary.Operand));
            }

            if (Expression is BinaryExpression binary)
            {
                Clauses.AddRange(new SqlExpression(binary.Left), GetNodeClause(binary.NodeType), new SqlExpression(binary.Right));
            }

            if (Expression is ConstantExpression constant)
            {
                object value = constant.Value;

                if (value is int)
                {
                    Clauses.Add(new SqlClause(value.ToString()));
                }

                if (value is string)
                {
                    value = (string)value;
                }

                Clauses.Add(new ParameterSqlClause(value));
            }

            if (Expression is MemberExpression member)
            {
                if (member.Member is PropertyInfo property)
                {
                    Clauses.Add(new SqlClause($"{property.GetColumnName()}.{member.Expression.Type.GetTableName()}"));
                }

                if (member.Member is FieldInfo)
                {
                    Clauses.Add(new ParameterSqlClause(GetMemberExpressionValue(member)));
                }

                throw new ArgumentOutOfRangeException(nameof(Expression), "Expression does not refer to a property or field.");
            }

            if (Expression is MethodCallExpression methodCall)
            {
                Clauses.Add(new ParameterSqlClause(GetMethodExpressionValue(methodCall)));
            }

            throw new ArgumentOutOfRangeException(nameof(Expression), $"Unsupported expression type '{Expression.GetType().Name}'.");
        }

        private object GetMemberExpressionValue(MemberExpression member)
        {
            var objectMember = Expression.Convert(member, typeof(object));
            var getterLambda = Expression.Lambda<Func<object>>(objectMember);
            return getterLambda.Compile().Invoke();
        }

        private object GetMethodExpressionValue(MethodCallExpression methodCall)
        {
            return Expression.Lambda(methodCall).Compile().DynamicInvoke();
        }

        private SqlClause GetNodeClause(ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.Add:
                    return new SqlClause("+");
                case ExpressionType.And:
                    return new SqlClause("&");
                case ExpressionType.AndAlso:
                    return new SqlClause("AND");
                case ExpressionType.Divide:
                    return new SqlClause("/");
                case ExpressionType.Equal:
                    return new SqlClause("=");
                case ExpressionType.ExclusiveOr:
                    return new SqlClause("^");
                case ExpressionType.GreaterThan:
                    return new SqlClause(">");
                case ExpressionType.GreaterThanOrEqual:
                    return new SqlClause(">=");
                case ExpressionType.LessThan:
                    return new SqlClause("<");
                case ExpressionType.LessThanOrEqual:
                    return new SqlClause("<=");
                case ExpressionType.Modulo:
                    return new SqlClause("%");
                case ExpressionType.Multiply:
                    return new SqlClause("*");
                case ExpressionType.Negate:
                    return new SqlClause("-");
                case ExpressionType.Not:
                    return new SqlClause("NOT");
                case ExpressionType.NotEqual:
                    return new SqlClause("<>");
                case ExpressionType.Or:
                    return new SqlClause("|");
                case ExpressionType.OrElse:
                    return new SqlClause("OR");
                case ExpressionType.Subtract:
                    return new SqlClause("-");
            }

            throw new ArgumentOutOfRangeException(nameof(nodeType), "Unsupported node type.");
        }
    }
}
