/*using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace finalexam_back
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 400 && context.Response.HasStarted == false)
            {
                var modelState = context.Features.Get<Microsoft.AspNetCore.Mvc.ModelStateDictionary>();
                if (modelState != null && !modelState.IsValid)
                {
                    var errors = modelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new { errors }));
                }
            }
        }
    }
}
*/