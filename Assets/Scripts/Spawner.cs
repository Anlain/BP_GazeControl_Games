using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject spawnObject = null;
    [SerializeField] Vector3 spawnField = Vector3.forward;
    [SerializeField] float spawnPeriod = 0.2f;
    [SerializeField] Color[] colors;

    private bool isGameOver = false;

    private Vector2 lastSpawn;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawn = new Vector2(0, 0);

        if (spawnObject == null)
        {
            Debug.Log("Spawner: I dont know what to spawn !");
        }

        StartCoroutine(spawnCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnCoroutine()
    {
        while (!isGameOver)
        {
            spawnBalloon();
            yield return new WaitForSeconds(Random.Range(0.5f, 0.5f + spawnPeriod));
        }
    }

    private void spawnBalloon()
    {
        Vector2 randPos;
        while (true)
        {
            // calculate random spawn position in 2D based on spawnField size
             randPos = new Vector2(Random.Range(-spawnField.x, spawnField.x), Random.Range(-spawnField.y, spawnField.y));
            if (randPos.x - 1 < lastSpawn.x && lastSpawn.x < randPos.x + 1) { continue; }
            else
            {
                lastSpawn = randPos;
                break;
            }
        }

        // create a new balloon
        GameObject newBalloon = Instantiate(spawnObject, randPos, Quaternion.identity, this.transform);
        // get a hold of its MeshRenderer
        MeshRenderer mr = newBalloon.GetComponent<MeshRenderer>();
        int index = (int) Mathf.Round(Random.Range(0.0f, 1.0f));
        if (index == 1)
        {
            newBalloon.GetComponent<BalloonController>().setIsBlue(true);
        }
        // assign random color to its material through meshRenderer
        mr.material.color = colors[index];
        // assign its emissive color through DynamicGI
        DynamicGI.SetEmissive(mr, colors[index]);
    }


}
