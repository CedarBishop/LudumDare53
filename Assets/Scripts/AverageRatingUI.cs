using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AverageRatingUI : MonoBehaviour
{
    //TODO: change to image of stars and use image fill amount
    public TextMeshProUGUI ratingText;

    private void Update()
    {
        ratingText.text = "Rating: " + GameManager.instance.GetAverageRating().ToString("F1");
    }
}
