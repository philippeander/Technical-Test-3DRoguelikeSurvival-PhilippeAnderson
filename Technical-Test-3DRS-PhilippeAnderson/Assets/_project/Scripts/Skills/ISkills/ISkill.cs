public interface ISkill
{
    void Initialize(PlayerCombat owner, SkillData data);
    void Tick();
}