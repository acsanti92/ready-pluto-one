using System.Collections;
using System.Collections.Generic;
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
    public float minAngle;
    public float maxAngle;

    // minForce and maxForce will determine the force at which the planets will spawn
    public float minForce;
    public float maxForce;

    // minLifetime and maxLifetime will determine the lifetime of the planets
    public float maxLifetime = 5f;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    // 
    private void onEnable()
    {
        StartCoroutine(SpawnPlanets());
    }

    //
    private void onDisable()
    {
        StopAllCoroutines();
    }

    //
    private IEnumerator SpawnPlanets()
    {
        // Wait for a second before spawning the first planet
        yield return new WaitForSeconds(1.0f);

        while (enabled)
        {
            // Spawn a random planet
            GameObject planet = Instantiate(planets[Random.Range(0, planets.Length)]);

            // Set the planet's position to a random position within the spawn area
            Vector3 spawnPosition = new Vector3();
            spawnPosition.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            spawnPosition.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            spawnPosition.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            // Wait for a random amount of time
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
