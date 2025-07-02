using System.Collections.Generic;
using UnityEngine;

public class StickerSpawner : MonoBehaviour
{
    Vector3 placementBaseAngle = new Vector3(0, 0, 0);
    Vector3 placementRange = new Vector3(90, 90, 90);

    public int StickerAmountFactor = 10;
    private int stickersToSpawn;
    List<GameObject> goodStickers = new List<GameObject>();
    List<GameObject> evilStickers = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        stickersToSpawn = Random.Range(1, StickerAmountFactor) * 2 + 1;
        int goodstickers = Random.Range(0, stickersToSpawn);
        SpawnStickers(goodstickers, goodStickers);
        SpawnStickers(stickersToSpawn - goodstickers, evilStickers);
    }

    private void OnDisable()
    {
        goodStickers.Clear();
        evilStickers.Clear();
    }

    private void SpawnStickers(int amount, List<GameObject> stickers)
    {
        Vector3 angle = new Vector3();
        for (int i = 0; i < amount; i++)
        {
            angle.x = Random.Range(placementRange.x, -placementRange.x) + placementBaseAngle.x;
            angle.y = Random.Range(placementRange.y, -placementRange.y) + placementBaseAngle.y;
            angle.z = Random.Range(placementRange.z, -placementRange.z) + placementBaseAngle.z;

        }
    }
}
