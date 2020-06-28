using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader _instance; 

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            loadMainMenu();
        }
    }

    public void loadGame1()
    {
        SceneManager.LoadScene(1);
    }

    public void loadGame2()
    {
        SceneManager.LoadScene(2);
    }

    public void loadGame3()
    {
        SceneManager.LoadScene(3);
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void loadGame(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void loadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
