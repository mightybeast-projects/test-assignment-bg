using System.Collections;
using UnityEngine;
using System.IO;

/// <summary>
/// This class is the manager of the system.
/// it allow us to open XMLs files and read them.
/// </summary>
public class LanguageManager : MonoBehaviour
{
    [Header("Languages")]

    //Those variables must be marked with "HideInInspector" due we want to dinamically show-hide them, this work is managed by "LanguageManagerEditor.cs".
    //Those variables contains the XML files as TextAssets. Those XMLs must be present in the "GAMEPROJECT_Data" when we build a project.

    public static LanguageManager instance; //For the singleton pattern.
    
    public LanguageReader langReader; //We must have a LanguageReader inside our class to "strip the flesh off" of the XML.

    public Message _message; //Also we need a reference to the Message.

    [HideInInspector]
    public bool opened = false; //This bool will tell to us if the XML is opened and so totally readed
    public string CurrentLanguage = "English"; //This variable will contain as a string the current language that we've selected

    //This bool that's also modificable at runtime will tell the script to look for the XML files on the web or locally.
    private bool isLocal = true;

    /// <summary>
    /// Singleton pattern
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(gameObject);

    }

    ///At the start, by default we set the English language, the XML file took depends with the bool "isLocal"
    private void Start()
    {
        OpenLocalXML("English");
    }


    /// <summary>
    /// This function allow us to open a XML file stored on the internet, we use a WWW class that returns a WWW.text, that will contains the XML text.
    /// </summary>

    /// <summary>
    /// This function allow us to open a XML file stored on the computer, in the GAMENAME_Data folder created by the unity build.
    /// </summary>
    public void OpenLocalXML(string Language)
    {
        //We're opening a file, so we reset all the states of this script
        opened = false;
        langReader = null;
        CurrentLanguage = null;

        //Switch for the "Language" (as parameter), foreach language present in the game we have a different name file, but the location of those is the same.
        //Despite from the Web opening, here we instantiate the LanguageReader instantaniely, because the file must be not loaded from the web cause we've got it on the hard-disk.
        switch (Language)
        {
            case "English":
                langReader = new LanguageReader("Lang/ENG", "English", true);
                break;
            case "Ukrainian":
                langReader = new LanguageReader("Lang/UKR", "Ukrainian", true);
                break;
            default:
                langReader = new LanguageReader("Lang/ENG", "English", true);
                break;
        }

        CurrentLanguage = Language;

        opened = true; //The file is opened

        StartCoroutine("ResetUpdateState");

    }

    /// <summary>
    /// This function will allow us to change the language of the game.
    /// </summary>

    public void SelectLanguage(string Language, bool isLocal)
    {
        //_message.gameObject.SetActive(true);
        //_message.ChangeMessage(langReader.getString("MESSAGE_LOADING_LANGUAGE"));

        if(Language != CurrentLanguage) //If we are not selecting the same language we have right now
        OpenLocalXML(Language);
    }

    IEnumerator ResetUpdateState()
    {
        yield return new WaitForSeconds(0.01f);
        langReader.update = false;
    }

}
