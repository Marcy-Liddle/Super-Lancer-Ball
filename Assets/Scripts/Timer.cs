using UnityEngine;
using TMPro;
using Unity.VisualScripting;



public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public static float clock;
    public static float startTime;

    public static bool gameWon = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Initialises the countdown timer
        clock = 120;
        startTime = Time.time;

        SetTimer(clock);
    }



    // Update is called once per frame

    void Update()
    {
        //if player has not won the level, count down the timer 
        if (gameWon == false)
        {
            float elapsedTime = Time.time - startTime;

            float countdown = clock - Mathf.Round(elapsedTime);
            SetTimer(countdown);

            //point deductions to decrease total score if time is running over
            if (countdown < 0 && countdown > -15)
            {
                PlayerController.count -= -50; 
            }
            else if (countdown < -15 && countdown > -30)
            {
                PlayerController.count -= 100;
            }

        }
    }

    void SetTimer(float gameTimer)
    {
        //display the timer to the player
        timerText.text = Mathf.Round(gameTimer).ToString();

        if (gameTimer < 0)
        {
            timerText.color = Color.red;
        }

    }
}
