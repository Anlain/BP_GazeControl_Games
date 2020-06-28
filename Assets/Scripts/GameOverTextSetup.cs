using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverTextSetup : MonoBehaviour
{
    private Text textfield;

    // Start is called before the first frame update
    private void Start()
    {
        textfield = transform.GetComponent<Text>();
        textfield.text = "Získal si: " + PlayerPrefs.GetInt("GameScore") + " bodov\n" +
                 "a prežil si: " + PlayerPrefs.GetFloat("GameTime") + " sekúnd";
    }

}
