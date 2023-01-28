using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;

    private float _speed;

    void Start()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed) * (Random.value > .5f? -1 : 1) / transform.position.magnitude;
    }

    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, _speed * Time.deltaTime);
    }
}
