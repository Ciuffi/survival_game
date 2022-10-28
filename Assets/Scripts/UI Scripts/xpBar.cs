using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xpBar : MonoBehaviour
{

    public Slider xpBarSlider;
    private Image xpBarImage;

    public GameObject Player;
    public float currentXP;
    public float nextXP;

    private void FixedUpdate()
    {
        currentXP = Player.GetComponent<StatsHandler>().xp;
        nextXP = Player.GetComponent<StatsHandler>().nextXp;
        xpBarSlider.value = nextXP / currentXP;
    }


    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
