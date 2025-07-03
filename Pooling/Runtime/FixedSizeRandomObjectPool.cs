using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tools
{
    public class FixedSizeRandomObjectPool : MonoBehaviour, IPool
    {
        public List<GameObject> prefabs;

        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++) {
                        prefabs.Add(transform.GetChild(i).gameObject);
            }
        }

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
                foundObject = prefabs[availableObjects.ElementAt(pos)];
            }

            // Debug.Log("Available objects :" + availableObjects.Count );
            return foundObject;
        }
    }
}
