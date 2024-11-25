using ErrorOr;
using SharedService.Models.Breakfasts;

namespace SharedService.ServiceErrors
{
    public static class Errors
    {
        public static class Breakfast
        {
            public static Error InvalidName => Error.Validation(
            code: "Breakfast.InvalidName",
            description: $"Breakfast name must be at least {BreakfastsModel.MinNameLength}" +
                $" characters long and at most {BreakfastsModel.MaxNameLength} characters long.");

            public static Error InvalidDescription => Error.Validation(
            code: "Breakfast.InvalidDescription",
            description: $"Breakfast description must be at least {BreakfastsModel.MinDescriptionLength}" +
                $" characters long and at most {BreakfastsModel.MaxDescriptionLength} characters long.");
             
            public static Error NotFound => Error.NotFound(
                code: "Breakfast.NotFound",
                description: "Breakfast not found");
        }
    }
}
