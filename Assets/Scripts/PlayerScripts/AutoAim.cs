using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public float aimRange = 2.5f;

    public float coneAngle = 60f; // Added cone angle
    public bool isCone = false; // Added bool to decide which implementation to use

    public SpriteRenderer rangeVisualizerSprite;
    public LayerMask enemyLayer;
    public WpnSpriteRotation weaponSpriteRotation;

    public GameObject currentTarget;
    public bool targetFound;

    private GameObject coneMeshObject; // Added cone mesh object
    public Material coneMaterial; // Material for the cone mesh

    public Vector2 AimDirection { get; private set; }
    PlayerMovement joystick;

    void Start()
    {
        joystick = FindObjectOfType<PlayerMovement>();
        UpdateVisualizerSpriteScale();
        weaponSpriteRotation = FindObjectOfType<WpnSpriteRotation>();
        if (isCone)
        {
            CreateConeMeshObject(); // Added cone mesh object creation
        }
    }

    void Update()
    {
        UpdateCurrentTarget();
        weaponSpriteRotation.SetAutoAim(targetFound, currentTarget);
    }

    public void UpdateAimRange(float aimRangeBase, float aimRangeAdded, bool wpnIsCone, float aimConeAngle)
    {
        aimRange = aimRangeBase + aimRangeAdded;
        aimRange = aimRange < 0.1f ? 0.1f : aimRange;

        isCone = wpnIsCone;
        coneAngle = aimConeAngle;

        UpdateVisualizerSpriteScale();
    }

    void UpdateCurrentTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, aimRange, enemyLayer);
        float closestDistance = float.MaxValue;
        GameObject closestEnemy = null;
        Vector2 aimDirection = transform.up;

        foreach (Collider2D col in colliders)
        {
            Enemy enemy = col.GetComponent<Enemy>();

            if (enemy != null && !enemy.isDead)
            {
                float distance = Vector2.Distance(transform.position, col.transform.position);
                Vector2 targetDirection = (col.transform.position - transform.position).normalized;

                if (isCone)
                {
                    float angle = Vector2.Angle(aimDirection, targetDirection);

                    if (distance < closestDistance && angle <= coneAngle * 0.5f)
                    {
                        closestDistance = distance;
                        closestEnemy = col.gameObject;
                    }
                }
                else
                {
                    // added check for direction based on last input direction
                    float dotProduct = Vector2.Dot(joystick.lastInputDirection.normalized, targetDirection);
                    if (distance < closestDistance && dotProduct > 0)
                    {
                        closestDistance = distance;
                        closestEnemy = col.gameObject;
                    }
                }
            }
        }

        currentTarget = closestEnemy;
        targetFound = currentTarget != null;
    }

    void UpdateVisualizerSpriteScale()
    {
        if (!isCone && rangeVisualizerSprite != null)
        {
            rangeVisualizerSprite.transform.localScale = new Vector3(aimRange * 1.5f, aimRange * 1.5f, 1);
        }
        else 
        {
            rangeVisualizerSprite.transform.localScale = new Vector3(0, 0, 0);
        }

        if (isCone && coneMeshObject != null)
        {
            coneMeshObject.SetActive(true); // Enable cone mesh object
            UpdateConeMesh(coneMeshObject, aimRange, coneAngle);
        }
        else if (!isCone && coneMeshObject != null)
        {
            coneMeshObject.SetActive(false); // Disable cone mesh object
        }
    }

    void OnDrawGizmos()
    {
        if (isCone)
        {
            // Draw cone visualization
            Gizmos.color = Color.yellow;
            float coneRange = aimRange;
            float halfConeAngle = coneAngle * 0.5f;
            Quaternion coneLeft = Quaternion.AngleAxis(-halfConeAngle, Vector3.forward);
            Quaternion coneRight = Quaternion.AngleAxis(halfConeAngle, Vector3.forward);
            Vector3 leftDirection = coneLeft * transform.up;
            Vector3 rightDirection = coneRight * transform.up;
            Gizmos.DrawLine(transform.position, transform.position + leftDirection * coneRange);
            Gizmos.DrawLine(transform.position, transform.position + rightDirection * coneRange);
        } else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, aimRange);
        }
    }

    void CreateConeMeshObject()
    {
        coneMeshObject = new GameObject("ConeMeshObject");
        coneMeshObject.transform.SetParent(transform);
        coneMeshObject.transform.localPosition = Vector3.zero;
        coneMeshObject.transform.localRotation = Quaternion.identity;

        MeshRenderer meshRenderer = coneMeshObject.AddComponent<MeshRenderer>();
        meshRenderer.material = coneMaterial;

        MeshFilter meshFilter = coneMeshObject.AddComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();

        UpdateConeMesh(coneMeshObject, aimRange, coneAngle);
    }

    void UpdateConeMesh(GameObject coneObject, float range, float angle)
    {
        MeshFilter meshFilter = coneObject.GetComponent<MeshFilter>();
        if (meshFilter == null) return;

        Mesh mesh = meshFilter.mesh;
        mesh.Clear();

        int numSegments = 36;
        Vector3[] vertices = new Vector3[numSegments + 2];
        Vector2[] uv = new Vector2[numSegments + 2];
        int[] triangles = new int[numSegments * 3];

        vertices[0] = Vector3.zero;
        uv[0] = new Vector2(0.5f, 0.5f);

        float angleStep = angle / numSegments;
        float uvStep = 1f / numSegments;

        for (int i = 0; i < numSegments + 1; i++)
        {
            float currentAngle = -angle * 0.5f + angleStep * i;
            float x = range * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
            float y = range * Mathf.Cos(currentAngle * Mathf.Deg2Rad);

            vertices[i + 1] = new Vector3(x, y, 0);
            uv[i + 1] = new Vector2(uvStep * i, 1);

            if (i < numSegments)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }
    }
}