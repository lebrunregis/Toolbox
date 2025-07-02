using ConveyorBeltScript.Runtime;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class DragAndDrop : MonoBehaviour
{
    #region Publics
    [Header("Pickup settings")]
    public RigidbodyConstraints holdAreaConstraints;
    public RigidbodyConstraints releaseAreaConstraints;

    [Header("Physics Parameters")]
    [SerializeField] public float holdRange = 2.0f;
    [SerializeField] public float pickupRange = 5.0f;
    [SerializeField] public float pickupForce = 1f;
    [SerializeField] public float heldLinearDamping = 10f;

    #endregion


    #region Unity Api

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (holding)
        {
            MoveObject();
        }
    }

    #endregion


    #region Main Methods
    public void OnGrab(CallbackContext context)
    {
        Debug.Log("Grabbed object");
        if (context.performed == true)
        {
            OnGrabButtonPressed();
        }
        else if (context.canceled == true)
        {
            OnGrabButtonReleased();
        }
    }
    private void OnGrabButtonPressed()
    {
        Debug.Log("Trying to grab something");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            if (hit.rigidbody.gameObject.TryGetComponent<Grabable>(out Grabable grab))
            {
                heldObjGrabableComponent = grab;
                grab.grabbed = true;
                holding = true;
                PickupObject(hit.transform.gameObject);
            }
            else
            {
                Debug.LogWarning("Tried to grab an object but it did not contain a grabable component!");
            }
        }
    }

    private void OnGrabButtonReleased()
    {
        if (holding)
        {
            holding = false;
            DropObject();
        }
    }
    #endregion


    #region Utils
    private void PickupObject(GameObject pickedObject)
    {
        Rigidbody rb = pickedObject.GetComponentInChildren<Rigidbody>();
        if (rb != null)
        {
            heldObj = pickedObject;
            heldObjRB = rb;
            rb.useGravity = false;
            tempDamping = heldObjRB.linearDamping;
            heldObjRB.linearDamping = heldLinearDamping;
            heldObjRB.constraints = holdAreaConstraints;
        }
    }

    private void DropObject()
    {
        heldObjRB.useGravity = true;
        heldObjRB.linearDamping = tempDamping;
        heldObjRB.constraints = releaseAreaConstraints;
        heldObjGrabableComponent.grabbed = false;

        heldObjGrabableComponent = null;
        heldObjRB.transform.parent = null;
        heldObj = null;
    }

    private void MoveObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 holdPoint = ray.GetPoint(holdRange);

        if (Vector3.Distance(heldObj.transform.position, holdPoint) > 0.1f)
        {
            Vector3 moveDirection = (holdPoint - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }
    #endregion


    #region Private and Protected
    private GameObject heldObj;
    private Rigidbody heldObjRB;
    private Grabable heldObjGrabableComponent;
    private bool holding;
    private float tempDamping;
    #endregion


}
