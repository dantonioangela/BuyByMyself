using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuPrincipale : MonoBehaviour
{
    public Player_Controller player;
    public GameObject OptionsUI;
    public GameObject MainMenuUI;
    public AudioMixer audioMixer;
    public GameObject SceltaDiffUI;
	public static int levelDifficulty; //0 = facile, 1 = normale, 3 = difficile;
																			

    static public bool MainMenuActive = false;

    bool inOptions = false;
    bool inScelta = false;
    static public bool inGame = false;

    Resolution[] resolutions;
    public Dropdown MAINResolutionDropdownUI;

    public Animator LevelTransition;
    public float TransitionTime = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        Player_Controller.UI_active = true;
        MainMenuUI.SetActive(true);
        MainMenuActive = true;
        resolutions = Screen.resolutions;
        MAINResolutionDropdownUI.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        MAINResolutionDropdownUI.AddOptions(options);
        MAINResolutionDropdownUI.value = currentResolutionIndex;
        MAINResolutionDropdownUI.RefreshShownValue();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!inGame)
            {
                if (inOptions)
                {
                    Indietro();
                }

                if (inScelta)
                {
                    BackToMainMenu();
                }
            }

        }
    }

    public void StartGame()
    {
        MainMenuUI.SetActive(false);
        SceltaDiffUI.SetActive(true);
        inScelta = true;
    }

    public void Opzioni()
    {
        MainMenuUI.SetActive(false);
        OptionsUI.SetActive(true);
        inOptions = true;
    }

    public void Esci()
    {
        Debug.Log("Uscita applicazione...");
        Application.Quit();
    }

    public void Volume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void Indietro()
    {
        OptionsUI.SetActive(false);
        MainMenuUI.SetActive(true);
        inOptions = false;
    }

    public void Credits()
    {
        Debug.Log("Credits...");
    }

    public void Tutorial()
    {
        MainMenuActive = false;
        inGame = true;
        MainMenuUI.SetActive(false);
        SceneManager.LoadScene(1);
    }

   

    public void SetRisoluzione(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void BackToMainMenu()
    {
        SceltaDiffUI.SetActive(false);
        MainMenuUI.SetActive(true);
        inScelta = false;
    }

    public void PartitaFacile()
    {       
        MainMenuActive = false;
        SceltaDiffUI.SetActive(false);
        MainMenuUI.SetActive(false);
        inGame = true;
		levelDifficulty = 0;
        StartCoroutine(LoadFacile());
    }

    IEnumerator LoadFacile()
    {
        LevelTransition.SetTrigger("Start");
        yield return new WaitForSeconds(TransitionTime);
        LevelTransition.SetTrigger("End");
    }

    public void PartitaMedia()
    {
        MainMenuActive = false;
        SceltaDiffUI.SetActive(false);
        MainMenuUI.SetActive(false);
        inGame = true;
		levelDifficulty = 1;
        StartCoroutine(LoadMedia());
    }

    IEnumerator LoadMedia()
    {
        LevelTransition.SetTrigger("Start");
        yield return new WaitForSeconds(TransitionTime);
        LevelTransition.SetTrigger("End");
    }
    public void PartitaDifficile()
    {
        MainMenuActive = false;
        SceltaDiffUI.SetActive(false);
        MainMenuUI.SetActive(false);
        inGame = true;
		levelDifficulty = 2;
        StartCoroutine(LoadDifficile());
    }

    IEnumerator LoadDifficile()
    {
        LevelTransition.SetTrigger("Start");
        yield return new WaitForSeconds(TransitionTime);
        LevelTransition.SetTrigger("End");
    }

}
