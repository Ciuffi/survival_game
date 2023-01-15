using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AttackGrid : MonoBehaviour
{

    public GameObject squareUIprefab;
    public GameObject textPrefab;
    public float xAdjust;
    public float yAdjust;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Canvas game object
        GameObject canvasObject = GameObject.Find("Canvas");

        // Get the Canvas's RectTransform component
        RectTransform canvasTransform = canvasObject.GetComponent<RectTransform>();

        // Get the Canvas's position as a Vector3
        Vector3 canvasPosition = canvasTransform.position;

        Vector3 spawnPos = new Vector3(canvasPosition.x + xAdjust, canvasPosition.y + yAdjust, canvasPosition.z);

        InstantiateRandomColorGrid(squareUIprefab, spawnPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiateRandomColorGrid(GameObject prefab, Vector3 position)
    {
        // Create a list of the primary colors
        List<Color> primaryColors = new List<Color> { Color.red, Color.yellow, Color.blue , Color.magenta, Color.cyan, Color.green};

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                // Calculate the position of the prefab
                Vector3 prefabPosition = position + new Vector3(i + 250f, j + 250f, 0);

                // Instantiate the prefab
                GameObject prefabInstance = Instantiate(prefab, prefabPosition, Quaternion.identity);

                // Get the prefab's SpriteRenderer component
                SpriteRenderer prefabRenderer = prefabInstance.GetComponent<SpriteRenderer>();

                // Set the prefab's color to a random primary color
                int randomIndex = Random.Range(0, primaryColors.Count);
                Color randomColor = primaryColors[randomIndex];

                // Set the prefab's color to the chosen color
                prefabRenderer.color = randomColor;

                // Remove the chosen color from the list so it cannot be used again
                primaryColors.RemoveAt(randomIndex);

                // Instantiate a TextMesh UI element under the prefab
                GameObject textObject = Instantiate(textPrefab, prefabInstance.transform.position - new Vector3(0, 1f, 0), Quaternion.identity);

                // Set the text of the TextMesh to a new string
                textObject.GetComponent<TextMeshPro>().text = "Hello World!";
                textObject.GetComponent<TextMeshPro>().fontSize = 200;
            }
        }
    }
}
