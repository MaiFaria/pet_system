using Microsoft.AspNetCore.Mvc;
using PS.Core.Communication;

namespace PS.WebApp.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponseHasErrors(ResponseResult response)
        {
            if (response != null && response.Errors.Messages.Any())
            {
                foreach (var message in response.Errors.Messages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }

                return true;
            }

            return false;
        }

        protected void AddErrorValidation(string message)
        {
            ModelState.AddModelError(string.Empty, message);
        }

        protected bool ValidOperation()
        {
            return ModelState.ErrorCount == 0;
        }
    }
}
