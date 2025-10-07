namespace School.Services
{
    public interface ILanguagePreferenceService
    {
        string GetPreferredLanguage();
        void SetPreferredLanguage(string languageCode);
    }
}