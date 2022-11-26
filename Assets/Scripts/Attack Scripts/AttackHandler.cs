using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackHandler : MonoBehaviour
{
    [SerializeField]
    public List<Attack> attacks;
    public AttackState attackState;
    public bool usingAttackBar;
    private GameObject attackContainer;
    private int attackIndex;
    
    private Slider attackBar;
    private Image attackBarImage;
    private GameObject defaultWeapon;
    public Color[] colors = {
        Color.blue,
        Color.green,
        Color.yellow,
        new Color(255, 177, 0),
        Color.red,
        Color.magenta
    };


    IEnumerator HandleAttackSlider(float castTime)
    {
        float timer = 0;
        while (true)
        {
            if (timer == 0) attackBarImage.color = colors[attackIndex];
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / castTime);
            attackBar.value = progress;
            yield return new WaitForEndOfFrame();
            if (timer >= castTime)
            {
                timer = 0;
                yield break;
            }
        }
    }


    IEnumerator Attack()
    {
        while (true)
        {
            attackState = AttackState.Casting;
            if (attacks.Count == 0) yield return null;
            Attack currentAttack = attacks[attackIndex];
            if (usingAttackBar) StartCoroutine(HandleAttackSlider(currentAttack.castTime));
            yield return new WaitForSeconds(currentAttack.castTime);
            StopCoroutine("HandleAttackSlider");
            attackState = AttackState.Attacking;
            if (currentAttack != null) currentAttack.Shoot();
            yield return new WaitForSeconds(currentAttack.attackTime);
            attackState = AttackState.Recovery;
            attackIndex++;
            if (attackIndex >= attacks.Count)
            {
                attackIndex = 0;
            }
            if (currentAttack.recoveryTime > 0) yield return new WaitForSeconds(currentAttack.recoveryTime);
        }
    }

    public void AddWeapon(GameObject weapon)
    {
        weapon.transform.parent = attackContainer.transform;
        attacks.Add(weapon.GetComponent<Attack>());
        weapon.GetComponent<Attack>().owner = GetComponent<Attacker>();
    }

    public void ResetWeapons()
    {
        StopCoroutine(Attack());

        foreach (Transform trans in attackContainer.transform)
        {
            Destroy(trans.gameObject);
        }
        attacks.Clear();
        attackIndex = 0;

        GameObject newWeapon = Instantiate(defaultWeapon, transform.position, Quaternion.identity);

        AddWeapon(newWeapon);
        StartCoroutine(Attack());
    }



    // Start is called before the first frame update
    void Start()
    {
        attackIndex = 0;
        attackBar = GameObject.Find("AttackBar").GetComponent<Slider>();
        attackBarImage = attackBar.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        attackContainer = new List<Transform>(GetComponentsInChildren<Transform>()).Find(t =>
        {
            return t.name == "Weapons";
        }).gameObject;
        new List<Attack>(attackContainer.GetComponentsInChildren<Attack>()).ForEach(a =>
        {
            AddWeapon(a.gameObject);
        });
        StartCoroutine(Attack());
        defaultWeapon = Resources.Load<GameObject>("Attacks/Single Shot");
    }

}
