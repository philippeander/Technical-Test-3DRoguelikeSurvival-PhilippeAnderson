using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    public GameOver gameOverUI;
    private void Awake()
    {
        Instance = this;
    }

    public void StartLevelUp()
    {
        SkillSelectionUI.Instance.OpenSelection();
    }
    
    public bool PauseGame(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0f;
            return true;
        }
        else
        {
            Time.timeScale = 1f;
            return false;
        }
    }
    
    public void ShowGameOver()
    {
        gameOverUI.ShowGameOver();
    }
}
