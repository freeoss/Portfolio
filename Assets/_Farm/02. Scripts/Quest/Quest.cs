using UnityEngine;

public class Quest : IObserver
{
    private QuestData questData;
    public string QuestName { get; private set; }
    
    public int CurrentCount { get; private set; }
    public bool IsCompleted { get; private set; }
    
    public Quest(QuestData data)
    {
        this.questData = data;
        CurrentCount = 0;
        IsCompleted = false;
        QuestName = data.questName;
        
        QuestManager.Instance.AddObserver(this);
    }

    public void Notify(string questName)
    {
        if (questData.questName == questName && !IsCompleted)
        {
            CurrentCount++;
            
            if (CurrentCount >= questData.requestCount)
            {
                IsCompleted = true;
                Debug.Log($"{questData.questName} 완료");
                
                QuestManager.Instance.RemoveObserver(this);
            }
        }
    }
}
