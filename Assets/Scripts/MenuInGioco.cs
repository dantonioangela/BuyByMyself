using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class MenuInGioco : MonoBehaviour
{

    static public bool GiocoInPausa = false;
    public Player_Controller player;

    public GameObject PauseMenuUI;
    public GameObject OptionsMenuUI;
    public GameObject MainMenuUI;

    public AudioMixer audioMixer;

    Resolution[] resolutions;
    public Dropdown ResolutionDropdownUI;

    bool inOptions = false;

    private List<string> options = new List<string>();

    private void Start()
    {
        resolutions = GetResolutions().ToArray();
        ResolutionDropdownUI.ClearOptions();
        options.Clear();
        int currentResolutionIndex = 0;
        for(int i=0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
            currentResolutionIndex = i;
        }
        ResolutionDropdownUI.AddOptions(options);
        ResolutionDropdownUI.value = currentResolutionIndex;
        ResolutionDropdownUI.RefreshShownValue();
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!MenuPrincipale.MainMenuActive && ! Player_Controller.UI_active)
            {
                if (GiocoInPausa)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }

            if (inOptions)
            {
                Indietro();
            }
        }
    }
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Player_Controller.UI_active = false;
        Time.timeScale = 1f;
        GiocoInPausa = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Player_Controller.UI_active = true;
        Time.timeScale = 0f;
        GiocoInPausa = true;
    }

    public void Opzioni()
    {
        PauseMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(true);
        MenuPrincipale.MainMenuActive = true;
        inOptions = true;
    }

    public void MainMenu()
    {
        //PauseMenuUI.SetActive(false);
        //MainMenuUI.SetActive(true);
        //MenuPrincipale.MainMenuActive = true;
        //MenuPrincipale.inGame = false;
        Time.timeScale = 1f;
        //SceneManager.LoadScene(0);
        StartCoroutine(Reload());
    }

    
    public void Indietro()
    {
        OptionsMenuUI.SetActive(false);
        PauseMenuUI.SetActive(true);
        MenuPrincipale.MainMenuActive = false;
        inOptions = false;
    }

    public void Volume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetGrafica(int graficaIndex)
    {
        QualitySettings.SetQualityLevel(graficaIndex);
    }

    public void SchermoIntero(bool isSchermoIntero)
    {
        Screen.fullScreen = isSchermoIntero;
    }

    public void SetRisoluzione(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private IEnumerator Reload()
    {
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(0);
        yield return null;
    }

}
