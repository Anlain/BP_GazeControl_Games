using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameSettings : MonoBehaviour
{
    public void setGame1_EyeTracking(bool isEnabled)
    {
        if (isEnabled)
        {
            PlayerPrefs.SetInt("Game_1_ET_Enabled", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Game_1_ET_Enabled", 0);
        }
        Debug.Log("Game1 EyeTracking control: " + isEnabled);
    }

    public void setGame2_EyeTracking(bool isEnabled)
    {
        if (isEnabled)
        {
            PlayerPrefs.SetInt("Game_2_ET_Enabled", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Game_2_ET_Enabled", 0);
        }
        Debug.Log("Game2 EyeTracking control: " + isEnabled);
    }

    public void setGame3_EyeTracking(bool isEnabled)
    {
        if (isEnabled)
        {
            PlayerPrefs.SetInt("Game_3_ET_Enabled", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Game_3_ET_Enabled", 0);
        }
        Debug.Log("Game3 EyeTracking control: " + isEnabled);
    }

    public bool getGame1_EyeTracking()
    {
        if (PlayerPrefs.GetInt("Game_1_ET_Enabled") > 0)
        {
            return true;
        }
        return false;
    }

    public bool getGame2_EyeTracking()
    {
        if (PlayerPrefs.GetInt("Game_2_ET_Enabled") > 0)
        {
            return true;
        }
        return false;
    }

    public bool getGame3_EyeTracking()
    {
        if (PlayerPrefs.GetInt("Game_3_ET_Enabled") > 0)
        {
            return true;
        }
        return false;
    }

    public void setGameNumber_EyeTracking(bool value, int index)
    {
        if (index == 1) { setGame1_EyeTracking(value); }
        if (index == 2) { setGame2_EyeTracking(value); }
        if (index == 3) { setGame3_EyeTracking(value); }
    }
}
