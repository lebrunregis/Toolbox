using UnityEngine;
using UnityEngine.UIElements;

public class MouseHandler : MonoBehaviour
{
    [SerializeField] private ScoreHandler scoreHandler;
    [SerializeField] private LayerMask layerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    private void Update()
    {
        //Check for mouse click 
        if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
        {
            Debug.Log("Click");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 9999f, layerMask))
            {
                if (raycastHit.transform != null)
                {
                    raycastHit.transform.TryGetComponent<TargetBehaviour>(out TargetBehaviour targetBehaviour);
                    if (targetBehaviour != null)
                    {
                        Debug.Log("Target hit");
                        targetBehaviour.Hit(raycastHit);
                    }
                    else
                    {
                        Debug.Log("Miss");
                        scoreHandler.EndCombo();
                    }
                }
                else
                {
                    Debug.Log("No hit");
                    scoreHandler.EndCombo();
                }
            }
        }
    }
}
