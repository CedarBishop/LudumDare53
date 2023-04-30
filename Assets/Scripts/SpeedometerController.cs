using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpeedometerController : MonoBehaviour
{
    private Player player;

    // Speedometre
    [SerializeField] Image forwardSpeedometre;
    [SerializeField] Image reverseSpeedometre;
    private float maxForwardSpeed;
    private float maxReverseSpeed;

    [SerializeField] private TMP_Text speedText;

    void Start()
    {
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
        speedText.SetText((player.GetCurrentSpeed().ToString("F1") + " Kms/h"));

    }
}
