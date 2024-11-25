using ErrorOr;
using SharedService.Models.Breakfasts;

namespace WebApiServices.Services.Interface
{
    public interface IBreakfastService
    {
        ErrorOr<Created> CreateBreakfast(BreakfastsModel breakfast);
        ErrorOr<BreakfastsModel> GetBreakfast(Guid id);
        ErrorOr<UpsertedBreakfast> UpsertBreakfast(BreakfastsModel breakfast);
        ErrorOr<Deleted> DeleteBreakfast(Guid id);
    }
}
