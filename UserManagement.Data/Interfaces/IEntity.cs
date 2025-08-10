using System;

namespace UserManagement.Data.Interfaces;

public interface IEntity<out TId>
    where TId : IEquatable<TId>
{
    TId Id { get; }
}
