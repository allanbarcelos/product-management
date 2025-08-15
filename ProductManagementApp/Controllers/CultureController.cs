using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;

namespace ProductManagementApp.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CultureController : Controller
    {
        public IActionResult Set(string culture, string redirectUri)
        {
            if (culture != null)
            {
                var reqCulture = new RequestCulture(culture, culture);
                var cookieName = CookieRequestCultureProvider.DefaultCookieName;
                var cookieValue = CookieRequestCultureProvider.MakeCookieValue(reqCulture);

                HttpContext.Response.Cookies.Append(
                    cookieName,
                    cookieValue,
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            }

            return LocalRedirect(redirectUri);
        }
    }
}