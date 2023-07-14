using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private float minX, minY, maxX, maxY;
    private void Start()
    {
        Vector2 pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(player.name, pos, Quaternion.identity);   
    }
}
