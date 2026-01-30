using System.Collections.Generic;
using NUnit.Framework;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeController : MonoBehaviourPun
{
    public GameObject tailPrefab;
    public List<Transform> tailPoints = new List<Transform>();
    
    public Transform coinTransform;
    
    private Vector3 moveInput;
    public float moveSpeed = 5f;
    public float turnSpeed = 120f;
    public float lerpSpeed = 5f;

    private bool isCoin = false;

    private MeshRenderer headRenderer;
    
    private void Start()
    {
        if (coinTransform == null)
        {
            coinTransform = GameObject.FindGameObjectWithTag("Coin").transform;
        }

        headRenderer = GetComponent<MeshRenderer>();

        if (photonView.IsMine)
        {
            headRenderer.material.color = Color.green;
        }
        else
        {
            headRenderer.material.color = Color.red;
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            MoveHead();
        }
        
        MoveTail();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            if (photonView.IsMine && !isCoin)
            {
                isCoin = true;
                
                // MoveCoin();
                photonView.RPC("MoveCoin", RpcTarget.MasterClient);
            }
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void MoveHead()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * moveInput.x, -turnSpeed * Time.deltaTime);
        
    }

    void MoveTail()
    {
        Transform target = transform;   // 처음에는 Target을 Snake로 설정

        foreach (var tail in tailPoints)
        {
            Vector3 pos = target.position;
            Quaternion rot = target.rotation;
            
            tail.position = Vector3.Lerp(tail.position, pos, lerpSpeed * Time.deltaTime);
            tail.rotation = Quaternion.Lerp(tail.rotation, rot, lerpSpeed * Time.deltaTime);

            target = tail;  // 현재 꼬리를 Target으로 설정
        }
    }
    
    [PunRPC]
    private void AddTail()
    {
        Vector3 spawnPos = transform.position;

        if (tailPoints.Count > 0)
        {
            spawnPos = tailPoints[tailPoints.Count - 1].position;
        }
        
        GameObject newTail = Instantiate(tailPrefab, spawnPos, Quaternion.identity);
        tailPoints.Add(newTail.transform);
        
        newTail.GetComponent<Tail>().SetSnake(this, photonView);

        MeshRenderer tailRenderer = newTail.GetComponent<MeshRenderer>();
        
        if (photonView.IsMine)
        {
            tailRenderer.material.color = Color.green;
        }
        else
        {
            tailRenderer.material.color = Color.red;
        }
    }

    [PunRPC]
    private void MoveCoin()
    {
        float randomX = Random.Range(-13f, 13f);
        float randomY = Random.Range(-4f, 4f);
        Vector3 pos = new Vector3(randomX, randomY, 0);
        
        photonView.RPC("SetCoinPosition", RpcTarget.AllBufferedViaServer, pos);
    }

    [PunRPC]
    private void SetCoinPosition(Vector3 newPos)
    {
        if (coinTransform == null)
        {
            coinTransform = GameObject.FindGameObjectWithTag("Coin").transform;
        }
        
        coinTransform.position = newPos;

        if (photonView.IsMine)
        {
            isCoin = false;
        }
        
        AddTail();
    }

    [PunRPC]
    public void Death()
    {
        GetComponent<Collider>().enabled = false;
        foreach (var tail in tailPoints)
        {
            tail.gameObject.SetActive(false);
        }
        
        this.enabled = false;
    }

    public int GetTailCount()
    {
        return tailPoints.Count;
    }
}
