using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public Canvas pauseMenu;
    public GameObject playerPlane;
    public GameObject controlsPage;
    public GameObject optionsPage;
    public GameObject audioManagerObj;

    private bool gamePaused;
    private StationManager stationManager;
    private SelectWheel selectWheel;
    private bool showCursor;
    private AudioManager audioManager;
    private AudioSource[] ambiance;

    private CursorLockMode cursorLockState;
    // Start is called before the first frame update
    void Start()
    {
        gamePaused = false;
        stationManager = playerPlane.GetComponentInChildren<StationManager>();
        selectWheel = playerPlane.GetComponentInChildren<SelectWheel>();
        int childSize = audioManagerObj.transform.childCount;
        audioManager = audioManagerObj.GetComponent<AudioManager>();
        ambiance = new AudioSource[childSize];
        for (int i = 0; i < childSize; ++i)
        {
            ambiance[i] = audioManagerObj.transform.GetChild(i).GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePauseGame();
        }
    }

    public void TogglePauseGame()
    {
        if (!gamePaused)
        {
            Time.timeScale = 0.0f;
            gamePaused = true;
            pauseMenu.gameObject.SetActive(true);
            SetStationControlActive(false);
            ToggleAmbianceMsuic(false);
            selectWheel.gameObject.SetActive(false);
            showCursor = Cursor.visible;
            Cursor.visible = true;
            cursorLockState = Cursor.lockState;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            gamePaused = false;
            DisableOtherMenus();
            pauseMenu.gameObject.SetActive(false);
            SetStationControlActive(true);
            ToggleAmbianceMsuic(true);
            selectWheel.gameObject.SetActive(true);
            Cursor.lockState = cursorLockState;
            Cursor.visible = showCursor;
            Time.timeScale = 1.0f;
        }
    }

    public void RestartGame()
    {
        if (gamePaused)
            TogglePauseGame();
        StationManager.currentlyActiveControlScheme = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ViewControls()
    {
        controlsPage.SetActive(!controlsPage.activeSelf);
        pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
    }

    public void ViewOptions()
    {
        optionsPage.SetActive(!optionsPage.activeSelf);
        pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
    }

    public void LoadMainMenu()
    {
        if (gamePaused)
            TogglePauseGame();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StationManager.currentlyActiveControlScheme = null;
        SceneManager.LoadScene("MainMenu");
    }

    private void DisableOtherMenus()
    {
        controlsPage.SetActive(false);
        optionsPage.SetActive(false);
    }

    private void SetStationControlActive(bool isActive)
    {
        Station currentStation = StationManager.currentCenterStation;
        currentStation.controlScheme.SetActiveControl(isActive);
    }

    private void ToggleAmbianceMsuic(bool playMusic)
    {
        if (playMusic)
        {
            for (int i = 0; i < ambiance.Length; ++i)
            {
                ambiance[i].Play();
            }
            audioManager.Play("General", 0);
        }
        else
        {
            for (int i = 0; i < ambiance.Length; ++i)
            {
                ambiance[i].Stop();
            }
            audioManager.Stop("General", 0);
        }
    }
}
