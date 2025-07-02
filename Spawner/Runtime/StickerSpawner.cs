using LayerChangeCoffin.Runtime;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LayerChanger))]
public class StickerSpawner : MonoBehaviour
{
    Vector3 placementBaseAngle = new(0, 0, 0);
    Vector3 placementRange = new(90, 90, 90);
    public int raycastDistance = 3;

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
            Vector3 fromPosition = transform.position + angle * raycastDistance;
            Vector3 toPosition = transform.position;
            Vector3 direction = toPosition - fromPosition;
            if (Physics.Raycast(toPosition, direction, out RaycastHit hit, raycastDistance, layerAsLayerMask))
            {
                int stickerIndex = Random.Range(0, stickers.Count - 1);
                GameObject instance = Instantiate(stickers[stickerIndex]);
                instance.transform.SetParent(transform);
                instance.transform.position = hit.point;
                instance.transform.rotation = Quaternion.Euler(hit.normal);
                instance.SetActive(true);
            }
            else
            {
                Debug.Log("Sticker raycast didn't hit!");
            }
        }
    }
}
