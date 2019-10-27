namespace Thunder.Platform.Core.Domain.UnitOfWork.Abstractions
{
    public interface IUnitOfWorkManagerAccessor
    {
        IUnitOfWorkManager UnitOfWorkManager { get; }
    }
}
