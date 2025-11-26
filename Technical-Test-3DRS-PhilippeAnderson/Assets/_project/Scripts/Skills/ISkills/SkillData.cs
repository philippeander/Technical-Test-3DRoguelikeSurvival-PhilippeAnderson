using UnityEngine;

public enum SkillType { Melee, ProjectileForward, Projectile4Dir, DamageField, Orbit, SpeedBuff, Heal }

[CreateAssetMenu(menuName = "Game/Skill")]
public class SkillData : ScriptableObject
{
    public Sprite icon;
    public SkillType type;
    public string skillName;
    public float cooldown = 3f;
    public float duration = 0f;     // Para habilidades com duração
    public float value = 0f;        // Dano, velocidade, cura, etc.
    public GameObject prefab;       // Para projéteis, campos, orbitais, etc.
}