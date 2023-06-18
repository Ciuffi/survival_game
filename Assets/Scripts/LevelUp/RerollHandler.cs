using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerollHandler : MonoBehaviour
{
    public int baseReroll;
    public int currentReroll;
    public int baseSwap;
    public int currentSwap;


    // Start is called before the first frame update
    void Start()
    {
        currentReroll = baseReroll;
        currentSwap = baseSwap;
    }


    public void usedReroll()
    {
        currentReroll -= 1;

    }

    public void usedSwap()
    {
        currentSwap -= 1;

    }

    public void gainRerolls()
    {
        currentReroll += 1;
    }

    public void resetSwap()
    {
        currentSwap = baseSwap;
    }

    public void AddReroll (int amount)
    {
        currentReroll += amount;
    }

    public void AddSwap(int amount)
    {
        currentSwap += amount;
    }
}
