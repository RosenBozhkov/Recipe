using Persistence.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities;

public class Ingredient : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    //nav prop, many to many
    public ICollection<IngredientAmount> IngredientAmounts { get; set; }
}