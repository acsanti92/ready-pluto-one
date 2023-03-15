using System.Collections;
using UnityEngine;

public class Comet : MonoBehaviour
{
    // Access to the main camera
    private Camera mainCamera;
    private Collider cometCollider;
    private bool slicing;

    private void Awake()
    {
        cometCollider = GetComponent<Collider>();
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
        slicing = true;
        cometCollider.enabled = true;
    }

    private void StopSlicing()
    {
        slicing = false;
        cometCollider.enabled = false;
    }

    private void ContinueSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
    }
}
