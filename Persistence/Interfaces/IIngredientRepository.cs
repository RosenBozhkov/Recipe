using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Interfaces;

public interface IIngredientRepository
{
    Task<Ingredient?> GetByIdAsync(Guid id);
    Task<Ingredient?> GetByNameAsync(string name);
    Ingredient Add(Ingredient ingredient);
    Task SaveChangesAsync();
}
