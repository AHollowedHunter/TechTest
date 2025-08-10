using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data.Interfaces;
using UserManagement.Models;

namespace UserManagement.Data;

public class DataContext : DbContext, IDataContext
{
    public DataContext()
    {
        // Normally we would not Migrate at runtime, but this is just a demonstration
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Normally would set this within a Hosting builder and use IConfiguration
        options.UseSqlite("Data Source=:memory:");

        // Using recommend data seeding: https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding
        options.UseSeeding((context, _) =>
        {
            context.Set<User>().AddRange(SeedData.Users);
            context.SaveChanges();
        });
    }

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<User>(builder =>
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Email)
                .HasMaxLength(255);
        });
    }

    public DbSet<User>? Users { get; set; }

    public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        => base.Set<TEntity>();

    public TEntity? Get<TEntity, TId>(TId id) where TEntity : class, IEntity<TId> where TId : IEquatable<TId>
        => base.Set<TEntity>().SingleOrDefault(x => x.Id.Equals(id));

    public TEntity? Get<TEntity>(long id) where TEntity : class, ILongEntity
        => Get<TEntity, long>(id);

    public bool Exists<TEntity, TId>(long id) where TEntity : class, IEntity<TId> where TId : IEquatable<TId>
        => base.Set<TEntity>().Any(x => x.Id.Equals(id));

    public bool Exists<TEntity>(long id) where TEntity : class, ILongEntity
        => Exists<TEntity, long>(id);

    public void Create<TEntity>(TEntity entity) where TEntity : class
    {
        base.Add(entity);
        SaveChanges();
    }

    public new void Update<TEntity>(TEntity entity) where TEntity : class
    {
        base.Update(entity);
        SaveChanges();
    }

    public void Delete<TEntity>(TEntity entity) where TEntity : class
    {
        base.Remove(entity);
        SaveChanges();
    }
}
