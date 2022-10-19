using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Interfaces;

public interface IRecipeRepository
{
    Task<Recipe?> GetByIdAsync(Guid id);
    Task<Recipe?> GetByNameAsync(string name);
    Recipe Add(Recipe recipe);
    Task SaveChangesAsync();
}
