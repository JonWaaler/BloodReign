using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    [Header("Gun Firing Options")]
    public float RateOfFire = 0.5f;
    public float t_RateOfFireTimer = 0.5f;  // Should keep the timer init to  the val of Rate
    public float Recoil = 10f;              //  OfFire so you dont have to wait for the timer
    public float Damage = 10f;
    public float TimeToReload = 3f;
    public int BulletsInMag = 15;       // The amount of bullets currently in mag
    public int MagazineCapacity = 15;   // How many bullets the mag can hold
    public Sounds.SoundName Sound_GunShot;

    [Header("SHOTGUN")]
    [TextArea]
    public string subtitle = "NOTE: Shotgun requires larger bullet pool";
    public bool isShotGun = false;
    public int minBullets = 4;
    public int maxBullets = 10;

    [Header("ROCKET")]
    public bool isRocket = false;
    public string H_RS_PNum, V_RS_PNum;

    [Header("CHARGE")]
    public bool isChargeGun = false;
    public float chargeTime = 1;
    private float t_chargeTime = 0;
    public float percentageDmgReduction = .25f;
    public GameObject p_ChargeSystem;

    [Header("Particle Effects")]
    public ParticleSystem Gun_Shot; //When a bullet is shot sparks
    public ParticleSystem Gun_Smoke;// After a bullet is shot smoke, or when reloading...

    [HideInInspector] // Bullet Pooling
    public bool isShooting = false;

    [Header("Bullet Pooling")]
    public int BULLET_POOL_SIZE;        // How many bullets to pool for this weapon
    public List<GameObject> Bullets;    // A "dynamic" array type for the bullets

    [Header("Gun Set-Up")]
    public GameObject Prefab_Bullet;    // This is a copy of our bullet
    public Transform Emitter;           // Emitter is the spawn point of the bullet

    [Header("Double Damage Set-up")]
    public GameObject bullet_Doubledmg;
    public List<GameObject> Bullets_DD;    // A "dynamic" array type for the Double Damage bullets
    private bool useDoubleDamage = false;

    [Header("ReloadUI")]
    public UnityEngine.UI.Slider Slider_Reload;


    public AbilitySettings abilitySettings;

    [Header("Ammo - UI")]
    public Transform ammo_parent;
    public GameObject ammo_shotUI;

    private GameObject player;
    private bool requestReload = false;
    [HideInInspector]
    public float t_Reload = 0;

    public string RT_PNum;
    public string RB_PNum;
    public string xButton_PNum;
    public SoundManager soundManager;
    public PlayerSettings playerSettings;
    private string tempReloading_Str;

    void Start()
    {
        player = GameObject.Find("Player");
        
        for (int i = 0; i < BULLET_POOL_SIZE; i++)
        {
            // Spawn in a specified bullets
            GameObject myInstance = Instantiate<GameObject>(Prefab_Bullet);
            myInstance.SetActive(false);
            Bullets.Add(myInstance);
            myInstance.GetComponent<Bullet>().Damage = Damage;
            myInstance.GetComponent<Bullet>().ID = RT_PNum.Substring(RT_PNum.Length-1);

            //Double Dmg Instances
            if (bullet_Doubledmg != null)
            {
                GameObject myDDInstance = Instantiate<GameObject>(bullet_Doubledmg);
                myDDInstance.SetActive(false);
                Bullets_DD.Add(myDDInstance);
                myDDInstance.GetComponent<Bullet>().Damage = Damage;
                myDDInstance.GetComponent<Bullet>().ID = RT_PNum.Substring(RT_PNum.Length - 1);
            }
            else
                Debug.LogError("Player:" + gameObject.name + "   does not have double damage bullet", gameObject);
        }

        for (int i = 0; i < MagazineCapacity; i++)
        {
            Instantiate(ammo_shotUI).transform.SetParent(ammo_parent);
        }
    }

    GameObject chargeParticles_ref;
    void Update()
    { 
        if(transform.parent.GetComponent<Player>().activeState == PlayerState.dead)
        {
            BulletsInMag = MagazineCapacity;
            Slider_Reload.transform.parent.position = transform.parent.position + Vector3.up * 2;

            return;
        }

        if (chargeParticles_ref != null)
        {
            chargeParticles_ref.transform.position = transform.position;
        }
        if ((Input.GetButtonDown(RB_PNum)))
        {
            isShooting = true;
            if (isChargeGun)
            {
                p_ChargeSystem.SetActive(true);

                if (chargeParticles_ref == null)
                {
                    if ((t_RateOfFireTimer >= RateOfFire) && (BulletsInMag > 0) && !requestReload)
                    {
                        chargeParticles_ref = Instantiate(p_ChargeSystem);
                        chargeParticles_ref.transform.position = transform.position;
                        Destroy(chargeParticles_ref, 1);
                    }
                    else
                    {
                        isShooting = false;
                    }
                }
            }
        }
        else if ((Input.GetButtonUp(RB_PNum)) && isShooting)
        {
            isShooting = false;
            //Destroy(chargeParticles_ref);

            if (isChargeGun && (t_RateOfFireTimer >= RateOfFire) && (BulletsInMag > 0) && !requestReload)
            {
                if (!useDoubleDamage)
                {
                    for (int i = 0; i < BULLET_POOL_SIZE; i++)
                    {
                        if (Bullets[i].activeInHierarchy == false)
                        {
                            InvisParticles();

                            ShootBullet(Bullets, i, 1);
                            t_chargeTime = 0;
                            t_RateOfFireTimer = 0;
                            soundManager.Play(Sound_GunShot);
                            //p_ChargeSystem.GetComponent<ParticleSystem>().Clear();
                            //p_ChargeSystem.SetActive(false);
                            Destroy(chargeParticles_ref);


                            return;
                        }
                    }
                }
                else if (useDoubleDamage)
                {
                    for (int i = 0; i < BULLET_POOL_SIZE; i++)
                    {
                        if (Bullets_DD[i].activeInHierarchy == false)
                        {
                            InvisParticles();

                            ShootBullet(Bullets_DD, i, 2);
                            t_chargeTime = 0;
                            t_RateOfFireTimer = 0;
                            soundManager.Play(Sound_GunShot);
                            //p_ChargeSystem.GetComponent<ParticleSystem>().Clear();
                            //p_ChargeSystem.SetActive(false);
                            if (chargeParticles_ref != null)
                                Destroy(chargeParticles_ref);

                            return;
                        }
                    }
                }
            }
        }

        if (isShooting && BulletsInMag == 0)
        {
            if (requestReload == false)
            {
                transform.parent.GetComponent<RumblePack>().addRumbleTimerL(0.30f, 0.1f);
                transform.parent.GetComponent<RumblePack>().addRumbleTimerL(0.20f, 0.1f);
                transform.parent.GetComponent<RumblePack>().addRumbleTimerL(0.10f, 0.1f);
            }
            requestReload = true;
            soundManager.Play(Sounds.SoundName.Reload_Shotgun);            
        }

        if ((isShooting) && (t_RateOfFireTimer >= RateOfFire) && (BulletsInMag > 0) && !requestReload)
        {
            //Then search through bullet list and fire the first inactive
            if (isShotGun)
            {
                int sprayAmount = Random.Range(minBullets, maxBullets);
                int foundCounter = 0;
                soundManager.Play(Sound_GunShot);

                // Shooting
                if (!useDoubleDamage)
                {
                    for (int i = 0; i < BULLET_POOL_SIZE; i++)
                    {
                        if (Bullets[i].activeInHierarchy == false)
                        {

                            ShootBullet(Bullets, i, 1);
                            foundCounter++;
                            

                            t_RateOfFireTimer = 0; // Reset ROF timer

                            // Check if we've shot required bullets
                            if (foundCounter >= sprayAmount)
                            {
                                InvisParticles();
                                BulletsInMag--;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < BULLET_POOL_SIZE; i++)
                    {
                        if (Bullets_DD[i].activeInHierarchy == false)
                        {


                            ShootBullet(Bullets, i, 2);

                            foundCounter++;

                            t_RateOfFireTimer = 0; // Reset ROF timer

                            // Check if we've shot required bullets
                            if (foundCounter >= sprayAmount)
                            {
                                InvisParticles();
                                BulletsInMag--;
                                return;
                            }
                        }
                    }
                }
                
            }


            if (!isChargeGun)
            {
                for (int i = 0; i < BULLET_POOL_SIZE; i++)
                {
                    if (!useDoubleDamage)
                    {
                        if (Bullets[i].activeInHierarchy == false)
                        {
                            InvisParticles();


                            ShootBullet(Bullets, i, 1);

                            BulletsInMag--;

                            soundManager.Play(Sound_GunShot);
                            t_RateOfFireTimer = 0; // Reset ROF timer
                            return;
                        }
                    }
                    else
                    {
                        if (Bullets_DD[i].activeInHierarchy == false)
                        {
                            InvisParticles();

                            ShootBullet(Bullets_DD, i, 2);

                            BulletsInMag--;

                            soundManager.Play(Sound_GunShot);
                            t_RateOfFireTimer = 0; // Reset ROF timer
                            return;
                        }
                    }
                }
            }
            else if(isChargeGun)
            {
                // Play Charge Sound
                if(chargeParticles_ref != null)
                {
                    chargeParticles_ref.transform.position = transform.position;

                }
                t_chargeTime += Time.deltaTime; // Increment time
            }
        }

        if (isRocket)
        {
            if (!useDoubleDamage)
            {
                for (int i = 0; i < BULLET_POOL_SIZE; i++)
                    Bullets[i].transform.eulerAngles = new Vector3(0, Mathf.Atan2(Input.GetAxisRaw(V_RS_PNum), Input.GetAxisRaw(H_RS_PNum)) * Mathf.Rad2Deg + 90, 0);
            }
            else
            {
                for (int i = 0; i < BULLET_POOL_SIZE; i++)
                    Bullets_DD[i].transform.eulerAngles = new Vector3(0, Mathf.Atan2(Input.GetAxisRaw(V_RS_PNum), Input.GetAxisRaw(H_RS_PNum)) * Mathf.Rad2Deg + 90, 0);
            }
        }

        if ((Input.GetButtonDown(xButton_PNum)) && (!requestReload))
        {
            // Play Reload Sound
            // Also play a reload graphic on screen
            transform.parent.GetComponent<RumblePack>().addRumbleTimerL(0.10f, 0.1f);
            transform.parent.GetComponent<RumblePack>().addRumbleTimerL(0.20f, 0.1f);
            
            tempReloading_Str = xButton_PNum;
            //print("This: " + tempReloading_Str);
            soundManager.Play(Sounds.SoundName.Reload_Shotgun);

            requestReload = true;
        }

        Slider_Reload.transform.parent.position = transform.parent.position + Vector3.up *2;

        if ((requestReload))
        {
            t_Reload += Time.deltaTime;
            // Reload UI
            Slider_Reload.gameObject.SetActive(true);
            Slider_Reload.value = Mathf.Lerp(0, 1, t_Reload / TimeToReload);

            if(t_Reload > TimeToReload)
            {
                //print("Reloaded GM: " + gameObject.transform.parent.gameObject.name);
                BulletsInMag = MagazineCapacity;
                requestReload = false;
                t_Reload = 0;
                Slider_Reload.value = 0;
                Slider_Reload.gameObject.SetActive(false);

                transform.parent.GetComponent<RumblePack>().addRumbleTimerL(0.30f, 0.1f);
                transform.parent.GetComponent<RumblePack>().addRumbleTimerL(0.20f, 0.1f);
                transform.parent.GetComponent<RumblePack>().addRumbleTimerL(0.10f, 0.1f);
            }
        }



        // Update Ammo UI
        for (int i = 0; i < MagazineCapacity; i++)
        {
            if(i <= BulletsInMag - 1)
            {
                // Should be active
                ammo_parent.GetChild(i).gameObject.SetActive(true);
            }
            else
                ammo_parent.GetChild(i).gameObject.SetActive(false);
        }

        t_RateOfFireTimer += Time.deltaTime;
    }

    // The timer increments when the shoot button is held down
    // Called on fire, takes in timer, recalculates dmg
    private float ChargeUp(float timer)
    {
        /* [Header("CHARGE")]
         * public bool isChargeGun = false;
         * public float chargeTime = 1;
         * private float t_chargeTime = 0;
         */

        if (!isChargeGun) // If not charge gun, dont change dmg
        {
            return 1;
        }

        if(timer >= chargeTime)
        {
            // return regular dmg
            return 1;
        }
        else
        {

            // Calculate smaller dmg
            return Mathf.Lerp(percentageDmgReduction, 1, timer/RateOfFire);
        }
    }

    private void ShootBullet(List<GameObject> bulletPool, int index, int dmgMultiplier)
    {
        transform.parent.GetComponent<RumblePack>().addRumbleTimerL(0.10f, 0.5f);
        transform.parent.GetComponent<RumblePack>().addRumbleTimerH(0.10f, 0.02f);
        bulletPool[index].transform.position = Emitter.position;
        bulletPool[index].SetActive(true);
        float randomAngle = Random.Range(Recoil, -Recoil);
        bulletPool[index].transform.eulerAngles = gameObject.transform.parent.transform.eulerAngles - new Vector3(0, randomAngle, 0);
        bulletPool[index].GetComponent<Bullet>().Damage = Damage * dmgMultiplier * ChargeUp(t_chargeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DoubleDamage")
        {
            useDoubleDamage = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DoubleDamage")
        {
            useDoubleDamage = false;

        }
    }


    private void InvisParticles()
    {
        // Particles while invis
        if (!transform.GetChild(2).gameObject.activeSelf)
        {
            GameObject invisPartIns_New = Instantiate(abilitySettings.invisPart);
            invisPartIns_New.transform.position = transform.position;
            Destroy(invisPartIns_New, 4f);
        }
    }
}
