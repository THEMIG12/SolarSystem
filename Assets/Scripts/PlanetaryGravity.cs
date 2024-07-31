using Unity.VisualScripting;
using UnityEngine;

public class PlanetaryGravity : MonoBehaviour
{
    public float gravityStrength = 9.8f; // Strength of the gravitational pull


    void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = transform.position - rb.position;
            float distance = direction.magnitude;
            float gravity = gravityStrength / (distance * distance);
            rb.AddForce(direction.normalized * gravity * rb.mass);
        }
    }
}