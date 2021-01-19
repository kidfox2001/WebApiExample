using Microsoft.AspNet.Identity;
using WebApiExample.Models;
using System.Web.Http;

namespace WebApiExample.Controllers
{
    [RoutePrefix("api/Accounts")]
    public class AccountController : ApiController
    {
        private readonly AuthRepository authRepository;

        public AccountController()
        {
            authRepository = new AuthRepository();
        }

        [AllowAnonymous]
        [Route("Register")]
        public IHttpActionResult Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            IdentityResult resultRegister = authRepository.Register(userModel);
            IHttpActionResult httpErrorResult = GetErrorResult(resultRegister);

            if (httpErrorResult != null)
                return httpErrorResult;

            return Ok();
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
                return InternalServerError();

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                    return BadRequest();

                return BadRequest(ModelState);
            }

            return null;
        }
    }

}
