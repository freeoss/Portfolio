using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private Unit unit;

    private ICommand attackCommand;
    private ICommand moveCommand;
    private ICommand skillCommand;
    
    private Queue<ICommand> commandsQueue = new Queue<ICommand>();
    private Stack<ICommand> executeCommands = new Stack<ICommand>();

    private void Awake()
    {
        unit = GetComponent<Unit>();

        attackCommand = new AttackCommand(unit);
        moveCommand = new SkillCommand(unit);
        skillCommand = new MoveCommand(unit, "FireBall");
    }

    private void Update()
    {
        // 즉시 실행
        if (Input.GetKeyDown(KeyCode.Q))
        {
            attackCommand.Excute();
            executeCommands.Push(attackCommand);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            moveCommand.Excute();
            executeCommands.Push(moveCommand);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            skillCommand.Excute();
            executeCommands.Push(skillCommand);
        }
        
        // 누적 실행
        if (Input.GetKeyDown(KeyCode.Z))
        {
            commandsQueue.Enqueue(attackCommand);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            commandsQueue.Enqueue(moveCommand);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            commandsQueue.Enqueue(skillCommand);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            while (commandsQueue.Count > 0)
            {
                ICommand command = commandsQueue.Dequeue();
                command.Excute();
                executeCommands.Push(command);
            }
        }

        // 한 번에 실행
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (executeCommands.Count > 0 )
            {
                ICommand lastCommand = executeCommands.Pop();
                Debug.Log($"명령 취소: {lastCommand.GetType().Name}");
                lastCommand.Cancel();
            }
            else
            {
                Debug.Log("더 이상 취소할 명령이 없습니다.");
            }
        }
    }
}
