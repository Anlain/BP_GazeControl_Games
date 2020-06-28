using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private TileManager tileManager;
    [SerializeField]
    private float delayForRecycle = 3.0f;

    private void OnEnable()
    {
        tileManager = GameObject.FindObjectOfType<TileManager>();

    }

    // This method is being called when not seen by camera
    private IEnumerator OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit called");
        if (other.tag == "Player")
        {
            Debug.Log("Player left a tile, calling delayed recycle tile");
            yield return new WaitForSeconds(delayForRecycle);
            tileManager.recycleTile(transform.parent.gameObject);
            other.gameObject.GetComponent<Player>().addScore();
        }
    }
}
