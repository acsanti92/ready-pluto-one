using System.Collections;
using UnityEngine;

public class Comet : MonoBehaviour
{
    // Access to the main camera
    private Camera mainCamera;
    // The comet's rigidbody
    private Collider cometCollider;
    // cometTrail will be the comet's trail renderer
    private TrailRenderer cometTrail;
    // slicing will determine if the comet is being sliced
    private bool slicing;

    // get and set the direction of the comet
    public Vector3 direction { get; private set; }
    //
    public float minSliceVelocity = 0.01f;

    private void Awake()
    {
        mainCamera = Camera.main;
        cometCollider = GetComponent<Collider>();
        cometTrail = GetComponentInChildren<TrailRenderer>();
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
        else if (Input.GetMouseButton(0))
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        // When we start slicing, we need to get the position of the mouse in world space
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        // Set the comet's position to the new position
        transform.position = newPosition;

        slicing = true;
        cometCollider.enabled = true;
        cometTrail.enabled = true;

        // clear all points in the comet's trail
        cometTrail.Clear();
    }

    private void StopSlicing()
    {
        slicing = false;
        cometCollider.enabled = false;
        cometTrail.enabled = false;
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
        cometCollider.enabled = velocity > minSliceVelocity;

        // Set the comet's position to the new position
        transform.position = newPosition;
    }
}
