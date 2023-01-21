using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float _speed;

    void Start()
    {
        _speed = Random.value * 100 - 50;
    }

    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, _speed * Time.deltaTime);
    }
}
