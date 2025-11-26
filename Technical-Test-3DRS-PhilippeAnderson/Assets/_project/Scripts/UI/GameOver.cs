using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [Header("UI")]
    public GameObject pausePanel;     // O painel completo (Canvas > PausePanel)
    public Button restartButton;
    public Button ExitGameButton;

    private void Start()
    {
        pausePanel.SetActive(false);

        // Liga os botões
        restartButton.onClick.AddListener(RestartGame);
        ExitGameButton.onClick.AddListener(QuitGame);
    }
    
    public void ShowGameOver()
    {
        pausePanel.SetActive(true);
        GameManager.Instance.PauseGame(true);
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
