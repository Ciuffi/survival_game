using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaypointManager : MonoBehaviour
{
    public Sprite arrowSprite;
    public Sprite eliteEnemySprite;
    public Sprite lootSprite;
    public Canvas uiCanvas;
    public float spriteOffset = 10f;  // Adjust this value to change the offset distance of the sprite.

    private List<Waypoint> waypoints = new List<Waypoint>();

    public void AddWaypoint(GameObject target, bool isEliteEnemy)
    {
        // Create waypoint object
        GameObject waypointObject = new GameObject("Waypoint");
        waypointObject.transform.SetParent(uiCanvas.transform, false);
        waypointObject.transform.SetAsLastSibling();

        // Create arrow as a child of waypoint object
        GameObject arrowObject = new GameObject("Arrow");
        arrowObject.transform.SetParent(waypointObject.transform, false);
        Image arrow = arrowObject.AddComponent<Image>();
        arrow.sprite = arrowSprite;
        arrow.color = new Color(1, 1, 1, 0.25f);

        // Create sprite based on type
        GameObject spriteObject = new GameObject("Sprite");
        Image sprite = spriteObject.AddComponent<Image>();
        sprite.color = new Color(1, 1, 1, 0.6f);
        spriteObject.transform.SetParent(waypointObject.transform, false);
        spriteObject.transform.localScale *= 0.6f;
        spriteObject.transform.localPosition = new Vector3(0, -spriteOffset, 0);  // Offset the sprite

        if (isEliteEnemy)
            sprite.sprite = eliteEnemySprite;
        else
            sprite.sprite = lootSprite;

        waypoints.Add(new Waypoint(waypointObject, arrowObject, target));
    }

    public void RemoveWaypoint(GameObject target)
    {
        Waypoint waypointToRemove = waypoints.Find(waypoint => waypoint.Target == target);
        if (waypointToRemove != null)
        {
            Destroy(waypointToRemove.WaypointObject);
            waypoints.Remove(waypointToRemove);
        }
    }


    private void Update()
    {
        foreach (Waypoint waypoint in waypoints)
            waypoint.UpdatePosition();
    }
}

public class Waypoint
{
    public GameObject WaypointObject { get; private set; }
    public GameObject ArrowObject { get; private set; } // New arrow object
    public GameObject Target { get; private set; }

    public Waypoint(GameObject waypointObject, GameObject arrowObject, GameObject target)
    {
        WaypointObject = waypointObject;
        ArrowObject = arrowObject; // Assign the arrow object
        Target = target;
    }

    public void UpdatePosition()
    {
        if (Time.timeScale == 0)
        {
            WaypointObject.SetActive(false);
            return;
        } else
        {
            WaypointObject.SetActive(true);
        }


        Camera cam = Camera.main;

        // Convert target's world position to viewport position
        Vector3 viewportPos = cam.WorldToViewportPoint(Target.transform.position);

        // If target is behind the camera, flip the position so the arrow points towards the target correctly
        if (viewportPos.z < 0)
        {
            viewportPos.x = 1 - viewportPos.x;
            viewportPos.y = 1 - viewportPos.y;
        }

        // Determine if target is offscreen
        bool isOffscreen = viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1;

        // If target is offscreen, adjust viewport position to be just inside the edge of the screen
        if (isOffscreen)
        {
            viewportPos = new Vector3(Mathf.Clamp(viewportPos.x, 0.05f, 0.95f), Mathf.Clamp(viewportPos.y, 0.05f, 0.95f), viewportPos.z);
            WaypointObject.SetActive(true);
        }
        else
        {
            WaypointObject.SetActive(false);
        }

        // Scale viewport position to screen size
        Vector3 screenPos = new Vector3(viewportPos.x * cam.pixelWidth, viewportPos.y * cam.pixelHeight, viewportPos.z);

        // Convert screen position to UI canvas position
        RectTransform canvasRect = WaypointObject.transform.parent as RectTransform;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect, screenPos, cam, out Vector3 worldPos);

        // Set the position of the waypoint
        WaypointObject.transform.position = worldPos;

        // Update the rotation of the arrow
        Vector3 dir = Target.transform.position - ArrowObject.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        ArrowObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
