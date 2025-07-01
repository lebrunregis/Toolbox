using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class FixedSizeRandomObjectPool : MonoBehaviour, IPool
    {
        public List<GameObject> prefabs;

        public GameObject GetGameObject()
        {
            LinkedList<int> availableObjects = new();
            GameObject foundObject = null;
            for (int i = 0; i < prefabs.Count; i++)
            {
                //Debug.Log("Testing " + prefabs[i].name);
                if (!prefabs[i].activeSelf)
                {
                    //Debug.Log("Adding gameobject to list");
                    availableObjects.AddLast(i);
                }
            }

            if (availableObjects.Count > 0)
            {
                int pos = Random.Range(0, availableObjects.Count);
                foundObject = prefabs[pos];
            }

            // Debug.Log("Available objects :" + availableObjects.Count );
            return foundObject;
        }
    }
}
