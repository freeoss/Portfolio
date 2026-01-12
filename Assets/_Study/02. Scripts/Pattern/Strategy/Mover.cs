using UnityEngine;

public class Mover : MonoBehaviour
{
    public bool isRun, isFly, isSwim;

    private IMove move;
    private MoveRun run;
    private MoveFly flay;
    private MoveSwim swim;
    
    private void Update()
    {
        if (Input.anyKey)
        {
            
        }
    }
}
