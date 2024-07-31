using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    public float G = 100f;
    public GameObject[] solar;

    void Awake()
    {
        solar = GameObject.FindGameObjectsWithTag("Solar");
    }

    void Start()
    {
        InitialVelocity();
    }

    void FixedUpdate()
    {
        Gravity();
    }

    void Gravity()
    {
        foreach (GameObject other in solar)
        {
            if (!transform.Equals(other) && Vector3.Distance(transform.position, other.transform.position) != 0)
            {
                float m1 = GetComponent<Rigidbody>().mass;
                float m2 = other.GetComponent<Rigidbody>().mass;
                float r = Vector3.Distance(transform.position, other.transform.position);

                transform.GetComponent<Rigidbody>().AddForce((other.transform.position - transform.position).normalized *
                (G * (m1 * m2) / (r * r)));
            }
        }
    }

    void InitialVelocity()
    {
        foreach (GameObject other in solar)
        {
            if (!transform.Equals(other) && Vector3.Distance(transform.position, other.transform.position) != 0)
            {
                float m2 = other.GetComponent<Rigidbody>().mass;
                float r = Vector3.Distance(transform.position, other.transform.position);
                transform.LookAt(other.transform);

                transform.GetComponent<Rigidbody>().velocity += transform.right * Mathf.Sqrt((G * m2) / r);
            }
        }
    }
}
