using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class GameObjectPool : MonoBehaviour
    {
        public GameObject prefab;
        public int poolIncrement = 20;
        public int maxSize = 100;
        public LinkedList<GameObject> list;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            list = new LinkedList<GameObject>();
            Allocate(poolIncrement);
        }

        private void Allocate(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject go = Instantiate(prefab);
                go.SetActive(false);
                list.AddLast(go);
            }
        }

        // Update is called once per frame
        private void Update()
        {

        }

        public GameObject GetFirstAvailableObject()
        {
            GameObject go = null;
            LinkedListNode<GameObject> node = list.First;
            do
            {
                if (!node.Value.activeSelf)
                {
                    go = node.Value;
                }
                else
                {
                    node = node.Next;
                }
            } while (go == null && node != null);

            if (go == null && list.Count < maxSize)
            {
                Allocate(poolIncrement);
                go = list.Last.Value;
            }
            return go;
        }
    }
}
