namespace School.Services
{
    public class LanguagePreferenceService : ILanguagePreferenceService
    {
        private const string CookieName = "PreferredLanguage";
        private const string DefaultLanguage = "en";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LanguagePreferenceService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetPreferredLanguage()
        {
            var context = _httpContextAccessor.HttpContext;

            if (context == null) return DefaultLanguage; // context is null

            if (context.Request.Cookies.TryGetValue( CookieName, out var languageCode )) return languageCode; // there's a language cookie

            return DefaultLanguage; // There's context, but no language cookie
        }

        public void SetPreferredLanguage(string languageCode)
        {
            var context = _httpContextAccessor.HttpContext;

            if (context == null) return;

            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddYears(1)
            };

            context.Response.Cookies.Append(CookieName, languageCode, cookieOptions);
        }
    }
}