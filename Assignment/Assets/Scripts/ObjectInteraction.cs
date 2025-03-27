using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public float pickupRange = 3f;
    public LayerMask pickupLayer;

    private GameObject currentObject = null;
    private Rigidbody currentObjectRigidbody;
    private Camera mainCamera;

    public Transform vehicleDropZone;
    public float placementTolerance = 0.5f;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryPickupObject();
        }

        if (Input.GetMouseButtonUp(0) && currentObject != null)
        {
            TryPlaceObject();
        }

        if (currentObject != null)
        {
            HandleObjectRotation();
        }
    }

    void TryPickupObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
        {
            currentObject = hit.collider.gameObject;
            currentObjectRigidbody = currentObject.GetComponent<Rigidbody>();

            currentObjectRigidbody.isKinematic = true;
        }
    }

    void TryPlaceObject()
    {
        if (currentObject == null) return;

        currentObjectRigidbody.isKinematic = false;

        bool correctPlacement = ValidatePlacement();

        Renderer objectRenderer = currentObject.GetComponent<Renderer>();
        objectRenderer.material.color = correctPlacement ? Color.green : Color.red;

        currentObject = null;
        currentObjectRigidbody = null;
    }

    void HandleObjectRotation()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentObject.transform.Rotate(Vector3.up, -90f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentObject.transform.Rotate(Vector3.up, 90f);
        }
    }

    bool ValidatePlacement()
    {
        if (vehicleDropZone == null) return false;

        float distance = Vector3.Distance(
            currentObject.transform.position,
            vehicleDropZone.position
        );

        return distance <= placementTolerance;
    }
}