using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class DragAndDrop : MonoBehaviour
{
    #region Publics
    [Header("Pickup settings")]
    public RigidbodyConstraints holdAreaConstraints;

    [Header("Physics Parameters")]
    [SerializeField] public float holdRange = 2.0f;
    [SerializeField] public float pickupRange = 5.0f;
    [SerializeField] public float pickupForce = 1f;
    [SerializeField] public float heldLinearDamping = 10f;

    #endregion


    #region Unity Api

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickupRange))
        {
            holding = true;
            PickupObject(hit.transform.gameObject);
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
        heldObjRB.constraints = RigidbodyConstraints.None;

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
    private bool holding;
    private float tempDamping;
    #endregion


}
