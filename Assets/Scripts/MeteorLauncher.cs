using System.Collections;
using UnityEngine;

public class MeteorLauncher : MonoBehaviour
{
    public GameObject meteorPrefab;   // Reference to the meteor prefab
    public float minSize = 0.5f;      // Minimum size of meteors
    public float maxSize = 5f;        // Maximum size of meteors
    public float minMass = 1f;        // Minimum mass of meteors
    public float maxMass = 40f;       // Maximum mass of meteors
    public float launchForce = 2300f; // Force applied to launch meteors
    public float launchDelayMin = 1f; // Minimum delay between launches
    public float launchDelayMax = 7f; // Maximum delay between launches

    public float planetGravityMultiplier = 2f; // Multiplier for gravity when meteor enters a planet's atmosphere

    void Start()
    {
        StartCoroutine(Launcher());
    }

    IEnumerator Launcher()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(launchDelayMin, launchDelayMax));
            LaunchMeteor();
        }
    }

    void LaunchMeteor()
    {
        // Instantiate a new meteor at the launcher's position
        Vector3 position = new Vector3(transform.position.x + Random.Range(-10.0f, 10.0f), transform.position.y, transform.position.z + Random.Range(-10.0f, 10.0f));
        GameObject meteor = Instantiate(meteorPrefab, position, Quaternion.identity);

        // Randomize the size of the meteor
        float size = Random.Range(minSize, maxSize);
        meteor.transform.localScale = new Vector3(size, size, size);

        // Set the mass of the meteor based on its size
        float mass = Mathf.Lerp(minMass, maxMass, (size - minSize) / (maxSize - minSize));
        Rigidbody rb = meteor.GetComponent<Rigidbody>();
        rb.mass = mass;

        // Randomize launch direction
        Vector3 launchDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        rb.AddForce(launchDirection * launchForce);

        // Add PlanetGravity script to handle gravitational interactions
        meteor.AddComponent<PlanetGravity>().gravityMultiplier = planetGravityMultiplier;
    }
}

public class PlanetGravity : MonoBehaviour
{
    public float gravityMultiplier = 2f; // Gravity multiplier for planet's atmosphere
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Solor"))
        {
            // Adjust gravity when entering a planet's atmosphere
            rb.useGravity = true;
            rb.mass *= gravityMultiplier;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Solor"))
        {
            // Reset gravity when leaving the planet's atmosphere
            rb.useGravity = false;
            rb.mass /= gravityMultiplier;
        }
    }
}
