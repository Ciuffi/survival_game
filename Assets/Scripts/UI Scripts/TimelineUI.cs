using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

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

        // Store the final positions and scales of the icons
        List<Vector3> finalPositions = new List<Vector3>();

        foreach (Attack attack in playerAttacks.attacks)
        {
            GameObject Icon = Instantiate(IconPrefab, transform);
            TimelineIcon t = Icon.transform.Find("WpnImage").gameObject.GetComponent<TimelineIcon>();
            t.AssociatedAttack = attack;

            Icon.GetComponentInChildren<TextMeshProUGUI>().text = attack.weaponSetType.ToString();

            Icon.transform.Find("BG").gameObject.GetComponent<Image>().color = rarityColors[(int)attack.stats.GetRarity()];
            Icon.transform.Find("WpnImage").gameObject.GetComponent<Image>().sprite = attack.thrownSprite;

            finalPositions.Add(Icon.transform.localPosition + Vector3.right * currentOffset);

            // Move the icon offscreen and scale it down
            Icon.transform.localPosition += Vector3.right * 2000;

            currentOffset += offsetAmount;
            icons.Add(Icon);

            // Adjust visibility of delete button based on attack count
            var deleteButton = Icon.GetComponentInChildren<DeleteAttackButton>();
            if (deleteButton != null)
            {
                deleteButton.gameObject.SetActive(playerAttacks.attacks.Count > 1);
            }
        }

        // Calculate the width of each icon (assuming they're all the same size)
        float iconWidth = IconPrefab.GetComponent<RectTransform>().rect.width;
        // The space between each icon
        float spacing = 10f;

        // Animate the icons into place
        float delay = 0.5f;
        for (int i = 0; i < icons.Count; i++)
        {
            // Calculate the final position of each icon
            Vector3 finalPos = new Vector3((iconWidth + spacing) * i, 0, 0);

            // Create a sequence for each icon
            Sequence sequence = DOTween.Sequence();
            // Append a move and scale tween to the sequence
            sequence.Join(icons[i].transform.DOLocalMove(finalPos, 0.5f).SetEase(Ease.OutExpo));
            sequence.SetUpdate(true);
            // Start the sequence after a delay
            sequence.Play().SetDelay(delay);

            delay += 0.075f;
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
