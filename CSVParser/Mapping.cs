using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace CSVParser
{
    public class Mapping<TEntity>
    {
        //private readonly List<PropertyMapping> propertyMappings;

        //protected PropertyMapping<TEntity, TProperty> MapProperty<TProperty>(int columnIndex, Expression<Func<TEntity, TProperty>> property)
        //{
        //    if (propertyMappings.Any(x => x.ColumnIndex == columnIndex))
        //    {
        //        throw new InvalidOperationException(string.Format("Duplicate mapping for column index {0}", columnIndex));
        //    }

        //    var propertyMapping = new CsvPropertyMapping<TEntity, TProperty>(property, typeConverter);

        //    AddPropertyMapping(columnIndex, propertyMapping);

        //    return propertyMapping;
        //}
    }

    public class PropertyMapping<TEntity, TProperty> : IPropertyMapping<TEntity>
    {
        private readonly PropertyInfo propertyInfo;
        private readonly string propertyName;
        private Action<TEntity, TProperty> propertySetter;

        public PropertyMapping(Expression<Func<TEntity, TProperty>> property)
        {

            propertyInfo = GetPropertyInfo(property);
            if (propertyInfo is null)
            {
                throw new InvalidOperationException("Expression must be a MemberExpression.");
            }
            propertyName = propertyInfo.Name;
            propertySetter = GetPropertySetter<TEntity, TProperty>(propertyName);

        }

        public bool TryMap(TEntity entity, string value)
        {
            //TProperty convertedValue;

            //if (!propertyConverter.TryConvert(value, out convertedValue))
            //{
            //    return false;
            //}

            //propertySetter(entity, convertedValue);

            return false;
        }

        private PropertyInfo GetPropertyInfo<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> expression)
        {
            var lambda = (LambdaExpression)expression;

            if (lambda.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new InvalidOperationException("Expression must be a MemberExpression.");
            }

            var memberExpression = (MemberExpression)lambda.Body;

            if (memberExpression == null)
            {
                throw new ArgumentException("Not a member access", "expression");
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"Expression member {memberExpression.Member.Name} must be a property reference.");
            }


            return propertyInfo;
        }

        public static Action<TEntity, TProperty> GetPropertySetter<TEntity, TProperty>(string propertyName)
        {
            ParameterExpression instanceExp = Expression.Parameter(typeof(TEntity));

            ParameterExpression parameterExp = Expression.Parameter(typeof(TProperty), propertyName);

            MemberExpression propertyGetterExpression = Expression.Property(instanceExp, propertyName);

            Action<TEntity, TProperty> result = Expression.Lambda<Action<TEntity, TProperty>>
            (
                Expression.Assign(propertyGetterExpression, parameterExp), instanceExp, parameterExp
            ).Compile();

            return result;
        }
    }

    public interface IPropertyMapping<TEntity>
    {
        bool TryMap(TEntity entity, string value);

    }
}
