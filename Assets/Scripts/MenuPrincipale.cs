using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class MenuPrincipale : MonoBehaviour
{
    public Player_Controller player;
    public GameObject OptionsUI;
    public GameObject MainMenuUI;
    public AudioMixer audioMixer;
    public GameObject SceltaDiffUI;
	public static int levelDifficulty; //0 = facile, 1 = normale, 2 = difficile;
    private AudioManager audioManager;
    public Slider volSlider;

    public GameObject CreditsUI;
    public GameObject ComandiUI;

    bool inCredits = false;
    bool inComandi = false;

    static public bool MainMenuActive = false;

    bool inOptions = false;
    bool inScelta = false;
    static public bool inGame = false;

    Resolution[] resolutions;
    public Dropdown MAINResolutionDropdownUI;

    public Animator LevelTransition;
    public float TransitionTime = 0.8f;

    private List<string> options = new List<string>();

    public GameObject SchermataFacileGame;
    public GameObject SchermataMediaGame;
    public GameObject SchermataDifficileGame;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("MainMenuMusic");
        Player_Controller.UI_active = true;
        MainMenuUI.SetActive(true);
        MainMenuActive = true;
        resolutions = GetResolutions().ToArray();
        MAINResolutionDropdownUI.ClearOptions();
        options.Clear();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            currentResolutionIndex = i;
        }

        MAINResolutionDropdownUI.AddOptions(options);
        MAINResolutionDropdownUI.value = currentResolutionIndex;
        MAINResolutionDropdownUI.RefreshShownValue();
    }

    public static List<Resolution> GetResolutions()
    {
        //Filters out all resolutions with low refresh rate:
        Resolution[] resolutions = Screen.resolutions;
        HashSet<System.Tuple<int, int>> uniqResolutions = new HashSet<Tuple<int, int>>();
        Dictionary<Tuple<int, int>, int> maxRefreshRates = new Dictionary<Tuple<int, int>, int>();
        for (int i = 0; i < resolutions.GetLength(0); i++)
        {
            //Add resolutions (if they are not already contained)
            Tuple<int, int> resolution = new Tuple<int, int>(resolutions[i].width, resolutions[i].height);
            uniqResolutions.Add(resolution);
            //Get highest framerate:
            if (!maxRefreshRates.ContainsKey(resolution))
            {
                maxRefreshRates.Add(resolution, resolutions[i].refreshRate);
            }
            else
            {
                maxRefreshRates[resolution] = resolutions[i].refreshRate;
            }
        }
        //Build resolution list:
        List<Resolution> uniqResolutionsList = new List<Resolution>(uniqResolutions.Count);
        foreach (Tuple<int, int> resolution in uniqResolutions)
        {
            Resolution newResolution = new Resolution();
            newResolution.width = resolution.Item1;
            newResolution.height = resolution.Item2;
            if (maxRefreshRates.TryGetValue(resolution, out int refreshRate))
            {
                newResolution.refreshRate = refreshRate;
            }
            uniqResolutionsList.Add(newResolution);
        }
        return uniqResolutionsList;
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

                if (inCredits)
                {
                    RitornoDaCredits();
                }

                if (inComandi)
                {
                    RitornoDaComandi();
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
        volSlider.value = volume.vol;
        inOptions = true;
    }

    public void Esci()
    {
        Application.Quit();
    }

    public void onValueChanged(System.Single value)
    {
        audioManager.setGeneralVolume(value);
    }

    public void Indietro()
    {
        OptionsUI.SetActive(false);
        MainMenuUI.SetActive(true);
        inOptions = false;
    }

    public void Credits()
    {
        MainMenuUI.SetActive(false);
        CreditsUI.SetActive(true);
        inCredits = true;
    }

    public void Comandi()
    {
        MainMenuUI.SetActive(false);
        ComandiUI.SetActive(true);
        inComandi = true;
    }

    public void RitornoDaCredits()
    {
        CreditsUI.SetActive(false);
        MainMenuUI.SetActive(true);
        inCredits = false;
    }

    public void RitornoDaComandi()
    {
        ComandiUI.SetActive(false);
        MainMenuUI.SetActive(true);
        inComandi = false;
    }

    public void Tutorial()
    {
        MainMenuActive = false;
        inGame = true;
        MainMenuUI.SetActive(false);
        StartCoroutine(StartTutorial());
    }

    IEnumerator StartTutorial()
    {
        LevelTransition.SetTrigger("Start");
        yield return new WaitForSeconds(TransitionTime);
        LevelTransition.SetTrigger("End");
        yield return new WaitForSeconds(1.3f);
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

    public void Skip()
    {
        MainMenuActive = false;
        inGame = true;
        MainMenuUI.SetActive(false);
        MainMenuActive = false;
        if (levelDifficulty == 0)
        {
            SchermataFacileGame.SetActive(false);
            audioManager.Stop("schermata_facile_audio");
        } else if (levelDifficulty == 1)
        {
            SchermataMediaGame.SetActive(false);
            audioManager.Stop("schermata_media_audio");
        } else
        {
            SchermataDifficileGame.SetActive(false);
            audioManager.Stop("schermata_difficile_audio");
        }
        Player_Controller.UI_active = false;
    }

    public void PartitaFacile()
    {
        SceltaDiffUI.SetActive(false);
        levelDifficulty = 0;
        Player_Controller.UI_active = true;
        StartCoroutine(LoadFacile());
    }

    IEnumerator LoadFacile()
    {
        LevelTransition.SetTrigger("Start");
        yield return new WaitForSeconds(TransitionTime);
        LevelTransition.SetTrigger("End");
        audioManager.Play("GameplayMusic");
        yield return new WaitForSeconds(1.3f);
        SchermataFacileGame.SetActive(true);
        audioManager.PlayInstance("schermata_facile_audio");
    }

    public void PartitaMedia()
    {
        SceltaDiffUI.SetActive(false);
		levelDifficulty = 1;
        Player_Controller.UI_active = true;
        StartCoroutine(LoadMedia());
    }

    IEnumerator LoadMedia()
    {
        LevelTransition.SetTrigger("Start");
        yield return new WaitForSeconds(TransitionTime);
        LevelTransition.SetTrigger("End");
        audioManager.Play("GameplayMusic");
        yield return new WaitForSeconds(1.3f);
        SchermataMediaGame.SetActive(true);
        audioManager.PlayInstance("schermata_media_audio");
    }
    public void PartitaDifficile()
    {
        SceltaDiffUI.SetActive(false);
		levelDifficulty = 2;
        Player_Controller.UI_active = true;
        StartCoroutine(LoadDifficile());
    }

    IEnumerator LoadDifficile()
    {
        LevelTransition.SetTrigger("Start");
        yield return new WaitForSeconds(TransitionTime);
        LevelTransition.SetTrigger("End");
        audioManager.Play("GameplayMusic");
        yield return new WaitForSeconds(1.3f);
        SchermataDifficileGame.SetActive(true);
        audioManager.PlayInstance("schermata_difficile_audio");
    }

}
