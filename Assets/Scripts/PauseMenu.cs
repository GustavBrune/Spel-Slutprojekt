using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour

 {

    public GameObject pauseMenu; 

    public void Resume()
    {
        
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void QuitGame()

    {
        Application.Quit();
        Debug.Log("QUIT");

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
