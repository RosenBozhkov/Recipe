using Persistence.Entities.Abstract;
using Persistence.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Entities;

public class Recipe : BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }// = string.Empty;
    public MealType MealType { get; set; }
    public DifficultyLevel Difficulty { get; set; }
    public string Description { get; set; }// = string.Empty;
    public TimeSpan TotalPreparationTime { get; set; }

    //nav prop, one to many
    public ICollection<PreparationStep> PreparationSteps { get; set; }
    //nav prop, many to many
    public ICollection<IngredientAmount> IngredientAmounts { get; set; }// = new List<IngredientAmount>();
}
