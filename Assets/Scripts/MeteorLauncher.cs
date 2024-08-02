using System.Collections;
using UnityEngine;

public class MeteorLauncher : MonoBehaviour
{
    public GameObject meteorPrefab;  // Reference to the meteor prefab
    public float minSize = 0.5f;     // Minimum size of meteors
    public float maxSize = 5f;     // Maximum size of meteors
    public float minMass = 1f;     // Minimum mass of meteors
    public float maxMass = 40f;    // Maximum mass of meteors
    public float launchForce = 2300f; // Force applied to launch meteors

    void Start()
    {
        StartCoroutine(Launcher());
    }

    IEnumerator Launcher()
    {
        yield return new WaitForSeconds(Random.Range(0, 7));

        yield return StartCoroutine(LaunchMeteor());
    }

    IEnumerator LaunchMeteor()
    {
        // Instantiate a new meteor at the launcher's position
        var position = new Vector3(transform.position.x + Random.Range(-10.0f, 10.0f), transform.position.z, transform.position.z + Random.Range(-10.0f, 10.0f));
        GameObject meteor = Instantiate(meteorPrefab, position, transform.rotation);

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
        yield return StartCoroutine(Launcher());
    }
}
