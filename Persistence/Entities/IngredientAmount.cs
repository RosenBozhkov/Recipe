using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities;

public class IngredientAmount
{
    public Guid IngredientId { get; set; }
    public Ingredient Ingredient { get; set; }
    
    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; }

    public string Unit { get; set; }
    public decimal Amount { get; set; }
}