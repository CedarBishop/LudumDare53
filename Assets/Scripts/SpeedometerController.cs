using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpeedometerController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Image forwardSpeedometre;
    [SerializeField] Image reverseSpeedometre;

   // private float currentSpeed;
    private float maxForwardSpeed;
    private float maxReverseSpeed;
    private bool isReversing;

    void Start()
    {
        player = FindObjectOfType<Player>();
        maxForwardSpeed = player.GetMaxForwardSpeed();
        maxReverseSpeed = player.GetMaxReverseSpeed();
        isReversing = player.GetIsReversing();
    }


    void Update()
    {

        float currentSpeed = player.GetCurrentSpeed();

        if(currentSpeed > 0)
        {
            forwardSpeedometre.fillAmount = player.GetCurrentSpeed() / maxForwardSpeed;
        }
        else if (currentSpeed < 0)
        {
            reverseSpeedometre.fillAmount = player.GetCurrentSpeed() / maxReverseSpeed;
        }
        else
        {
            forwardSpeedometre.fillAmount = 0;
            reverseSpeedometre.fillAmount = 0;
        }

        //Debug.Log(player.GetCurrentSpeed() / maxForwardSpeed);
    }
}
