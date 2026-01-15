using UnityEngine;

public class SkillCommand : ICommand
{
    private Unit unit;
    
    public SkillCommand(Unit unit)
    {
        this.unit = unit;
    }
    
    public void Excute()
    {
        unit.Skill();
    }

    public void Cancel()
    {
        unit.SkillCancel();
    }
}
