using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillUIManager : MonoBehaviour
{
    static public SkillUIManager Instance;
    
    [SerializeField] private Transform panelContainer;  
    [SerializeField] private Image skillIconPrefab;  

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        skillIconPrefab.gameObject.SetActive(false);
    }

    public void AddSkillIcon(Sprite skillSprite)
    {
        Image newIcon = Instantiate(skillIconPrefab, panelContainer);
        newIcon.gameObject.SetActive(true);
        newIcon.sprite = skillSprite;
    }
}