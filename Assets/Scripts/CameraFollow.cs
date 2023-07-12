using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 _playerVector;
    [SerializeField] private float speed;

    private void FixedUpdate()
    {
        _playerVector = playerTransform.position;
        _playerVector.z = -10;
        transform.position = Vector3.Lerp(transform.position, _playerVector, speed * Time.deltaTime);
    }
}
