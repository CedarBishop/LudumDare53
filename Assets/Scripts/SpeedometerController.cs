using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpeedometerController : MonoBehaviour
{
    [SerializeField] Player player;

    // Speedometre
    [SerializeField] Image forwardSpeedometre;
    [SerializeField] Image reverseSpeedometre;
    private float maxForwardSpeed;
    private float maxReverseSpeed;

    // Cruise Control
    [SerializeField] RectTransform cruiseControlArrow;
    private float forwardImageSize;
    private float reverseImageSize;
    [SerializeField] private TMP_Text cruiseSpeedTMP;

    //Debug
    public float var1;
    public float var2;

    void Start()
    {
        forwardImageSize = forwardSpeedometre.transform.localScale.x;
        reverseImageSize = reverseSpeedometre.transform.localScale.x;

        player = FindObjectOfType<Player>();
        maxForwardSpeed = player.GetMaxForwardSpeed();
        maxReverseSpeed = player.GetMaxReverseSpeed();
    }


    void Update()
    {

        float currentSpeed = player.GetCurrentSpeed();

        if(currentSpeed > 0)
        {
            forwardSpeedometre.fillAmount = player.GetCurrentSpeed() / maxForwardSpeed;
           SetCurrentCruiseSpeedUI(forwardImageSize, maxForwardSpeed);
        }
        else if (currentSpeed < 0)
        {
            reverseSpeedometre.fillAmount = player.GetCurrentSpeed() / maxReverseSpeed;
           SetCurrentCruiseSpeedUI(reverseImageSize, maxReverseSpeed);
        }
        else
        {
            forwardSpeedometre.fillAmount = 0;
            reverseSpeedometre.fillAmount = 0;
            cruiseControlArrow.position = new Vector3 (0f, cruiseControlArrow.position.y, cruiseControlArrow.position.z);
        }
        cruiseSpeedTMP.SetText(("Cruise: " + player.GetCruiseControlSpeed()));

        //Debug.Log(player.GetCurrentSpeed() / maxForwardSpeed);
    }


    //private void OnValidate()
    //{
    //    SetCurrentCruiseSpeedUI(var1,var2);
    //}

    private void SetCurrentCruiseSpeedUI( float imageSize, float maxSpeed)
    {
        cruiseControlArrow.localPosition = new Vector3((player.GetCruiseControlSpeed() / maxSpeed) * imageSize, cruiseControlArrow.position.y, cruiseControlArrow.position.z);
        //cruiseControlArrow.position = new Vector3(imageSize, maxSpeed, 0);
        player.GetCruiseControlSpeed();
    }
}
