using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;


public class PlayerGaze : MonoBehaviour
{
    private int bluePopped = 0;
    private int redPopped = 0;
    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    GameObject needle;

    private bool isEyeTrackingEnabled;
    GazePoint currentGazePoint;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (PlayerPrefs.GetInt("Game_2_ET_Enabled") > 0)
        {
            isEyeTrackingEnabled = true;
        }
        else
        {
            isEyeTrackingEnabled = false;
            Cursor.visible = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isEyeTrackingEnabled)
        {
            moveCursorWithGaze();
        }
        else
        {
            //moveNeedleToCursor();
        }
        CheckCursorCollisions();
        isGameOver();
    }

    void moveCursorWithGaze()
    {

        currentGazePoint = TobiiAPI.GetGazePoint();
        if(currentGazePoint.IsRecent())
        {
            Vector3 newPosition = mainCamera.ScreenToWorldPoint(new Vector3(currentGazePoint.Screen.x, currentGazePoint.Screen.y, needle.transform.position.z));
            newPosition = new Vector3(newPosition.x, newPosition.y, needle.transform.position.z);
            needle.transform.position = new Vector3(Mathf.Lerp(needle.transform.position.x, newPosition.x, 10*Time.deltaTime), Mathf.Lerp(needle.transform.position.y, newPosition.y, 10*Time.deltaTime),needle.transform.position.z);
        }

    }

    void moveNeedleToCursor ()
    {
        Vector3 newPosition = mainCamera.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, needle.transform.position.z));
        needle.transform.position = new Vector3(newPosition.x, newPosition.y, needle.transform.position.z);
    }

    public void resolveColor(bool isBlue)
    {
        if (isBlue)
        {
            bluePopped++;
        }
        else
        {
            redPopped++;
        }
    }

    void CheckCursorCollisions()
    {
        if (Input.GetButtonDown("Fire"))
        {
            Ray ray;
            if (isEyeTrackingEnabled && currentGazePoint.IsRecent())
            {
                ray = mainCamera.ScreenPointToRay(new Vector3(currentGazePoint.Screen.x, currentGazePoint.Screen.y, 0.0f));
            }
            else
            {
               ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            }

            RaycastHit targetHit;

            if (Physics.Raycast(ray, out targetHit))
            {
                if (targetHit.transform.tag == "Balloon")
                {
                    targetHit.transform.GetComponent<BalloonController>().triggerExplosion();
                }
            }
        }
    }

    private void isGameOver()
    {
        if (redPopped >= 5)
        {
            Debug.Log("Game Over, saving variables and calling gameOver scene");
            PlayerPrefs.SetInt("GameScore", bluePopped * 10);
            PlayerPrefs.SetFloat("GameTime", Time.timeSinceLevelLoad);
            PlayerPrefs.SetInt("Most_Recent_Game", 2);
            Debug.Log(PlayerPrefs.GetFloat("GameTime"));
            Debug.Log(PlayerPrefs.GetInt("GameScore"));
            Cursor.visible = true;
            GameObject.Find("LevelManager").GetComponent<SceneLoader>().loadGameOver();
        }
    }
}
