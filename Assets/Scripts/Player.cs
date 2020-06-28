using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class Player : MonoBehaviour
{
    private CharacterController charController;
    private AudioSource audioPlayer;

    [SerializeField]
    [Tooltip("Rychlost akou sa hracova postava pohybuje vpred.")]
    private float speed = 15.0f;

    [SerializeField]
    private float jumpHeight = 20.0f;

    [SerializeField]
    private float gravity = 1.0f;

    [SerializeField]
    [Range(0, 1)]
    [Tooltip("Urcuje hranicu pre vykonanie skoku podla oci. 0 je stred obrazovky, 1 je uplny vrchol obrazovky.")]
    private float jumpThreshold = 0.50f;

    private float yVelocity = 0.0f;
    private float xVelocity = 0.0f;
    private Vector2 screenCenter;
    private float eyeHorizontalFactor;
    private float eyeVerticalFactor;
    private bool isEyeTrackingEnabled;
    private float keySensitivity = 0.5f;

    private float totalTime = 0.0f;
    private int score = 0;

    void Start()
    {
        //Debug.Log("Screen Width: " + Screen.width + "Screen Height: " + Screen.height);
        if (PlayerPrefs.GetInt("Game_1_ET_Enabled") > 0)
        {
            isEyeTrackingEnabled = true;
        }
        else
        {
            isEyeTrackingEnabled = false;
        }

        charController = GetComponent<CharacterController>();
        screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        eyeHorizontalFactor = eyeVerticalFactor = 0;
        DisplayInfo display = TobiiAPI.GetDisplayInfo();
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEyeTrackingEnabled)
        {
            GazePoint gazePoint = TobiiAPI.GetGazePoint();
            //Debug.Log("x: " + gazePoint.Screen.x + "   y: " + gazePoint.Screen.y);

            if (!gazePoint.IsValid)
            {
                return;
            }
            else
            {
                setEyeHorizontalFactor(gazePoint);                                              // Calculate horizontal eye movement from screen center
                setEyeVerticalFactor(gazePoint);                                                // Calculate vertical eye movement from screen center
                //Debug.Log("x: " + eyeHorizontalFactor + "   y: " + eyeVerticalFactor);
                Vector3 direction = new Vector3(eyeHorizontalFactor, 0, 1);
                Vector3 velocity = direction * speed;

                if (charController.isGrounded == true)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        yVelocity = jumpHeight;
                        audioPlayer.Play();
                    }
                }
                else
                {
                    yVelocity -= gravity;
                    //velocity.x = xVelocity;
                }

                velocity.y = yVelocity;


                charController.Move(velocity * Time.deltaTime);
            }
        }
        else
        {
            float horizontal = Input.GetAxis("Horizontal") * keySensitivity;
            Vector3 direction = new Vector3(horizontal, 0.0f, 1.0f);
            //Debug.Log(direction);
            Vector3 velocity = direction * speed;

            if (charController.isGrounded == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    yVelocity = jumpHeight;
                    audioPlayer.Play();
                }
            }
            else
            {
                yVelocity -= gravity;
                //velocity.x = xVelocity;
            }

            velocity.y = yVelocity;

            charController.Move(velocity * Time.deltaTime);
        }
    }

    private void setEyeVerticalFactor(GazePoint gazePoint)
    {
        int GazeCenterDeviation = (int)(gazePoint.Screen.y - screenCenter.y);
        eyeVerticalFactor = GazeCenterDeviation / (float)screenCenter.y;
    }

    private void setEyeHorizontalFactor(GazePoint gazePoint)
    {
        int GazeCenterDeviation = (int)(gazePoint.Screen.x - screenCenter.x);
        eyeHorizontalFactor = GazeCenterDeviation / (float)screenCenter.x;
        Debug.Log(eyeHorizontalFactor);

        if (eyeHorizontalFactor > 0.02f)
        {
            eyeHorizontalFactor += 0.07f;
            return;

        }
        else if (eyeHorizontalFactor < -0.02f)
        {
            eyeHorizontalFactor -= 0.07f;
            return;
        }
        else if (eyeHorizontalFactor > 0.07f)
        {
            eyeHorizontalFactor += 0.2f;
            return;
        }
        else if (eyeHorizontalFactor < -0.07f)
        {
            eyeHorizontalFactor -= 0.2f;
            return;
        }
        else
        {
            eyeHorizontalFactor = 0;
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Harmful")
        {
            Debug.Log("Harmful target hit. Player dies. ");
            GameOver();
        }
        if (hit.gameObject.tag == "TileExit")
        {
            score += 10;
            speed += 0.05f;
        }
    }

    private void GameOver()
    {
        Debug.Log(score);
        Debug.Log(Time.timeSinceLevelLoad);
        PlayerPrefs.SetInt("Most_Recent_Game", 1);
        PlayerPrefs.SetInt("GameScore", score);
        PlayerPrefs.SetFloat("GameTime", Time.timeSinceLevelLoad);
        Debug.Log(PlayerPrefs.GetFloat("GameTime"));
        Debug.Log(PlayerPrefs.GetInt("GameScore"));
        GameObject.Find("LevelManager").GetComponent<SceneLoader>().loadGameOver();
    }

    public void addScore()
    {
        score += 10;
    }
}
