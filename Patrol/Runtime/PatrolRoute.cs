using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PatrolRoute : MonoBehaviour
{
    #region Publics
    public List<Transform> navPoints = new();
    public float idleTime;
    #endregion


    #region Unity Api

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        for (int i = 0; i < navPoints.Count; i++)
        {
            navPoints[i].SetParent(null);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (navPoints.Count > 0)
        {
            if (_agent.remainingDistance < 0.5f)
            {
                _idleDelta -= Time.deltaTime;
                if (_idleDelta < 0)
                {
                    _idleDelta = idleTime;
                    GotoNextPoint();
                }
            }
        }
    }

    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _idleDelta = idleTime;
        _agent.autoBraking = false;

        GotoNextPoint();
    }


    private void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (navPoints.Count == 0)
            return;

        // Set the agent to go to the currently selected destination.
        _agent.destination = navPoints[_destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        _destPoint = (_destPoint + 1) % navPoints.Count;
    }

    #endregion


    #region Main Methods

    #endregion


    #region Utils

    #endregion


    #region Private and Protected
    private NavMeshAgent _agent;
    private float _idleDelta;
    private int _destPoint;
    #endregion


}
