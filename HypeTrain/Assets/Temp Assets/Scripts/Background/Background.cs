// Background Script
// by Sam Fields

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum BackgroundState
{
    DAY,
    NIGHT,
    SUNSETTING,
    SUNRISING,
}

public class Background : LogController {

    [Tooltip("Indicates the length in seconds of a day (and of a night).")]
    public float dayLength = 60.0f;
    public float sunTransitionLength = 30.0f;
    public float timer;
    public Color nightColor = new Color(.01f, .01f, .5f, 1);
    public Color dayColor = new Color(1f, 1f, 1f, 0);
    private Color currColor;
    private BackgroundState bgState;
    private float t; // This is for the Color.Lerp function. "When t is 0 returns a. When t is 1 returns b."

    private void Start()
    {
        bgState = BackgroundState.DAY;
        timer = dayLength;
        t = 1.0f;
        currColor = Color.Lerp(nightColor, dayColor, t);
        GetComponent<Image>().color = currColor;
    }

    void Update() {

        switch(bgState)
        {
            case BackgroundState.DAY:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    bgState = BackgroundState.SUNSETTING;
                }
                break;

            case BackgroundState.NIGHT:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    bgState = BackgroundState.SUNRISING;
                }
                break;

            case BackgroundState.SUNSETTING:
                //  Increment t and paint the background with the new color.
                Debug.Log(Time.deltaTime);
                t = t - (Time.deltaTime / sunTransitionLength);
                currColor = Color.Lerp(nightColor, dayColor, t);
                GetComponent<Image>().color = currColor;
                if (currColor.Equals(nightColor))
                {
                    bgState = BackgroundState.NIGHT;
                    timer = dayLength;
                }
                break;

            case BackgroundState.SUNRISING:
                //  Increment t and paint the background with the new color.
                t = t + (Time.deltaTime / sunTransitionLength);
                currColor = Color.Lerp(nightColor, dayColor, t);
                GetComponent<Image>().color = currColor;
                if (currColor.Equals(dayColor))
                {
                    bgState = BackgroundState.DAY;
                    timer = dayLength;
                }
                break;
        }

	}
}
