using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Fire : MonoBehaviour
{
    [SerializeField] private float delay = 0.5f, bulletForce = 10f;
    
    private Transform _positionToSpawn;
    private float _timer;
    private bool _isDown;
    void Start()
    {
        _isDown = false;
        _timer = 0f;
        _positionToSpawn = FindObjectOfType<PlayerController>().BulletSpawn;
    }

    void Update()
    {
        _timer += Time.deltaTime;
    }

    public void Shoot()
    {
        if(_isDown && _timer > delay)
        {
            GameObject bullet = PhotonNetwork.Instantiate("Bullet", _positionToSpawn.position, _positionToSpawn.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(_positionToSpawn.up * bulletForce, ForceMode2D.Impulse);
            _timer = 0f;
        }
    }

    public void FireButtonDown()
    {
        _isDown = true;
    }
    public void FireButtonUp()
    {
        _isDown = false;
    }
}