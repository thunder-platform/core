using System;
using System.Runtime.Serialization;
using Thunder.Platform.Core.Exceptions;

namespace Thunder.Platform.Core.Domain.Entities
{
    /// <summary>
    /// This exception is thrown if an entity excepted to be found but not found.
    /// </summary>
    [Serializable]
    public class EntityNotFoundException : GeneralException
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        public EntityNotFoundException(Type entityType, object id)
            : this(entityType, id, null)
        {
        }

        public EntityNotFoundException(Type entityType, object id, Exception innerException)
            : base($"There is no such an entity. Entity type: {entityType.FullName}, id: {id}", innerException)
        {
            EntityType = entityType;
            Id = id;
        }

        public EntityNotFoundException(string message)
            : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Type of the entity.
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// Id of the Entity.
        /// </summary>
        public object Id { get; set; }
    }
}
