/*
 * Miroslav Ivan 2020 pre bakalarsku pracu
 * Tento skript je v prvej hre na kamere
 * Pomocou tohoto skriptu kamera nasleduje hraca
*/
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player = null;                   // Referencia na hraca nastavena cez editor
    public bool isFollowOn;                            // ma nasledovat hraca ?
    private Vector3 offset;                            // odstup od hraca


    void Start()
    {
        // Zisti aky mas mat odstup podla pozicii zo sceny
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // Ak je hrac nastaveny a mam nasledovat hraca
        if (isFollowOn & player != null)
        {
            // aktualizuj poziciu kamery
            transform.position = player.transform.position + offset;
        }
    }
}

