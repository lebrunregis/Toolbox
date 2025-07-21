using System.Collections.Generic;
using UnityEngine;

public class StickerSpawner : MonoBehaviour
{
    public Vector3 placementBaseAngle = new(180, 0, 0);
    public Vector3 placementRange = new(45, 45, 0);
    public int raycastDistance = 3;
    public Transform raycastSource;

    public int StickerAmountFactor = 5;
    public int stickersToSpawn;
    public List<GameObject> stickerPrefabs = new();
    public List<GameObject> spawnedStickers = new();

    private void OnEnable()
    {
        int stickerAmount = Random.Range(1, StickerAmountFactor) * 2 + 1;
        SpawnStickers(stickerAmount, stickerPrefabs);
    }

    private void OnDisable()
    {

        ClearStickers(spawnedStickers);
    }

    private void ClearStickers(List<GameObject> objects)
    {
        for (int i = 0; i > objects.Count; i++)
        {
            objects[i].transform.parent = null;
        }

        objects.Clear();
    }

    private void SpawnStickers(int amount, List<GameObject> stickers)
    {
        Vector3 angle = new();
        LayerMask layerAsLayerMask = (1 << transform.gameObject.layer);
        for (int i = 0; i < amount; i++)
        {
            angle.x = Random.Range(placementRange.x, -placementRange.x) + placementBaseAngle.x;
            angle.y = Random.Range(placementRange.y, -placementRange.y) + placementBaseAngle.y;
            angle.z = Random.Range(placementRange.z, -placementRange.z) + placementBaseAngle.z;

            raycastSource.SetPositionAndRotation(transform.position, transform.rotation);
            raycastSource.rotation *= Quaternion.Euler(angle);
            raycastSource.position += raycastSource.transform.forward * raycastDistance;
            Vector3 direction = (transform.position - raycastSource.position).normalized;
            RaycastHit[] hits = Physics.RaycastAll(raycastSource.position,
                                                   direction,
                                                   raycastDistance,
                                                   layerAsLayerMask);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject == transform.gameObject)
                {
                    UnityEngine.Debug.Log("Hit found, placing sticker");
                    int stickerIndex = Random.Range(0, stickers.Count - 1);
                    GameObject instance = Instantiate(stickers[stickerIndex]);
                    instance.transform.SetParent(transform);
                    instance.transform.position = hit.point;
                    instance.transform.rotation = Quaternion.LookRotation(hit.normal);
                    instance.SetActive(true);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Set the color with custom alpha.
        Gizmos.color = new Color(0f, 1f, 0f, 1f); // Green with custom alpha
        Vector3 direction = (transform.position - raycastSource.position).normalized;
        // Draw the ray.
        Gizmos.DrawRay(raycastSource.position, direction * raycastDistance);
    }
}
