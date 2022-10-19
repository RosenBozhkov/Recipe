using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ServiceErrors;

public static class Errors
{
    public static class Recipe
    {
        public static Error InvalidName => Error.Validation(
            code: "Recipe.InvalidName",
            description: $"Recipe name must be at least {5}" +
                $" characters long and at most {25} characters long.");

        public static Error InvalidDescription => Error.Validation(
            code: "Recipe.InvalidDescription",
            description: $"Recipe description must be at least {15}" +
                $" characters long and at most {155} characters long.");

        public static Error NotFound => Error.NotFound(
            code: "Recipe.NotFound",
            description: "Recipe not found"
            );
    }
}
