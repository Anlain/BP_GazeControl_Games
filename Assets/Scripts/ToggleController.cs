using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    [SerializeField] int gameNumber;
    Toggle toggle;

    private void Awake()
    {
        toggle = transform.GetComponent<Toggle>();

        if (PlayerPrefs.GetInt("Game_" + gameNumber + "_ET_Enabled") == 1)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        gameSettings gameSettings = GameObject.Find("GamesSettings").GetComponent<gameSettings>();

        toggle.onValueChanged.AddListener(delegate { gameSettings.setGameNumber_EyeTracking(toggle.isOn, gameNumber); });
    }


    

}
