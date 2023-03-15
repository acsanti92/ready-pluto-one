using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // spawnArea will determine where the planets will spawn
    private Collider spawnArea;

    // planets will be the array of planets that will be spawned
    public GameObject[] planets;

    // minSpawnDelay and maxSpawnDelay will determine the delay between each spawn
    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1.0f;

    // minAngle and maxAngle will determine the angle at which the planets will spawn
    public float minAngle = -15f;
    public float maxAngle = 15f;

    // minForce and maxForce will determine the force at which the planets will spawn
    public float minForce = 18f;
    public float maxForce = 22f;

    // maxLifetime will determine the lifetime of the planets
    public float maxLifetime = 5f;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    // OnEnable will be called when the script is enabled
    private void OnEnable()
    {
        StartCoroutine(SpawnPlanets());
    }

    // OnDisable will be called when the script is disabled
    private void OnDisable()
    {
        StopAllCoroutines();
    }

    // SpawnPlanets will spawn the planets
    private IEnumerator SpawnPlanets()
    {
        // Wait for a second before spawning the first planet
        yield return new WaitForSeconds(2f);

        while (enabled)
        {
            // Spawn a random planet
            GameObject prefabPlanet = planets[Random.Range(0, planets.Length)];

            // Set the planet's position to a random position within the spawn area
            Vector3 spawnPosition = new Vector3();
            spawnPosition.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            spawnPosition.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            spawnPosition.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            // Set the planet's position to the spawn position
            // Quaternion is the rotation of the planet
            Quaternion spawnRotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            // Instantiate the planet
            GameObject planet = Instantiate(prefabPlanet, spawnPosition, spawnRotation);
            // Destroy the planet after a certain amount of time
            Destroy(planet, maxLifetime);
            // Add a random amount of force to the planet
            float force = Random.Range(minForce, maxForce);
            planet.GetComponent<Rigidbody>().AddForce(planet.transform.up * force, ForceMode.Impulse);

            // Wait for a random amount of time
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
