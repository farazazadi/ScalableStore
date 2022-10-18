using DemoStore.Services.CommandSide.Domain.Common;

namespace DemoStore.Services.CommandSide.Application.Common.Exceptions;

public sealed class EntityNotFoundException <TEntity> : ApplicationException
    where TEntity : Entity
{
    public override string Code => $"{nameof(EntityNotFoundException<TEntity>)}|{typeof(TEntity).Name}";

    public EntityNotFoundException(Guid id)
        : base($"{typeof(TEntity).Name} with Id ({id}) was not found!")
    {
    }

}