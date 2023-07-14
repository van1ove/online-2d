using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.GetComponent<PhotonView>().RPC("TakeCoin", RpcTarget.All);
        PhotonNetwork.Destroy(gameObject);
    }
}
