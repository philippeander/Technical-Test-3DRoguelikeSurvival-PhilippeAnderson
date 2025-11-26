using UnityEngine;

[CreateAssetMenu(menuName = "Game/Skill Database")]
public class SkillDatabase : ScriptableObject
{
    public SkillData[] skills;

    public SkillData GetRandomSkill()
    {
        return skills[Random.Range(0, skills.Length)];
    }
}