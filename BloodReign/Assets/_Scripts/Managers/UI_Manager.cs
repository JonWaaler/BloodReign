using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {
	
	public GameObject mainmenu;
	public GameObject characterselect;
	public CanvasGroup PressA;
	
    public bool isTranslator = false;

	private float fade = 1f;
	private bool Fade = true;

    const string DLL_TRANSLATOR = "UI_Translator";

    // This function gets our translation
    [DllImport(DLL_TRANSLATOR)]
    private static extern System.IntPtr SendTranslation();

    // This function sets translation
    //      - Finds the text file "language" + ".txt"
    //      - In the text file searches for "ui_name" and return the right 
    [DllImport(DLL_TRANSLATOR)]
    private static extern void PackTranslation(string language, string ui_name);


    [Tooltip("Everything to be translated")]
    public List<Text> words;
    [Tooltip("If auto selection off, this dropdown holds the names of the languages")]
    public Dropdown langSelector;
    [Tooltip("Will use system settings to auto change language")]
    public bool detectLang = false;

    // Will look for English.txt as default
    private string langSelection = "English";


    void Awake()
	{
        if (!isTranslator)
        {
		    mainmenu.SetActive(true);
		    characterselect.SetActive(false);
        }
	}

	void FixedUpdate()
	{
        if (!isTranslator)
        {
            // Text fade in and out
            if (fade >= 1f)
                Fade = true;
            else if (fade <= 0.25f)
                Fade = false;

            if (Fade)
                fade -= Time.deltaTime;
            else
                fade += Time.deltaTime;

            PressA.alpha = fade;

            // if (characterselect.activeInHierarchy && Input.GetKeyDown("joystick 1 button 0"))
            // {
            //     //SceneManager.LoadScene(1);
            // }
        }
	}

    private void Update()
    {

        if (detectLang)
        {
            if(Application.systemLanguage == SystemLanguage.English)
            {
                print("System language: English");
                langSelection = "English";
            }
            else if(Application.systemLanguage == SystemLanguage.French)
            {
                print("System language: French");
                langSelection = "French";
            }
            else if (Application.systemLanguage == SystemLanguage.Spanish)
            {
                print("System language: Spanish");
                langSelection = "Spanish";
            }
        }

        if (!detectLang)
        {
            if (langSelector.value == 0)
                langSelection = "English";
            else if (langSelector.value == 1)
                langSelection = "French";
            else if (langSelector.value == 2)
                langSelection = "Spanish";
            else
                langSelection = "English";
        }

        if(words.Count > 0)
        {
            foreach (var word in words)
            {
                // Searches through language.txt to look for the name of the gameobject
                PackTranslation(langSelection, word.gameObject.name);

                // Bring retrieved translation to C#
                string retrievedTranslation = Marshal.PtrToStringAnsi(SendTranslation());

                // Debuging
                print(retrievedTranslation);

                word.text = retrievedTranslation;
            }
        }
        else
        {
            Debug.LogError("No words to translate", this);
        }
    }

    public void OnOff(GameObject canvas)
    {
        canvas.SetActive(!canvas.activeInHierarchy);
    }

    public void Play()
	{
		SceneManager.LoadScene(1);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
