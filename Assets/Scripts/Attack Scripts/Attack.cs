using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour, Upgrade
{
    public float damage;
    public float damageUP;

    public float spread;
    public float spreadUP;
    public float spray;
    public int sprayThreshold;
    private int shotsCount = 0;

    public float castTime;
    public float castTimeUP;
    public float attackTime;
    public float recoveryTime;
    public float recoveryTimeUp;

    public float range = 5;
    public float rangeUP;

    public int shotsPerAttack;
    public int shotsPerAttackUP;

    public bool cantMove;

    public float speed;
    public float speedUP;

    public float knockback;
    public float knockbackUP;

    public int pierce;
    public int pierceUP;

    public float critChance; // 1 = 100% crit chance, 0 = 0% crit chance
    public float critDmg; //1 = 100% of normal damage on a crit, 2 = 200% damage, etc.

    public float multicastChance; //every 1.0f = one guarenteed multicast.
    public float multicastWaitTime;
    private float defaultMulticastWaitTime;
    public int multicastTimes;
    private int numMulticast = 0;

    public GameObject projectile;
    public Vector3 scaleUP;

    public int rarity = 0; //0-common, 1-rare, 2-epic, 4-legendary
    public List<int> chosenNumbers = new List<int>();

    public Effect effect;
    public AttackTypes attackType;
    public Attacker owner;
    GameObject Player;
    GameObject Camera;

    public GameObject MeleeAttack;
    public bool isToss;
    public int comboLength; // # of melee attacks instantiated in a row
    public float comboWaitTime;
    public float comboAttackBuff; //percent buff

    public float meleeShotsScaleUp; // scales up by % amount after each attack in the shotsPerattack
    public float meleeSpacer = 0.7f; //spacer for first melee attack
    public float meleeSpacerGap = 1f; //spacer added for subsequent shotsperattack
    public bool swapAnimOnAttack;
    private int attackAnimState = 0;

    public float shakeTime;
    public float shakeStrength;
    public float shakeRotation;

    public Sprite weaponSprite;
    public bool isAutoAim;
    public GameObject AutoAim;

    public GameObject thrownWeapon;
    public Sprite thrownSprite;
    public float throwSpeed;
    private bool firstShot = true;

    public GameObject bulletCasing;
    public List<GameObject> MuzzleFlashPrefab;
    public float muzzleFlashXOffset;
    public float muzzleFlashYOffset;

    private void Update()
    {
        if (attackType == AttackTypes.Shotgun)
        {
            attackTime = 0 + (multicastTimes * multicastWaitTime);
        }
        else if (attackType == AttackTypes.Melee)
        {
            // Add the definition for Melee attack type
            attackTime = (comboLength - 1) * comboWaitTime + (spread * shotsPerAttack) + (multicastTimes * multicastWaitTime);
        }
        else
        {
            // Add the default definition for SingleShot attack type
            attackTime = spread * shotsPerAttack + (multicastTimes * multicastWaitTime);
        }
    }

    private void rollMulticast()
    {
        multicastTimes = 0; //reset
        //remember to hook up multicast as a player stat and add it here before rolling

        if (multicastChance == 0)
        {
            return;
        }
        else if (multicastChance < 1)
        {
            float roll = Random.Range(0f, 1f);

            if (roll >= multicastChance)
            {
                multicastTimes += 1;
            }


        } else if (multicastChance == 1)
        {
            multicastTimes += 1;
        }
        
        else if (multicastChance > 1)
        {
            int intMulticastChance = (int)multicastChance;
            multicastTimes += intMulticastChance;

            float roll = Random.Range(0f, 1f);
            float chance = multicastChance - intMulticastChance;
            if (roll >= multicastChance)
            {
                multicastTimes += 1;
            }

        }
    }

    private IEnumerator ShootSingleShot()
    {

        if (multicastTimes >= 1 && !firstShot)
        {
            yield return new WaitForSeconds(multicastWaitTime * numMulticast);
        }

        firstShot = false;
        if (multicastTimes > 0)
        {
            numMulticast++;
            if (numMulticast > multicastTimes)
            {
                numMulticast = 0;
                firstShot = true;
            }
        }

        if (cantMove == true)
        {
            //if (isAutoAim)
           // {
              //  AutoAim.SetActive(true);
            //} else
           // {
              //  AutoAim.SetActive(false);
           // }

            for (int i = 0; i < shotsPerAttack; i++)
            {
                SpawnMuzzleFlash();
                SpawnBulletCasing();
                Player.GetComponent<AttackHandler>().triggerRecoil();

                Quaternion rotation = owner.GetTransform().rotation;
                Vector3 position = owner.GetTransform().position;
                Vector3 direction = owner.GetDirection();
                Quaternion spreadDirection;

                Player.GetComponent<PlayerMovement>().StopMoving(); //stop moving before shooting

                if (shotsCount >= sprayThreshold) // calculate spray pattern
                {
                    float spread = spray * (shotsCount - sprayThreshold + 1);
                    float randomSpread = Random.Range(-spread, spread);
                    spreadDirection = Quaternion.Euler(0, 0, randomSpread);
                    rotation *= spreadDirection;
                }

                GameObject projectileGO = Instantiate(projectile, (position + direction / 2), Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = rotation;
                p.projectileRange = range;

                shotsCount += 1;
                yield return new WaitForSeconds(spread);
                
            }
            Player.GetComponent<PlayerMovement>().StartMoving();
        }
        else
        {

            float startTime = Time.time;
            for (int i = 0; i < shotsPerAttack; i++)
            {
                SpawnMuzzleFlash();
                SpawnBulletCasing();
                Player.GetComponent<AttackHandler>().triggerRecoil();

                Quaternion rotation = owner.GetTransform().rotation;
                Vector3 position = owner.GetTransform().position;
                Vector3 direction = owner.GetDirection();
                Quaternion spreadDirection;

                if (shotsCount >= sprayThreshold) // calculate spray pattern
                {
                    float spread = spray * (shotsCount - sprayThreshold + 1);
                    float randomSpread = Random.Range(-spread, spread);
                    spreadDirection = Quaternion.Euler(0, 0, randomSpread);
                    rotation *= spreadDirection;
                }

                GameObject projectileGO = Instantiate(projectile, (position + direction / 2), Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = rotation;
                p.projectileRange = range;

                shotsCount += 1;
                yield return new WaitForSeconds(spread);
            }
        }

    }

    private IEnumerator ShootShotgun()
    {
        float spacer = 0;
        float angle = 0;
        int shotsLeft = shotsPerAttack;
        spacer = spread / (shotsPerAttack - 1);
        Vector3 position = owner.GetTransform().position;
        Vector3 direction = owner.GetDirection();

        if (multicastTimes >= 1 && !firstShot)
        {
            yield return new WaitForSeconds(multicastWaitTime * numMulticast);
        }

        firstShot = false;
        if (multicastTimes > 0)
        {
            numMulticast++;
            if (numMulticast > multicastTimes)
            {
                numMulticast = 0;
                firstShot = true;
            }
        }


        if (cantMove == true)
        {
            SpawnMuzzleFlash();

            if (shotsPerAttack % 2 != 0)
            {
                SpawnBulletCasing();
                Player.GetComponent<PlayerMovement>().StopMoving();
                GameObject projectileGO = Instantiate(projectile, position + direction / 2, Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = owner.GetTransform().rotation;
            }
            else
            {
                spacer = spacer / 2;
            }
            for (int i = 0; i < (shotsLeft % 2 == 0 ? shotsPerAttack : shotsPerAttack - 1); i++)
            {
                if (i == 0 && shotsPerAttack % 2 == 0)
                {
                    angle = spacer / 2;
                }
                else if (i % 2 == 0)
                {
                    angle = Mathf.Abs(angle) + spacer;
                }
                else
                {
                    angle = -angle;
                }
                SpawnBulletCasing();

                Player.GetComponent<PlayerMovement>().StopMoving();
                GameObject projectileGO = Instantiate(projectile, position + direction / 2, Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = owner.GetTransform().rotation;
                p.transform.Rotate(new Vector3(0, 0, angle), Space.Self);

                

            }
            yield return null;

            Player.GetComponent<PlayerMovement>().StartMoving();

        }
        else
        {
            SpawnMuzzleFlash();

            if (shotsPerAttack % 2 != 0)
            {
                SpawnBulletCasing();
                GameObject projectileGO = Instantiate(projectile, position + direction / 2, Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = owner.GetTransform().rotation;
            }
            else
            {
                spacer = spacer / 2;
            }
            for (int i = 0; i < (shotsLeft % 2 == 0 ? shotsPerAttack : shotsPerAttack - 1); i++)
            {
                if (i == 0 && shotsPerAttack % 2 == 0)
                {
                    angle = spacer / 2;
                }
                else if (i % 2 == 0)
                {
                    angle = Mathf.Abs(angle) + spacer;
                }
                else
                {
                    angle = -angle;
                }
                SpawnBulletCasing();
                GameObject projectileGO = Instantiate(projectile, position + direction / 2, Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = owner.GetTransform().rotation;
                p.transform.Rotate(new Vector3(0, 0, angle), Space.Self);
            }
            yield return null;

        }

    }

    private IEnumerator Melee()
    {

        if (multicastTimes >= 1 && !firstShot)
        {
            yield return new WaitForSeconds(multicastWaitTime * numMulticast);
        }

        firstShot = false;
        if (multicastTimes > 0)
        {
            numMulticast++;
            if (numMulticast > multicastTimes)
            {
                numMulticast = 0;
                firstShot = true;
            }
        }

        float localSpacer = meleeSpacer;
        if (cantMove)
        {
            Player.GetComponent<PlayerMovement>().StopMoving();
        }

        Vector3 originalScale = MeleeAttack.transform.localScale;
        Vector3 scaler = new Vector3(meleeShotsScaleUp, meleeShotsScaleUp, meleeShotsScaleUp);

        float OGdamage = damage; //save original damage amount

        for (int i = 0; i < comboLength; i++)
        {
            Player.GetComponent<AttackHandler>().triggerWpnOff();
            Vector3 position = owner.GetTransform().position;
            Vector3 direction = owner.GetDirection();
            Quaternion rotation = owner.GetTransform().rotation;

            float perAttackScaling = 1 + (comboAttackBuff * i);
            damage *= perAttackScaling; //scale damage per shotInAttack


            //chain spawn 
            for (int c = 0; c < shotsPerAttack; c++)
            {
                    Vector3 directionSpacer = Vector3.Scale(direction, new Vector3(localSpacer, localSpacer, localSpacer));
                    GameObject projectileGO = Instantiate(MeleeAttack, position + directionSpacer, Quaternion.identity);
                    Projectile p = projectileGO.GetComponent<Projectile>();
                    p.attack = this;
                    p.transform.rotation = rotation;
                    if (c >= 1)
                    {
                        p.transform.localScale += scaler * c;
                    }

                    //change animation state 
                    if (swapAnimOnAttack)
                    {
                        Animator attackAnimator = p.GetComponent<Animator>();
                        switch (attackAnimState)
                        {
                            case 0:
                                attackAnimator.SetInteger("AttackCount", 0);
                                break;
                            case 1:
                                attackAnimator.SetInteger("AttackCount", 1);
                                break;
                            case 2:
                                attackAnimator.SetInteger("AttackCount", 2);
                                break;
                        }
                    }

                Camera.GetComponent<ScreenShakeController>().StartShake(shakeTime, shakeStrength, shakeRotation);
      
                yield return new WaitForSeconds(spread);

                // after one hit in the combo, do this
                if (meleeShotsScaleUp > 0)
                {
                    localSpacer += meleeSpacerGap * (1 + meleeShotsScaleUp);
                }
                else
                {
                    localSpacer += meleeSpacerGap;
                }

            }

            //reset weapon damage
            damage = OGdamage;

            //reset gap between hits
            localSpacer = meleeSpacer;
            
            //update attack state 
            attackAnimState++;
            if (attackAnimState == comboLength)
            {
                attackAnimState = 0;
            }

            //wait until next hit in combo
            yield return new WaitForSeconds(comboWaitTime);
        }

        if (cantMove)
        {
            //can move again
            Player.GetComponent<PlayerMovement>().StartMoving();
        }

    }


    // private IEnumerator Utility() //buff effect on self
    // {

    //  }

    public void Shoot()
    {
        firstShot = true;
        rollMulticast();

        switch (attackType)
        {
            case AttackTypes.SingleShot:
                for (int i = 0; i < (multicastTimes + 1); i++)
                {
                    shotsCount = 0;
                    StartCoroutine(ShootSingleShot());
                }
                break;
            case AttackTypes.Shotgun:
                for (int i = 0; i < (multicastTimes + 1); i++)
                {
                    StartCoroutine(ShootShotgun());
                }
                break;
            case AttackTypes.Melee:
                for (int i = 0; i < (multicastTimes + 1); i++)
                {
                    StartCoroutine(Melee());
                }
                break;
            //case AttackTypes.Utility:
            //StartCoroutine(Utility());
            //break;
            default:
                break;
        }

    }

    public void ThrowWeapon()
    {
        if (thrownWeapon == null)
        {
            return;
        }
        else
        {
            Quaternion rotation = owner.GetTransform().rotation;
            Vector3 position = owner.GetTransform().position;
            Vector3 direction = owner.GetDirection();


            GameObject wpnToss = Instantiate(thrownWeapon, position, Quaternion.identity);
            wpnToss.GetComponent<SpriteRenderer>().sprite = thrownSprite;
            Rigidbody2D rb = wpnToss.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * throwSpeed * -1, ForceMode2D.Impulse);
            rb.AddTorque(1000f);

            Projectile p = wpnToss.GetComponent<Projectile>();
            p.attack = this;
            p.transform.rotation = rotation;
        }

    }

    public void SpawnMuzzleFlash()
    {
        if (MuzzleFlashPrefab.Count == 0)
        {
            return;
        }

        // Select a random MuzzleFlashPrefab from the list
        GameObject selectedPrefab = MuzzleFlashPrefab[Random.Range(0, MuzzleFlashPrefab.Count)];
        // Instantiate the selected MuzzleFlashPrefab at the specified position
        Vector3 spawnPosition = transform.position + new Vector3(muzzleFlashXOffset, muzzleFlashYOffset, 0f);

        GameObject MuzzleFlash = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

        Quaternion rotation = owner.GetTransform().rotation;
        MuzzleFlash.transform.rotation = rotation;
    }

public void SpawnBulletCasing()
    {
        if (bulletCasing == null)
        {
            return;
        } else
        {
            // Calculate random position modifiers
            float xModifier = Random.Range(-0.075f, 0.075f);
            float yModifier = Random.Range(-0.05f, 0.1f);

            // Calculate position for the new object
            Vector3 spawnPosition = transform.position + new Vector3(xModifier + 0.5f, yModifier - 0.1f, 0f);

            // Instantiate the new object
            GameObject newBulletCasing = Instantiate(bulletCasing, spawnPosition, Quaternion.identity);

        }
    }


    // Start is called before the first frame update
    void Start()
    {

        if (projectile == null)
        {
            projectile = Resources.Load("Prefabs/BasicProjectile", typeof(GameObject)) as GameObject;
        }
        owner = transform.GetComponentInParent<Attacker>();

        int y = rarity;
        GenerateRarity(y, 1, 5);


        if (chosenNumbers.Contains(1)) //Upgrade Type 1 - Damage
        {
            damage = damageUP;
        }

        if (chosenNumbers.Contains(2)) //Upgrade Type 2 - spread /+ shotsPerAttack
        {
            spread = spreadUP;
            shotsPerAttack = shotsPerAttackUP;
        }
        if (chosenNumbers.Contains(3)) //Upgrade Type 3 - castTime /+ startTime
        {
            castTime = castTimeUP;
            recoveryTime = recoveryTimeUp;
        }
        if (chosenNumbers.Contains(4)) //Upgrade Type 4 - Range /+ speed
        {
            range = rangeUP;
            speed = speedUP;
        }
        if (chosenNumbers.Contains(5)) //Upgrade Type 5 - Knockback
        {
            knockback = knockbackUP;
        }
        if (chosenNumbers.Contains(6)) //Upgrade Type 6 - Scale  
        {
            //projectile.transform.localScale = scaleUP;
        }

        Camera = GameObject.FindWithTag("MainCamera");
        Player = GameObject.FindWithTag("Player");
        defaultMulticastWaitTime = multicastWaitTime;

    }

    public List<int> GenerateRarity(int count, int minValue, int maxValue)
    {
        List<int> possibleNumbers = new List<int>();

        for (int index = minValue; index <= maxValue; index++)
            possibleNumbers.Add(index);


        while (chosenNumbers.Count < count)
        {
            int position = Random.Range(0, possibleNumbers.Count);
            chosenNumbers.Add(possibleNumbers[position]);
            possibleNumbers.RemoveAt(position);

            foreach (int value in chosenNumbers)
            {
            }
        }
        return chosenNumbers;


    }



    public Transform GetTransform()
    {
        return transform;
    }
    public UpgradeType GetUpgradeType()
    {
        return UpgradeType.Weapon;
    }

}
