using System.Runtime.Serialization;

namespace Pogulum.Server.Exceptions;

public class EntityNotFoundException<TEntity> : Exception where TEntity : class
{
    public EntityNotFoundException() { }

    public EntityNotFoundException(object entityId) : base($"{typeof(TEntity).FullName} with Id='{entityId.ToString()}' does not exist!") { }

    public EntityNotFoundException(string paramName, object paramValue) : base($"{typeof(TEntity).FullName} with {paramName}='{paramValue.ToString()}' does not exist!") { }

    public EntityNotFoundException(string? message) : base(message) { }

    public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }

    protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}