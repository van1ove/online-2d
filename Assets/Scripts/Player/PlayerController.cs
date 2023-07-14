using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class PlayerController : MonoBehaviourPun
{
    [SerializeField] private Transform bulletSpawn;
    public Transform BulletSpawn
    {
        get { return bulletSpawn; }
    }
    
    [Header("UI")]
    [SerializeField] private GameObject canvas;
    [SerializeField] private TextMeshProUGUI playerName;
    
    [Header("Movement")]
    private Joystick joystick;
    [SerializeField] private float moveSpeed = 5f;

    private Collider2D _collider;
    private Rigidbody2D _rb;
    private Animator _animator;
    private bool _isRunning, _isDead;
    private PhotonView _view;
    private float _x, _y, _z;

    private Fire _fire;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
        playerName.text = _view.Owner.NickName;
        if (_view.Owner.IsLocal) Camera.main.GetComponent<CameraFollow>().playerTransform = gameObject.transform;
    }

    private void Start()
    {
        _fire = FindObjectOfType<Fire>();

        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.freezeRotation = true;
        
        _animator = GetComponent<Animator>();
        _isRunning = false;
        _isDead = false;
        
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
    }

    private void Update()
    {
        canvas.transform.eulerAngles = new Vector3(0f, 0f, -transform.rotation.z);
        if (_view.IsMine && !_isDead)
        {
            _fire.Shoot();
        }
    }

    private void FixedUpdate()
    {
        _x = joystick.Horizontal;
        _y = joystick.Vertical;
        
        if (_view.IsMine && !_isDead)
        {
            _rb.velocity = new Vector2(_x * moveSpeed, _y * moveSpeed);

            _isRunning = (_rb.velocity != Vector2.zero); 
            _animator.SetBool("IsRunning", _isRunning);
            
            if (_x != 0 || _y != 0)
            {
                _z = Mathf.Atan2(_x, _y) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0f, 0f, -_z);
            }
        }
    }
    
    public void Die()
    {
        _isDead = true;
        _collider.enabled = false;
        _rb.isKinematic = true;
        _animator.SetTrigger("Died");
        
        GameManager.Instance.DestroyPlayer(gameObject);
        SpawnCoins.StopSpawning();
        GameManager.Instance.TurnGameOverPanel();
    }
}
