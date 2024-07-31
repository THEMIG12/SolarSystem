using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonSystem : MonoBehaviour
{
    public Transform center;

    public float radius;

    public float radiusSpeed;

    public float rotationSpped;

    private Vector3 axis;

    private Vector3 desiredPosition;

    void Start()
    {
        transform.position = (transform.position - center.position).normalized * radius + center.position;
        axis = Vector3.up;
    }

    void Update()
    {

        transform.RotateAround(center.position, axis, rotationSpped * Time.deltaTime);

        desiredPosition = (transform.position - center.position).normalized * radius + center.position;

        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime);


    }
}
