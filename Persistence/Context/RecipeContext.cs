using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.Entities;
using Persistence.Entities.Abstract;
using Persistence.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context;

public partial class RecipeContext : DbContext
{
    public RecipeContext()
    {

    }
    
    public RecipeContext(DbContextOptions<RecipeContext> options)
        : base(options)
    {

    }

    public virtual DbSet<Recipe> Recipes { get; set; }
    public virtual DbSet<Ingredient> Ingredients { get; set; }
    public virtual DbSet<IngredientAmount> IngredientAmounts { get; set; }
    public virtual DbSet<PreparationStep> PreparationSteps { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=.\\ ;Database=RecipeApi;Trusted_Connection=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IngredientAmount>()
        .HasKey(ri => new { ri.RecipeId, ri.IngredientId });

        modelBuilder.Entity<IngredientAmount>()
            .HasOne(ri => ri.Recipe)
            .WithMany(r => r.IngredientAmounts)
            .HasForeignKey(ri => ri.RecipeId);

        modelBuilder.Entity<IngredientAmount>()
            .HasOne(ri => ri.Ingredient)
            .WithMany(i => i.IngredientAmounts)
            .HasForeignKey(ri => ri.IngredientId);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override int SaveChanges()
    {
        UpdateBaseEntities();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateBaseEntities();
        return base.SaveChangesAsync();
    }

    private void UpdateBaseEntities()
    {
        IEnumerable<EntityEntry> entities = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entry in entities)
        {
            DateTime date = DateTime.UtcNow;
            BaseEntity baseEntity = (BaseEntity)entry.Entity;
            baseEntity.ModifiedAt = date;
            if (entry.State == EntityState.Added)
            {
                baseEntity.CreatedAt = date;
            }
        }
    }
}
