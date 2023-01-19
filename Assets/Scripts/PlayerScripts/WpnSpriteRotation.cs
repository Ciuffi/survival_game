using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WpnSpriteRotation : MonoBehaviour
{

    private VirtualJoystick VJ;
    public GameObject weapon;
    public float xOffset;
    public float yOffset;
    float inputX;
    float inputY;
   //bool autoAiming;
    //public GameObject AutoAim;
    
    // Start is called before the first frame update
    void Start()
    {
        inputX = 0;
        inputY = 0;

        VJ = GameObject.Find("Joystick Container").GetComponent<VirtualJoystick>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(weapon.transform.position.x + xOffset + inputX, weapon.transform.position.y + yOffset + inputY, weapon.transform.position.z);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, VJ.InputAngle + 90));

        // if (autoAiming == true)
        //{
        // Vector3 newAngle = AutoAim.GetComponent<AutoAim>().autoAimDirection;
        //  transform.rotation = Quaternion.Euler(newAngle.x + 90, newAngle.y + 90, 0);
        //   } else
        //  {
        //   transform.rotation = Quaternion.Euler(new Vector3(0, 0, VJ.InputAngle + 90));
        //  }



        if (VJ.InputAngle < 0) //input right
        {
            GetComponent<SpriteRenderer>().flipY = false;

        } else if (VJ.InputAngle > 0) //input left
        {
            GetComponent<SpriteRenderer>().flipY = true;
        }

    }

   // public void isAuto()
   // {
   //     autoAiming = true;
   // }

 //   public void stopAuto()
   // {
   //     autoAiming = false;
  //  }

    public void InputXY(float x, float y)
    {
        inputX = x / 3.75f ;
        inputY = y / 3.75f ;

    }
}
