using UnityEngine;

public class MoveCommand : ICommand
{
    private Unit unit;
    private string skillName;

    public MoveCommand(Unit unit, string skillName)
    {
        this.unit = unit;
        this.skillName = skillName;
    }
    
    public void Excute()
    {
        unit.Move();
    }

    public void Cancel()
    {
        unit.MoveCancel();
    }
}
