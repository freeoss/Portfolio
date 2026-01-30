using Controller;
using Photon.Pun;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleMovement : MonoBehaviourPun
{
    public GameObject hat;
    
    public TextMeshPro nicknameText;
    
    private Vector3 moveInput;

    public float moveSpeed = 5f;
    public float turnSpeed = 10f;

    private void Start()
    {
        if (photonView.IsMine)
        {
            nicknameText.text = PhotonNetwork.NickName;
            nicknameText.color = Color.green;
        }
        else
        {
            nicknameText.text = photonView.Owner.NickName;
            nicknameText.color = Color.red;
        }
    }

    void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();
        moveInput = new Vector3(input.x, 0, input.y);
    }

    void OnHatOn(InputValue value)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        
        photonView.RPC("SetHat", RpcTarget.All, true);
    }

    void OnHatOff(InputValue value)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        
        photonView.RPC("SetHat", RpcTarget.All, false);
    }

    [PunRPC]
    private void SetHat(bool isActive)
    {
        hat.SetActive(isActive);
    }
    
    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        if (moveInput.magnitude > 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
        }
    }
}
