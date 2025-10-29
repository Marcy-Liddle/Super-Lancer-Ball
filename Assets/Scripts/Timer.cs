using UnityEngine;
using TMPro;
using Unity.VisualScripting;



public class NewMonoBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float clock;
    public float startTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        clock = 60;
        startTime = Time.time;

        SetTimer(clock);
    }




    // Update is called once per frame

    void Update()
    {

        float elapsedTime = Time.time - startTime;

        float countdown = clock - Mathf.Round(elapsedTime);
        SetTimer(countdown);
  
    
    }

    void SetTimer(float gameTimer ) 
    {
        timerText.text = Mathf.Round(gameTimer).ToString();

        if (gameTimer < 0) 
        {
            timerText.color = Color.red;
        }

    }
}





