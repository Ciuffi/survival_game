using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopupText : MonoBehaviour
{
    TextMeshPro textMesh;

    public float moveYspeed;
    public float disappearTimer;
    public float disappearSpeed;
    public Color textColor;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Setup(float damageAmount)
    {
        textMesh = gameObject.GetComponent<TextMeshPro>();
        int damageRounded = Mathf.CeilToInt(damageAmount);
        textMesh.SetText(damageRounded.ToString());
        textColor = textMesh.color;
    }

    // Update is called once per frame 
    void Update()
    {
        transform.position += new Vector3(0, moveYspeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }

        } 
    }
}
