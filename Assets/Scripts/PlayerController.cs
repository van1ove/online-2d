using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using UnityEngine.UI;

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

    private Rigidbody2D _rb;
    //private Animator _animator;
    
    private PhotonView _view;
    private float _x, _y, _z;
    private bool _isRunning = false;
    
    private Image _healthBar;
    private readonly float maxHealth = 100f;
    private float _health = 100f;
    
    private Fire fire;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
        playerName.text = _view.Owner.NickName;
        if (_view.Owner.IsLocal) Camera.main.GetComponent<CameraFollow>().playerTransform = gameObject.transform;
    }

    private void Start()
    {
        fire = FindObjectOfType<Fire>();

        _rb = GetComponent<Rigidbody2D>();
        _rb.freezeRotation = true;
        
        //_animator = GetComponent<Animator>();
        
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        _healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
    }

    private void Update()
    {
        canvas.transform.eulerAngles = new Vector3(0f, 0f, -transform.rotation.z);
        if (_view.IsMine)
        {
            fire.Shoot();
        }
    }

    private void FixedUpdate()
    {
        _x = joystick.Horizontal;
        _y = joystick.Vertical;
        
        if (_view.IsMine)
        {
            _rb.velocity = new Vector2(_x * moveSpeed, _y * moveSpeed);

            _isRunning = (_rb.velocity != Vector2.zero); 
            //_animator.SetBool("IsRunning", _isRunning);
            
            if (_x != 0 || _y != 0)
            {
                _z = Mathf.Atan2(_x, _y) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0f, 0f, -_z);
            }
        }
    }
    private void GetDamage()
    {
        _health -= 10f;
        _healthBar.fillAmount = _health / maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Bullet bullet = col.gameObject.GetComponent<Bullet>();
        if (bullet == null) return;
        
        if (_view.IsMine)
        {
            Debug.Log("damaged");
            GetDamage();
        }
    }
}
