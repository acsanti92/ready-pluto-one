using System.Collections;
using UnityEngine;

public class Comet : MonoBehaviour
{
    // get and set the direction of the comet
    public Vector3 direction { get; private set; }
    // Access to the main camera
    private Camera mainCamera;
    // cometCollider will be the comet's collider
    private Collider sliceCollider;
    // cometTrail will be the comet's trail
    private TrailRenderer sliceTrail;
    // sliceForce will determine the force at which the comet will slice
    public float sliceForce = 5f;
    // minSliceVelocity will determine the minimum velocity at which the comet will slice
    public float minSliceVelocity = 0.01f;
    // slicing will determine if the comet is being sliced
    private bool slicing;

    private void Awake()
    {
        mainCamera = Camera.main;
        sliceCollider = GetComponent<Collider>();
        sliceTrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    // The comet's rigidbody
    private void Update()
    {
        // If the left mouse button is pressed, start slicing
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (slicing)
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        // When we start slicing, we need to get the position of the mouse in world space
        Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;

        // Set the slice's position to the new position
        transform.position = position;

        slicing = true;
        sliceCollider.enabled = true;
        sliceTrail.enabled = true;

        // clear all points in the slice's trail
        sliceTrail.Clear();
    }

    private void StopSlicing()
    {
        slicing = false;
        sliceCollider.enabled = false;
        sliceTrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        // direction will be the difference between the new position and the current position
        direction = newPosition - transform.position;

        // velocity will be the magnitude of the direction divided by the time it took to get there
        float velocity = direction.magnitude / Time.deltaTime;

        // If the velocity is greater than minSliceVelocity, enable the comet's collider
        sliceCollider.enabled = velocity > minSliceVelocity;

        // Set the comet's position to the new position
        transform.position = newPosition;
    }
}
