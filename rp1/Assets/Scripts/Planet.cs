using System.Collections;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // Whole and sliced will be the whole and sliced versions of the planet
    public GameObject whole;
    public GameObject sliced;
    // To access RigidBody component
    private Rigidbody planetRigidbody;
    // To access Collider component
    private Collider planetCollider;
    // To access ParticleSystem component
    private ParticleSystem rocksParticleEffect;
    // Default value of the planet
    public int points = 1;

    // Get the planet's rigidbody and collider when the script is enabled
    private void Awake()
    {
        planetRigidbody = GetComponent<Rigidbody>();
        planetCollider = GetComponent<Collider>();
        rocksParticleEffect = GetComponentInChildren<ParticleSystem>();
    }

    // whole planet becomes inactive and sliced planet becomes active
    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        // Increase the score
        FindObjectOfType<GameManager>().IncreaseScore(points);

        planetCollider.enabled = false;
        // Play the particle effect
        rocksParticleEffect.Play();
        // Disable the whole planet
        whole.SetActive(false);
        // Enable the sliced planet
        sliced.SetActive(true);
        // Equation for the angle of the planet
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Rotate the sliced planet, and set the position of the sliced planet
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        // Array of the sliced planet's rigidbodies
        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        // Loop through the slices
        foreach (Rigidbody slice in slices)
        {
            // Set the velocity of the slice to the velocity of the planet
            slice.velocity = planetRigidbody.velocity;
            // Add a force to the slice
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }

    }

    // OnTriggerEnter will be called when the planet collides with another object
    private void OnTriggerEnter(Collider other)
    {
        // compare the tag of the other object
        if (other.CompareTag("Player"))
        {
            // Get the comet's rigidbody
            Comet comet = other.GetComponent<Comet>();
            Slice(comet.direction, comet.transform.position, comet.sliceForce);
        }
    }
}
