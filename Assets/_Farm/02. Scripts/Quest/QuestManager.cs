using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : SingletonCore<QuestManager>, ISubject
{
    private List<IObserver> observers = new List<IObserver>();

    [SerializeField] private Button[] questButtons;
    [SerializeField] private QuestData[] questDatas;

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < questButtons.Length; i++)
        {
            // 클로져 이슈 방지
            int j = i;
            // 매개 변수가 있는 리스너 등록에는 람다식 사용
            questButtons[i].onClick.AddListener(() => SetButton(j));
        }
    }

    private void SetButton(int index)
    {
        Quest quest = new Quest(questDatas[index]);
        questButtons[index].gameObject.SetActive(false);
    }

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
        Debug.Log($"{observer.QuestName}을 등록하였습니다.");
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
        Debug.Log($"{observer.QuestName}을 삭제하였습니다.");
    }

    public void NotifyListener(string questName)
    {
        // 역순 실행
        for (int i = observers.Count - 1; i >= 0; i--)
        {
            observers[i].Notify(questName);
        }
    }
}
