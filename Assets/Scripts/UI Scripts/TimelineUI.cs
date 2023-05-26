using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public GameObject colorPrefab;
    public List<Color> rarityColors;

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerAttacks = Player.GetComponent<AttackHandler>();
        rarityColors = colorPrefab.GetComponent<InventoryItem>().rarityColors;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in attackContainer.transform)
        {
            attacks.Add(child.gameObject);
        }
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
        foreach (Attack attack in playerAttacks.attacks)
        {
            GameObject Icon = Instantiate(IconPrefab, transform);
            TimelineIcon t = Icon.GetComponent<TimelineIcon>();
            t.AssociatedAttack = attack;

            Icon.GetComponentInChildren<TextMeshProUGUI>().text = attack.weaponSetType.ToString();

            Icon.GetComponent<Image>().color = rarityColors[(int)attack.rarity];
            Icon.transform.Find("WpnImage").gameObject.GetComponent<Image>().sprite = attack.thrownSprite;

            Icon.transform.localPosition += Vector3.right * currentOffset;
            currentOffset += offsetAmount;
            icons.Add(Icon);

            // New code: Adjust visibility of delete button based on attack count
            var deleteButton = Icon.GetComponentInChildren<DeleteAttackButton>(); // get the DeleteAttackButton component
            if (deleteButton != null) // if it exists
            {
                deleteButton.gameObject.SetActive(playerAttacks.attacks.Count > 1); // set active only if there is more than one attack
            }
        }
    }

    public void despawnTimeline()
    {
        foreach (GameObject Icon in icons)
        {
            Destroy(Icon);
        }
        icons.Clear();
    }
}
