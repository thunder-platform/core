using System;
using System.Reflection;
using Thunder.Platform.Core.Exceptions;
using Thunder.Platform.Core.Helpers;

namespace Thunder.Platform.Core.Domain.Entities
{
    public static class EntityHelper
    {
        public static bool IsEntity(Type type)
        {
            return ReflectionHelper.IsAssignableToGenericType(type, typeof(IEntity<>));
        }

        public static Type GetPrimaryKeyType<TEntity>()
        {
            return GetPrimaryKeyType(typeof(TEntity));
        }

        public static Type GetPrimaryKeyType(Type entityType)
        {
            foreach (var interfaceType in entityType.GetTypeInfo().GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
                {
                    return interfaceType.GenericTypeArguments[0];
                }
            }

            throw new GeneralException("Can not find primary key type of given entity type: " + entityType + ". Be sure that this entity type implements IEntity<TPrimaryKey> interface");
        }

        public static object GetEntityId(object entity)
        {
            if (!ReflectionHelper.IsAssignableToGenericType(entity.GetType(), typeof(IEntity<>)))
            {
                throw new GeneralException(entity.GetType() + " is not an Entity !");
            }

            return ReflectionHelper.GetValueByPath(entity, entity.GetType(), "Id");
        }

        public static string GetHardDeleteKey(object entity, int? tenantId)
        {
            return entity.GetType().FullName + ";Id=" + GetEntityId(entity);
        }
    }
}
