using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OrthogonalDirection { Up, Right, Down, Left}

public class AISpawner : MonoBehaviour
{
    public OrthogonalDirection directionAIToTravelIn;
    public float minSpawnTimeInterval;
    public float maxSpawnTimeInterval;
    public AICar aiCarPrefab;

    IEnumerator Start()
    {
        switch (directionAIToTravelIn)
        {
            case OrthogonalDirection.Up:
                transform.up = Vector3.up;
                break;
            case OrthogonalDirection.Right:
                transform.up = Vector3.right;
                break;
            case OrthogonalDirection.Down:
                transform.up = Vector3.down;
                break;
            case OrthogonalDirection.Left:
                transform.up = Vector3.left;
                break;
            default:
                break;
        }
        while (true)
        {
            var ai = Instantiate(aiCarPrefab, transform.position, transform.rotation);
            ai.SetOrthogonalDirection(directionAIToTravelIn);
            yield return new WaitForSeconds(Random.Range(minSpawnTimeInterval, maxSpawnTimeInterval));
        }
    }

    public float gizmoRadius = 1f;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, gizmoRadius);       
    }
#endif

}
