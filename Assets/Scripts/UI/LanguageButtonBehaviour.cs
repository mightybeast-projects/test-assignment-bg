using UnityEngine;

public class LanguageButtonBehaviour : MonoBehaviour
{
    [SerializeField] private string _language;

    private LanguageManager _languageManager;

    private void Awake()
    {
        _languageManager = FindObjectOfType<LanguageManager>();
    }

    public void SwitchLanguage()
    {
        _languageManager.SelectLanguage(_language, true);
    }
}