using System;
using System.Collections.Generic;
using System.Linq;
using UserManagement.Data.Interfaces;

namespace UserManagement.Data;

public interface IDataContext
{
    /// <summary>
    /// Get a list of items
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;

    /// <summary>
    /// Get an entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    TEntity? Get<TEntity>(long id) where TEntity : class, ILongEntity;

    /// <summary>
    /// Create a new item
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    void Create<TEntity>(TEntity entity) where TEntity : class;

    /// <summary>
    /// Uodate an existing item matching the ID
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    void Update<TEntity>(TEntity entity) where TEntity : class;

    void Delete<TEntity>(TEntity entity) where TEntity : class;

    bool Exists<TEntity, TId>(long id) where TEntity : class, IEntity<TId> where TId : IEquatable<TId>;
    bool Exists<TEntity>(long id) where TEntity : class, ILongEntity;
}
