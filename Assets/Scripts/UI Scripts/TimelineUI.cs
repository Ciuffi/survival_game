using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimelineUI : MonoBehaviour
{
    public GameObject Player;
    private AttackHandler playerAttacks;
    public GameObject attackContainer;

    public GameObject IconPrefab;
    private List<GameObject> icons;
    public List<GameObject> attacks;
    public float offsetAmount;
    private float currentOffset;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        playerAttacks = Player.GetComponent<AttackHandler>();

        foreach (Transform child in attackContainer.transform)
        {
            attacks.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addAttack()
    {
        foreach (Transform child in attackContainer.transform)
        {
            if (child.gameObject.tag == "Attack")
            {
               attacks.Add(child.gameObject);
            }
        }

    }

    public void spawnTimeline()
    {
       

        currentOffset = 0;
        icons = new List<GameObject>();
        foreach (GameObject attack in attacks)
        {
            GameObject Icon = Instantiate(IconPrefab, transform);
            Icon.transform.position += Vector3.left * currentOffset;
            currentOffset += offsetAmount;
            Icon.transform.GetChild(0).GetComponent<TMP_Text>().text = attack.name;
            icons.Add(Icon);
        }
    }

    public void despawnTimeline()
    {
        foreach (GameObject Icon in icons)
        {
            Destroy(Icon);
        }
        icons.Clear();
        attacks.Clear();
    }
}
