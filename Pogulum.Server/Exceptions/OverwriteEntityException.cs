using System.Runtime.Serialization;

namespace Pogulum.Server.Exceptions;

public class OverwriteEntityException<TEntity> : Exception where TEntity : class
{
    public OverwriteEntityException() { }

    public OverwriteEntityException(object entityId) : base($"{typeof(TEntity).FullName} with Id='{entityId.ToString()}' already exists!") { }

    public OverwriteEntityException(string paramName, object paramValue) : base($"{typeof(TEntity).FullName} with {paramName}='{paramValue.ToString()}' already exists!") { }

    public OverwriteEntityException(string? message) : base(message) { }

    public OverwriteEntityException(string? message, Exception? innerException) : base(message, innerException) { }

    protected OverwriteEntityException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}