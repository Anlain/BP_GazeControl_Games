/*
 * Miroslav Ivan 2020 pre bakalarsku pracu
 * Tento skript je na kazdom tlacitku, ktore ma spustat hru
 * V editore mu bolo nastavene aku hru ma spustat
 * Sfunkcni komunikacicu so scriptom SceneLoader
*/
using UnityEngine;
using UnityEngine.UI;

public class ButtonsSetup : MonoBehaviour
{
    [SerializeField] int gameNumber = 0;                     // Cislo hry ktoru bude spustat
    private void Awake()
    {
        // Referencia na game objekt so skriptom SceneLoader
        SceneLoader scloader = GameObject.Find("LevelManager").GetComponent<SceneLoader>();
        // Pridaj "listenera", a pri kliknuti na tlacitko zavolaj funkciu nacitaj hru
        this.transform.GetComponent<Button>().onClick.AddListener(delegate { scloader.loadGame(gameNumber); });
    }


}

