using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    private const int MaxHealth = 100;
    private PhotonView _photonView;
    private Image _healthBar;
    private int _health;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _health = MaxHealth;
        _healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        if (!_photonView.IsMine) 
            return;
        
        _health -= damage;
        _healthBar.fillAmount = (float)_health / MaxHealth;
        if (_health <= 0)
        {
            _photonView.gameObject.GetComponent<PlayerController>().Die();
        }
    }
}
