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

    private bool gamePaused;
    private StationManager stationManager;
    private SelectWheel selectWheel;
    private bool showCursor;

    private CursorLockMode cursorLockState;
    // Start is called before the first frame update
    void Start()
    {
        gamePaused = false;
        stationManager = playerPlane.GetComponentInChildren<StationManager>();
        selectWheel = playerPlane.GetComponentInChildren<SelectWheel>();
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
            stationManager.gameObject.SetActive(false);
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
            stationManager.gameObject.SetActive(true);
            selectWheel.gameObject.SetActive(true);
            Cursor.lockState = cursorLockState;
            Cursor.visible = showCursor;
            Time.timeScale = 1.0f;
        }
    }

    public void RestartGame()
    {
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
}
