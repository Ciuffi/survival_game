using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{

    public GameObject weapon;
    public Animator animator;

    float startTime;
    float castTime;
    float timer;
    bool part2;

    // Start is called before the first frame update
    void Start()
    {
        startTime = weapon.GetComponent<Attack>().recoveryTime;
        castTime = weapon.GetComponent<Attack>().castTime;

        timer = startTime;
        part2 = false;

        animator.SetBool("IsAttacking", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

        }


        if (timer <= 0 && part2 == false)
        {
            timer = castTime - startTime;
            animator.SetBool("IsAttacking", false);
            part2 = true;
        }
        else if (timer <= 0 && part2 == true)
        {
            timer = startTime;
            animator.SetBool("IsAttacking", true);
            part2 = false;

        }




    }
}
