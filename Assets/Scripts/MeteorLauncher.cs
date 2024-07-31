using UnityEngine;

public class MeteorLauncher : MonoBehaviour
{
    public GameObject meteorPrefab;  // Reference to the meteor prefab
    public float minSize = 0.5f;     // Minimum size of meteors
    public float maxSize = 2.0f;     // Maximum size of meteors
    public float minMass = 1.0f;     // Minimum mass of meteors
    public float maxMass = 10.0f;    // Maximum mass of meteors
    public float launchForce = 1000f; // Force applied to launch meteors

    void Update()
    {
        // Check for input to launch a meteor
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchMeteor();
        }
    }

    void LaunchMeteor()
    {
        // Instantiate a new meteor at the launcher's position
        GameObject meteor = Instantiate(meteorPrefab, transform.position, transform.rotation);

        // Randomize the size of the meteor
        float size = Random.Range(minSize, maxSize);
        meteor.transform.localScale = new Vector3(size, size, size);

        // Set the mass of the meteor based on its size
        float mass = Mathf.Lerp(minMass, maxMass, (size - minSize) / (maxSize - minSize));
        Rigidbody rb = meteor.GetComponent<Rigidbody>();
        rb.mass = mass;

        // Apply a force to launch the meteor
        Vector3 launchDirection = transform.forward;  // Adjust as needed for desired launch direction
        rb.AddForce(launchDirection * launchForce);
    }
}
