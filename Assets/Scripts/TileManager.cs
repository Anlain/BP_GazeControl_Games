using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilePrefabs = null;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private int tileLength = 40;

    [SerializeField]
    private int tilesAmount = 5;

    private int offsetCounter;
    private int lastIndex;
    // Start is called before the first frame update
    void Start()
    {
        lastIndex = 0;
        Debug.Log(tilePrefabs.Length);

        for (int i = 1; i <= tilesAmount; i++)                               // Spawn tilesAmount of tiles
        {
            int index = getRandomIndex();
            //Debug.Log(i + ": " + index);
            while (index == lastIndex) { index = getRandomIndex(); }        // Calculate which tile youre going to spawn different to last spawned tile
            int reverse = getRandomReverse();                               // Calculate if the tile is going to be reversed
            offsetCounter += tileLength;                                    // update offset
            spawnTile(index, offsetCounter, reverse);
            lastIndex = index;
        }
        offsetCounter += tileLength;                                        // update offset
    }

    private void spawnTile(int index, int offset, int reverse)
    {
        if (reverse == 0) { Instantiate(tilePrefabs[index], new Vector3(0, 0, offset), Quaternion.identity, gameObject.transform); }
        else
        {
            //Debug.Log("Reverse tile");
            GameObject tile = Instantiate(tilePrefabs[index], new Vector3(0, 0, offset), Quaternion.Euler(0, 180, 0), gameObject.transform);
            GameObject exitTrigger = tile.transform.Find("ExitTrigger").gameObject;
            exitTrigger.transform.position += new Vector3(0.0f, 0.0f, tileLength); 
        }
    }

    public void recycleTile(GameObject tile)
    {
        Destroy(tile);
        int index = getRandomIndex();
        while (index == lastIndex) { index = getRandomIndex(); }                                // Calculate which tile youre going to spawn different to last spawned tile
        int reverse = getRandomReverse();
        spawnTile(index, offsetCounter, reverse);
        offsetCounter += tileLength;
        lastIndex = index;
    }

    private int getRandomIndex()
    {
        return Mathf.RoundToInt(Random.Range(0, tilePrefabs.Length));
    }

    private int getRandomReverse()
    {
        return Mathf.RoundToInt(Random.Range(0.0f, 1.0f));
    }

}
