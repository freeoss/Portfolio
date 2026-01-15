using UnityEngine;

public class AttackCommand : ICommand
{
    public Unit unit;

    public AttackCommand(Unit unit)
    {
        this.unit = unit;
    }
    
    public void Excute()
    {
        unit.Attack();
    }

    public void Cancel()
    {
        unit.AttackCancel();
    }
}
