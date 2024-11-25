using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using SharedService.Models.Breakfasts;
using WebApiServices.Services.Interface;

namespace WebApiServices.Controllers.BuberBreakfastController
{
    public class BreakfastsController : ControllerBase
    {
        private readonly IBreakfastService _breakfastService;

        public BreakfastsController(IBreakfastService breakfastService)
        {
            _breakfastService = breakfastService;
        }

        [HttpPost]
        public IActionResult CreateBreakfast(CreateBreakfastRequestModel request)
        {
            ErrorOr<BreakfastsModel> requestToBreakfastResult = BreakfastsModel.From(request);

            if (requestToBreakfastResult.IsError)
            {
                return Problem(requestToBreakfastResult.Errors.ToString());
            }

            var breakfast = requestToBreakfastResult.Value;
            ErrorOr<Created> createBreakfastResult = _breakfastService.CreateBreakfast(breakfast);

            return createBreakfastResult.Match(
                created => CreatedAtGetBreakfast(breakfast),
                errors => Problem(errors.ToString()));
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetBreakfast(Guid id)
        {
            ErrorOr<BreakfastsModel> getBreakfastResult = _breakfastService.GetBreakfast(id);

            return getBreakfastResult.Match(
                breakfast => Ok(MapBreakfastResponse(breakfast)),
                errors => Problem(errors.ToString()));
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequestModel request)
        {
            ErrorOr<BreakfastsModel> requestToBreakfastResult = BreakfastsModel.From(id, request);

            if (requestToBreakfastResult.IsError)
            {
                return Problem(requestToBreakfastResult.Errors.ToString());
            }

            var breakfast = requestToBreakfastResult.Value;
            ErrorOr<UpsertedBreakfast> upsertBreakfastResult = _breakfastService.UpsertBreakfast(breakfast);
             
            return upsertBreakfastResult.Match(
                upserted => CreatedAtGetBreakfast(breakfast), //upserted.IsNewlyCreated ? CreatedAtGetBreakfast(breakfast) : NoContent(),
                errors => Problem(upsertBreakfastResult.Errors.ToString()));
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteBreakfast(Guid id)
        {
            ErrorOr<Deleted> deleteBreakfastResult = _breakfastService.DeleteBreakfast(id);

            return deleteBreakfastResult.Match(
                deleted => null, // NoContent(),
                errors => Problem(deleteBreakfastResult.Errors.ToString()));
        }

        private static BreakfastResponseModel MapBreakfastResponse(BreakfastsModel breakfast)
        {
            return new BreakfastResponseModel(breakfast.Id,
                breakfast.Name,
                breakfast.Description,
                breakfast.StartDateTime,
                breakfast.EndDateTime,
                breakfast.LastModifiedDateTime,
                breakfast.Savory,
                breakfast.Sweet
                );
        }

        private CreatedAtActionResult CreatedAtGetBreakfast(BreakfastsModel breakfast)
        {
            return CreatedAtAction(
                actionName: nameof(GetBreakfast),
                routeValues: new { id = breakfast.Id },
                value: MapBreakfastResponse(breakfast));
        }
    }
}


// dotnet new sln -o BuberBreakfast
// dotnet new classlib -o BuberBreakfast.Contracts
// dotnet new webapi -o BuberBreakfast
// dotnet add BuberBreakfast/BuberBreakfast.csproj reference../BuberBreakfast/BuberBreakfast.Contracts/BuberBreakfast.Contracts.csproj
// dotnet run --project 