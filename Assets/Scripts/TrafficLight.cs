using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrafficLightColor {Invalid, Green, Yellow, Red}

public class TrafficLight : MonoBehaviour
{
    public TrafficLightColor startingTrafficLightColor;
    public OrthogonalDirection orthogonalDirection;

    public float greenTime;
    public float yellowTime;
    public float redTime;

    public Sprite greenLightSprite;
    public Sprite yellowLightSprite;
    public Sprite redLightSprite;

    private TrafficLightColor currentTrafficLightColor;

    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        SetCurrentTrafficLightColor(startingTrafficLightColor);
        StartCoroutine("CycleToNextColor");
    }

    public TrafficLightColor GetCurrentTrafficLightColor()
    {
        return currentTrafficLightColor;
    }

    public void SetCurrentTrafficLightColor(TrafficLightColor trafficLightColor)
    {
        currentTrafficLightColor = trafficLightColor;
        switch (trafficLightColor)
        {
            case TrafficLightColor.Green:
                spriteRenderer.sprite = greenLightSprite;
                break;
            case TrafficLightColor.Yellow:
                spriteRenderer.sprite = yellowLightSprite;
                break;
            case TrafficLightColor.Red:
                spriteRenderer.sprite = redLightSprite;
                break;
            default:
                break;
        }
    }

    IEnumerator CycleToNextColor()
    {
        while (true)
        {
            switch (GetCurrentTrafficLightColor())
            {
                case TrafficLightColor.Green:
                    yield return new WaitForSeconds(greenTime);
                    SetCurrentTrafficLightColor(TrafficLightColor.Yellow);
                    break;
                case TrafficLightColor.Yellow:
                    yield return new WaitForSeconds(yellowTime);
                    SetCurrentTrafficLightColor(TrafficLightColor.Red);
                    break;
                case TrafficLightColor.Red:
                    yield return new WaitForSeconds(redTime);
                    SetCurrentTrafficLightColor(TrafficLightColor.Green);
                    break;
                default:
                    SetCurrentTrafficLightColor(TrafficLightColor.Green);
                    yield return null;
                    break;
            }
        }
    }
}
