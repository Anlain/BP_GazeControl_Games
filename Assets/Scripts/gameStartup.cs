using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStartup : MonoBehaviour
{
    private void Awake()
    {
        // We want clear record for each session 
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("GameScore", 0);
        PlayerPrefs.SetFloat("GameTime", 0.0f);
        PlayerPrefs.SetInt("Game_1_ET_Enabled", 0);
        PlayerPrefs.SetInt("Game_2_ET_Enabled", 0);
        PlayerPrefs.SetInt("Game_3_ET_Enabled", 0);
    }
}
