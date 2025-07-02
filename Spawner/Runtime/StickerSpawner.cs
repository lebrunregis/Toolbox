using LayerChangeCoffin.Runtime;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

[RequireComponent(typeof(LayerChanger))]
public class StickerSpawner : MonoBehaviour
{
    public Vector3 placementBaseAngle = new(0, 0, 0);
    public Vector3 placementRange = new(90, 90, 90);
    public int raycastDistance = 3;
    public Transform raycastSource;

    public int StickerAmountFactor = 5;
    public int stickersToSpawn;
    public List<GameObject> goodStickers = new();
    public List<GameObject> evilStickers = new();
    public List<GameObject> spawnedStickers = new();

    private void OnEnable()
    {
        LayerChanger layerChanger = GetComponent<LayerChanger>();
        layerChanger.enabled = true;
        stickersToSpawn = Random.Range(1, StickerAmountFactor) * 2 + 1;
        int goodStickersAmount = Random.Range(0, stickersToSpawn);
        int badStickersAmount = stickersToSpawn - goodStickersAmount;
        SpawnStickers(goodStickersAmount, goodStickers);
        SpawnStickers(badStickersAmount, evilStickers);

        if (goodStickersAmount > badStickersAmount)
        {
            layerChanger.SetType(LayerChanger.CoffinType.Good);
        }
        else
        {
            layerChanger.SetType(LayerChanger.CoffinType.Evil);
        }

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

            raycastSource.position = transform.position;
            raycastSource.rotation = transform.rotation;
            raycastSource.rotation *= Quaternion.Euler(angle);
            raycastSource.position += raycastSource.transform.forward * raycastDistance;
            Vector3 direction = (transform.position - raycastSource.position).normalized;
            RaycastHit[] hits = Physics.RaycastAll(raycastSource.position, direction, raycastDistance, layerAsLayerMask);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject == transform.gameObject)
                {
                    Debug.Log("Hit found, placing sticker");
                    int stickerIndex = Random.Range(0, stickers.Count - 1);
                    GameObject instance = Instantiate(stickers[stickerIndex]);
                    instance.transform.SetParent(transform);
                    instance.transform.position = hit.point;
                    instance.transform.rotation = Quaternion.Euler(hit.normal);
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
