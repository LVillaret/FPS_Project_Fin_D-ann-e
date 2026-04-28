using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool _isPaused;
    
    public GameObject pausePanel;
   
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        _isPaused = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        _isPaused = false;
    }
}
