using Microsoft.AspNetCore.Mvc;
using School.DTO;
using School.Services;

[ApiController]
[Route("api/[controller]")]
public class LanguagePreferenceController : ControllerBase
{
    private readonly ILanguagePreferenceService _languagePreferenceService;

    public LanguagePreferenceController(ILanguagePreferenceService languagePreferenceService)
    {
        _languagePreferenceService = languagePreferenceService;
    }

    [HttpGet]
    public IActionResult GetPreferredLanguage()
    {
        var language = _languagePreferenceService.GetPreferredLanguage();
        return Ok(new { language });
    }

    [HttpPost]
    public IActionResult SetPreferredLanguage([FromBody] LanguageRequestDTO request)
    {
        if (string.IsNullOrWhiteSpace(request.LanguageCode))
        {
            return BadRequest("Language code is required");
        }

        // Optional: Validate against supported languages
        var supportedLanguages = new[] { "en", "es", "fr", "de", "zh" };
        if (!supportedLanguages.Contains(request.LanguageCode.ToLower()))
        {
            return BadRequest($"Unsupported language. Supported: {string.Join(", ", supportedLanguages)}");
        }

        _languagePreferenceService.SetPreferredLanguage(request.LanguageCode);
        return Ok(new { message = "Language preference updated", language = request.LanguageCode });
    }
}
