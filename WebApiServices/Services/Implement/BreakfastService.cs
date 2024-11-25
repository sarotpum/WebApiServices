using ErrorOr;
using SharedService.Models.Breakfasts;
using SharedService.ServiceErrors;
using WebApiServices.Services.Interface;

namespace WebApiServices.Services.Implement
{
    public class BreakfastService : IBreakfastService
    {
        private static readonly Dictionary<Guid, BreakfastsModel> _breakfasts = new();

        public ErrorOr<Created> CreateBreakfast(BreakfastsModel breakfast)
        {
            _breakfasts.Add(breakfast.Id, breakfast);
            return Result.Created;
        }

        public ErrorOr<Deleted> DeleteBreakfast(Guid id)
        {
            _breakfasts.Remove(id);
            return Result.Deleted;
        }

        public ErrorOr<BreakfastsModel> GetBreakfast(Guid id)
        {
            if (_breakfasts.TryGetValue(id, out var breakfast))
            {
                return breakfast;
            }

            return Errors.Breakfast.NotFound;
        }

        public ErrorOr<UpsertedBreakfast> UpsertBreakfast(BreakfastsModel breakfast)
        {
            var isNewlyCreated = !_breakfasts.ContainsKey(breakfast.Id);
            _breakfasts[breakfast.Id] = breakfast;

            return new UpsertedBreakfast(isNewlyCreated);
        }
    }
}
