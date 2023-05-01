using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICar : MonoBehaviour
{
    public float fadeSpeed;
    public float movementSpeed;
    public float accelerationSpeed;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;
    private OrthogonalDirection orthogonalDirection;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        OnSpawn();
    }

    private void Update()
    {
        float currentSpeed = 0;
        if (IsTrafficClear())
        {
            currentSpeed = Mathf.Lerp(rigidbody.velocity.magnitude, movementSpeed, accelerationSpeed);
        }
        else
        {
            currentSpeed = Mathf.Lerp(rigidbody.velocity.magnitude, 0, accelerationSpeed);
        }
        rigidbody.velocity = transform.up * currentSpeed;
    }

    public void OnSpawn()
    {
        StartCoroutine("CoFadeIn");
    }

    public void SetOrthogonalDirection(OrthogonalDirection newOrthogonalDirection)
    {
        orthogonalDirection = newOrthogonalDirection;
    }

    private bool IsTrafficClear()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.up, 1.0f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject == gameObject)
            {
                continue;
            }
            if (hit.collider.GetComponent<AICar>())
            {
                return false;
            }
            if (hit.collider.GetComponent<Player>())
            {
                return false;
            }
        }
        RaycastHit2D[] hits2 = Physics2D.RaycastAll(transform.position, transform.right * -1, 3.0f);
        foreach (RaycastHit2D hit in hits2)
        {
            if (hit.collider.gameObject == gameObject)
            {
                continue;
            }
            if (hit.collider.GetComponent<TrafficLight>() &&
                hit.collider.GetComponent<TrafficLight>().GetCurrentTrafficLightColor() == TrafficLightColor.Red &&
                hit.collider.GetComponent<TrafficLight>().orthogonalDirection == orthogonalDirection)
            {
                return false;
            }
        }
        return true;
    }

    public void FadeOnDestroy()
    {
        StartCoroutine("CoFadeOnDestroy");
    }

    IEnumerator CoFadeOnDestroy()
    {
        while (spriteRenderer.color.a > 0)
        {
            spriteRenderer.color -= new Color(0,0,0, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }

    IEnumerator CoFadeIn()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
        while (spriteRenderer.color.a < 1)
        {
            spriteRenderer.color += new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FadeOnDestroy();
    }
}
