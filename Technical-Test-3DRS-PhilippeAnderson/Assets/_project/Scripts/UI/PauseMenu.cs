using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject pausePanel;     // O painel completo (Canvas > PausePanel)
    public Button resumeButton;
    public Button restartButton;
    public Button ExitGameButton;

    private bool isPaused = false;

    private void Start()
    {
        pausePanel.SetActive(false);

        // Liga os botões
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        ExitGameButton.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }
    
    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        GameManager.Instance.PauseGame(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        GameManager.Instance.PauseGame(false);
    }

    public void RestartGame()
    {
        GameManager.Instance.PauseGame(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}