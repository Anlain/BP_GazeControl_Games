/*
 * Miroslav Ivan 2020 pre bakalarsku pracu
 * Tento skript je na kazdom baloniku ako komponent
 * Ovlada vsetko spravanie balonika:
 *   - Vybuch po case
 *   - Prasknutie ak je zvonka zavolana funkcia triggerExplosion()
 *   - Pohyb balonika smerom nahor
 *   - Spustenie zvukoveho efektu
*/ 

using System.Collections;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    GameObject player;
    GameObject audioPlayer;
    private bool isBlue = false;                                                // Drzi informaciu o farbe balonika
    float timeToPop = 22.0f;                                                    // Cas za ktory ma sam prasknut

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(DestroyAfterWhile());                                    // Balonik ma po urcitom case prasknut
        player = GameObject.Find("Player");                                     // Ziskaj referenciu na hraca
        audioPlayer = GameObject.Find("AudioPlayer");                           // Referencia na prehravac zvuku
    }

    private void Update()
    {
        transform.position += new Vector3 (0.0f, Time.deltaTime);               // Kazdnu snimku posuvaj balonik nahor
    }
        
    private IEnumerator DestroyAfterWhile()
    {
            yield return new WaitForSeconds(timeToPop);                        // Po stanovenom case pokracuj vo funkcii
            Destroy(this.gameObject);                                          // Znic sam seba
    }

    public void triggerExplosion()
    {
        player.GetComponent<PlayerGaze>().resolveColor(isBlue);               // Zavolaj hracovu metodu na pridelenie bodov / stratu zivota podla farby balonika
        audioPlayer.GetComponent<AudioSource>().Play();                       // Prehraj zvukovy efekt
        Destroy(gameObject);                                                  // Znic sam seba
    }

    public bool getIsBlue()                                                   // Getter premennej isBlue
    {
        return isBlue;
    }

    public void setIsBlue(bool value)                                         // Setter premennej isBlue
    {
        isBlue = value;
    }
}


