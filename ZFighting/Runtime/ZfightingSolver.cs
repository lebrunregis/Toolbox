using UnityEngine;

public class ZfightingSolver : MonoBehaviour
{
    #region Publics

    #endregion


    #region Unity Api

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

        Vector3 pos = transform.position;
        pos.z += Random.Range(0.0f, 0.02f);
        transform.position = pos;
    }

    // Update is called once per frame
    private void Update()
    {

    }

    #endregion


    #region Main Methods

    #endregion


    #region Utils

    #endregion


    #region Private and Protected

    #endregion


}
