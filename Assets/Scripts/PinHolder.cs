using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinHolder : MonoBehaviour
{

    public GameObject markerPrefab;
    public GameObject playerObject;
    public RectTransform markerParentRectTransform;
    public Camera minimapCamera;

    private List<(LocationTracker objectivePosition, RectTransform markerRectTransform)> currentObjectives;

    // Start is called before the first frame update
    void Awake()
    {
        currentObjectives = new List<(LocationTracker objectivePosition, RectTransform markerRectTransform)>();
    }


    public float scaler;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    

    // Update is called once per frame
    void Update()
    {
        foreach ((LocationTracker objectivePosition, RectTransform markerRectTransform) marker in currentObjectives)
        {
            Vector3 offset = Vector3.ClampMagnitude(marker.objectivePosition.transform.position - playerObject.transform.position, 
                minimapCamera.orthographicSize);
            offset = offset / minimapCamera.orthographicSize * (markerParentRectTransform.rect.width / 2f);

            Vector3 offsetDir = (marker.objectivePosition.transform.position - playerObject.transform.position);
            offsetDir = offsetDir * scaler;

            if (offsetDir.x > maxX)
            {
                offsetDir.x = maxX;
            }
            if (offsetDir.x < minX)
            {
                offsetDir.x = minX;
            }
            if (offsetDir.y > maxY)
            {
                offsetDir.y = maxY;
            }
            if (offsetDir.y < minY)
            {
                offsetDir.y = minY;
            }

            //marker.markerRectTransform.anchoredPosition = new Vector2(offset.x, offset.z);
            marker.markerRectTransform.anchoredPosition = new Vector2(offsetDir.x, offsetDir.y);
           
        }
    }
           // Vector3 offset = Vector3.ClampMagnitude(buff, minimapCamera.orthographicSize);

    public void AddObjectiveMarker(LocationTracker sender)
    {
        RectTransform rectTransform = Instantiate(markerPrefab, markerParentRectTransform).GetComponent<RectTransform>();
        currentObjectives.Add((sender, rectTransform));
    }

    public void RemoveObjectiveMarker(LocationTracker sender)
    {
        if (!currentObjectives.Exists(objective => objective.objectivePosition == sender))
            return;
        (LocationTracker pos, RectTransform rectTrans) foundObj = currentObjectives.Find(objective => objective.objectivePosition == sender);
        Destroy(foundObj.rectTrans.gameObject);
        currentObjectives.Remove(foundObj);
    }
}
