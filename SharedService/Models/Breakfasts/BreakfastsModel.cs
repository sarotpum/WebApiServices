using ErrorOr;
using SharedService.ServiceErrors;

namespace SharedService.Models.Breakfasts
{
    public class BreakfastsModel
    {
        public const int MinNameLength = 3;
        public const int MaxNameLength = 50;

        public const int MinDescriptionLength = 50;
        public const int MaxDescriptionLength = 150;

        public Guid Id { get; }
        public string Name { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public DateTime StartDateTime { get; }
        public DateTime EndDateTime { get; }
        public DateTime LastModifiedDateTime { get; }
        public List<string> Savory { get; } = new List<string>();
        public List<string> Sweet { get; } = new List<string>();

        public BreakfastsModel(
            Guid id,
            string name,
            string description,
            DateTime startDateTime,
            DateTime endDateTime,
            DateTime lastModifiedDateTime,
            List<string> savory,
            List<string> sweet)
        {
            Id = id;
            Name = name;
            Description = description;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            LastModifiedDateTime = lastModifiedDateTime;
            Savory = savory;
            Sweet = sweet;
        }

        public static ErrorOr<BreakfastsModel> Create(
            string name,
            string description,
            DateTime startDateTime,
            DateTime endDateTime,
            List<string> savory,
            List<string> sweet,
            Guid? id = null)
        {
            List<Error> errors = new();

            if (name.Length is < MinNameLength or > MaxNameLength)
            {
                errors.Add(Errors.Breakfast.InvalidName);
            }

            if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
            {
                errors.Add(Errors.Breakfast.InvalidDescription);
            }

            if (errors.Count > 0)
            {
                return errors;
            }

            return new BreakfastsModel(
                id ?? Guid.NewGuid(),
                name,
                description,
                startDateTime,
                endDateTime,
                DateTime.UtcNow,
                savory,
                sweet);
        }

        public static ErrorOr<BreakfastsModel> From(CreateBreakfastRequestModel request)
        {
            return Create(
                request.Name,
                request.Description,
                request.StartDateTime,
                request.EndDateTime,
                request.Savory,
                request.Sweet);
        }

        public static ErrorOr<BreakfastsModel> From(Guid id, UpsertBreakfastRequestModel request)
        {
            return Create(
                request.Name,
                request.Description,
                request.StartDateTime,
                request.EndDateTime,
                request.Savory,
                request.Sweet,
                id);
        }
    }
}
