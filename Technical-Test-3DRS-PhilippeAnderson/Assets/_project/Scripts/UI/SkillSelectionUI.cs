using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SkillSelectionUI : MonoBehaviour
{
    static public SkillSelectionUI Instance;
    
    [Header("UI")]
    public GameObject panel;                 // O painel da UI
    public Button option1Button;
    public Button option2Button;
    public TextMeshProUGUI option1Text;
    public TextMeshProUGUI option2Text;
    public Image option1Icon;
    public Image option2Icon;

    [Header("Sistema de Skills")]
    public SkillDatabase skillDatabase;      // ScriptableObject (lista de skills)
    public PlayerCombat playerCombat;        // Componente no Player

    private SkillData chosenSkill1;
    private SkillData chosenSkill2;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        panel.SetActive(false);

        // Liga os botões
        option1Button.onClick.AddListener(() => SelectSkill(chosenSkill1));
        option2Button.onClick.AddListener(() => SelectSkill(chosenSkill2));
    }

    public void OpenSelection()
    {
        panel.SetActive(true);
        GameManager.Instance.PauseGame(true);

        // Sorteia 2 skills diferentes
        chosenSkill1 = skillDatabase.GetRandomSkill();
        do
        {
            chosenSkill2 = skillDatabase.GetRandomSkill();
        }
        while (chosenSkill2 == chosenSkill1);

        // Atualiza textos
        option1Text.text = chosenSkill1.skillName;
        option2Text.text = chosenSkill2.skillName;
        option1Icon.sprite = chosenSkill1.icon;
        option2Icon.sprite = chosenSkill2.icon;
    }

    private void SelectSkill(SkillData skill)
    {
        // Adiciona skill ao player
        playerCombat.AddSkill(skill);

        // Fecha UI
        panel.SetActive(false);
        GameManager.Instance.PauseGame(false);
    }
}