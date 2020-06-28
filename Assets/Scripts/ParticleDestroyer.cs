using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfterWhile());
    }


    private IEnumerator DestroyAfterWhile()
    {
        float timeToPop = transform.GetComponent<ParticleSystem>().main.startLifetime.constant;
        yield return new WaitForSeconds(timeToPop);
        Destroy(this.gameObject);
    }
}
