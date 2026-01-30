using UnityEngine;
using Photon.Pun;

public class Tail : MonoBehaviour
{
    private SnakeController mySnake;
    private PhotonView myPV;
    private void OnTriggerEnter(Collider other)
    {
        SnakeController otherSnake = other.GetComponent<SnakeController>();
        if (otherSnake != null && mySnake != null)
        {
            if (otherSnake != mySnake && myPV.IsMine)
            {
                // otherSnake.GetComponent<Collider>().enabled = false;
                // otherSnake.enabled = false;
                // Debug.Log($"{otherSnake}를 잡았습니다.");
                
                // otherSnake.Death(out otherTailCount);
                PhotonView otherPV = other.GetComponent<PhotonView>();
                otherPV.RPC("Death", RpcTarget.AllBufferedViaServer);

                int otherTailCount = otherSnake.GetTailCount();
                for (int i = 0; i < otherTailCount; i++)
                {
                    // mySnake.AddTail();
                    myPV.RPC("AddTail", RpcTarget.AllBufferedViaServer);
                }
                
                Debug.Log($"뱀을 잡았습니다. 획득 꼬리 개수 : {otherTailCount}");
            }
        }
    }

    public void SetSnake(SnakeController snake, PhotonView pv)
    {
        mySnake = snake;
        myPV = pv;
    }
}
