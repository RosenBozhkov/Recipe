namespace Persistence.Entities;

public class PreparationStep
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public string Description { get; set; }
    public TimeSpan Duration { get; set; }

    //nav prop one to many
    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}