using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using WebAPI_one.ViewModel;

namespace WebAPI_one.Filters
{
    public class CarTypeValidationAttribute: ActionFilterAttribute
    {
        // before request, run this code before Action excuting
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            //var allowedTypeRegex = new Regex("Electric||Gas||Diesel||Hybrid");
            List<string> allowedType = new List<string>()
            {
                "Electric",
                "Gas",
                "Diesel",
                "Hybrid"
            };
            Car? car = context.ActionArguments["car"] as Car;
            if (car is null || !allowedType.Contains(car.Type))
            {
                //Exit with BadRequest
                context.Result = new BadRequestObjectResult(
                    new { TypeError = "Type is not correct" }
                    );
            }
        }
    }
}
