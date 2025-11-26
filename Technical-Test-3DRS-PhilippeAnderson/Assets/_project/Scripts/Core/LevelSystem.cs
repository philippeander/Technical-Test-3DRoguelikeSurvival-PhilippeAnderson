using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour {
    
    public static LevelSystem Instance;
    public int level = 1;
    public int currentXP = 0;
    public int xpToNextLevelBase = 50;
    
    public Slider xpSlider;
    public TextMeshProUGUI levelText;

    public event Action<int,int> OnXPChanged; // currentXP, xpToNext

    public int XPToNext => xpToNextLevelBase + (level - 1) * 20;
    
    void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        xpSlider.maxValue = XPToNext;
        xpSlider.value = currentXP;
        levelText.text = "Level " + level;
    }

    public void AddXP(int amount) {
        currentXP += amount;
        
        xpSlider.value = currentXP;
        
        if (currentXP >= XPToNext) {
            currentXP -= XPToNext;
            level++;
            // trigger level up process
            xpSlider.maxValue = XPToNext;
            levelText.text = "Level " + level;
            
            GameManager.Instance?.StartLevelUp();
        }
        OnXPChanged?.Invoke(currentXP, XPToNext);
    }

}