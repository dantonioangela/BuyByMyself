using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
public class tutorial_pausa_menu : MonoBehaviour
{

    static public bool GiocoInPausa = false;
    public tutorial_player_controller player;

    public GameObject PauseMenuUI;
    public GameObject OptionsMenuUI;
    public GameObject MainMenuUI;
    public GameObject MessaggiCassieraUI;

    public AudioMixer audioMixer;

    Resolution[] resolutions;
    public Dropdown ResolutionDropdownUI;

    bool inOptions = false;

    public Animator LevelTransition;
    public float TransitionTime = 2f;

    private void Start()
    {
        resolutions = Screen.resolutions;
        ResolutionDropdownUI.ClearOptions();
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
        ResolutionDropdownUI.AddOptions(options);
        ResolutionDropdownUI.value = currentResolutionIndex;
        ResolutionDropdownUI.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!MenuPrincipale.MainMenuActive)
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
        player.UI_active = false;
        Time.timeScale = 1f;
        GiocoInPausa = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        player.UI_active = true;
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
        Time.timeScale = 1f;
        GiocoInPausa = false;
        PauseMenuUI.SetActive(false);
        MessaggiCassieraUI.SetActive(false);
        StartCoroutine(ReturnMainMenu());
    }
        

    IEnumerator ReturnMainMenu()
    {
        LevelTransition.SetTrigger("Start");
        yield return new WaitForSeconds(TransitionTime);
        SceneManager.LoadScene(0);
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
}
