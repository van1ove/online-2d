using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    private const int Damage = 10;

    [SerializeField] private GameObject bullet;
    private PhotonView _bulletView;
    public float lifetime = 5f;
    private float _timer;
    
    private void Start()
    {
        _bulletView = GetComponent<PhotonView>();
        _timer = 0f;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= lifetime && _bulletView.IsMine)
        {
            PhotonNetwork.Destroy(bullet);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.layer is 6 or 3)
        {
            PhotonNetwork.Destroy(bullet);
            if (other.gameObject.layer == 6) return;
            
            other.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, Damage);
        }
    }
}