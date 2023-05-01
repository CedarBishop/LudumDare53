using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSoundPlayer : MonoBehaviour
{

    public float minSpeed = 5f;
    public float maxSpeed;
    private float currentSpeed;

    private Rigidbody2D carRb;

    public float minPitch;
    public float maxPitch;
    private float pitchFromCar;
    [SerializeField] float pitchDivider = 50f;
    private AudioSource carAudio;


    void Start()
    {
        maxSpeed = GetComponent<Player>().maxForwardSpeed;

        carAudio = SoundManager.instance.CarEnginePlayer();
        carRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        EngineSound();
    }

    void EngineSound()
    {
        currentSpeed = carRb.velocity.magnitude;
        pitchFromCar = carRb.velocity.magnitude / 50f;

        if (currentSpeed < minSpeed)
        {
            carAudio.pitch = minPitch;
        }

        if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            carAudio.pitch = minPitch + pitchFromCar;
        }

        if (currentSpeed > maxSpeed)
        {
            carAudio.pitch = maxPitch;
        }
    }
}
