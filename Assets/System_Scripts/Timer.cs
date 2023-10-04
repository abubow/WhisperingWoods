using System.Drawing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    // background panel
    [SerializeField] GameObject panel;
    [SerializeField] float timeRemaining = 0;
    [SerializeField] float timeScale = 1;
    [SerializeField] string sceneName;
    public bool isRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime * timeScale;
                DisplayTime(timeRemaining);
            }
            else {
                isRunning = false;
            }
        }
        else {
            // change scene
            SceneManager.LoadScene(sceneName);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // make background panel redder as time goes (after it's below 15), going from white to red
        if (timeToDisplay < 15)
        {
            panel.GetComponent<Image>().color = 2*(UnityEngine.Color.red * timeToDisplay) + (UnityEngine.Color.white * (15 - timeToDisplay));
        }
        else
        {
            panel.GetComponent<Image>().color = UnityEngine.Color.white;
        }
    }
}
