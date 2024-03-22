using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Pause menu script responsible for pausing/resuming based on 'Esc' button press

public class PauseMenu : MonoBehaviour
{

    public bool paused = false;
    public GameObject pauseMenuObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    // Pauses game time and activates pause menu
    void Pause()
    {
        pauseMenuObj.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    // Resumes game time and deactivates pause menu
    public void Resume()
    {
        pauseMenuObj.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    // Sends player to main menu
    public void MainMenu()
    {
        Time.timeScale = 1f;
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadLevel(0);
    }
}
