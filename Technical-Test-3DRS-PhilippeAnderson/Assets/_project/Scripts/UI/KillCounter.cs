using System;
using UnityEngine;
using TMPro;

public class KillCounter : MonoBehaviour
{
    static public KillCounter Instance;
    public TextMeshProUGUI killText;

    private int killCount = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddKill()
    {
        killCount++;
        UpdateUI();
    }

    public int GetKillCount()
    {
        return killCount;
    }

    private void UpdateUI()
    {
        killText.text = killCount.ToString();
    }
}