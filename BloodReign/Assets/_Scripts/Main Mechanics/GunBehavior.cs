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
    }


    void Update()
    {
        if(transform.parent.GetComponent<Player>().activeState == PlayerState.dead)
        {
            BulletsInMag = MagazineCapacity;
            Slider_Reload.transform.parent.position = transform.parent.position + Vector3.up * 2;

            return;
        }

        //print("RT1 : " + Input.GetAxisRaw("RT1"));
        //print("RT2 : " + Input.GetAxisRaw("RT2"));
        //print("LT1 : " + Input.GetAxisRaw("LT1"));
        //print("LT2 : " + Input.GetAxisRaw("LT2"));
        //for (int i = 0; i < 20; i++)
        //{
        //    if (Input.GetKeyDown("joystick 1 button " + i)) { print("joystick 1 button " + i); }
        //    if (Input.GetKeyDown("joystick 2 button " + i)) { print("joystick 2 button " + i); }
        //}

        if ((Input.GetButton(RB_PNum)))
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;

            // Play shot smoke particles
        }

        if (isShooting && BulletsInMag == 0)
        {
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
                            foundCounter++;
                            Bullets[i].transform.position = Emitter.position;
                            Bullets[i].SetActive(true);
                            float randomAngle = Random.Range(Recoil, -Recoil);
                            Bullets[i].transform.eulerAngles = gameObject.transform.parent.transform.eulerAngles - new Vector3(0, randomAngle, 0);
                            Bullets[i].GetComponent<Bullet>().Damage = Damage;

                            t_RateOfFireTimer = 0; // Reset ROF timer

                            // Check if we've shot required bullets
                            if (foundCounter >= sprayAmount)
                            {
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
                            foundCounter++;
                            Bullets_DD[i].transform.position = Emitter.position;
                            Bullets_DD[i].SetActive(true);
                            float randomAngle = Random.Range(Recoil, -Recoil);
                            Bullets_DD[i].transform.eulerAngles = gameObject.transform.parent.transform.eulerAngles - new Vector3(0, randomAngle, 0);
                            Bullets_DD[i].GetComponent<Bullet>().Damage = Bullets[i].GetComponent<Bullet>().Damage * 2; // Double damage

                            t_RateOfFireTimer = 0; // Reset ROF timer

                            // Check if we've shot required bullets
                            if (foundCounter >= sprayAmount)
                            {
                                BulletsInMag--;
                                return;
                            }
                        }
                    }
                }


            }
            if (!useDoubleDamage)
            {
                for (int i = 0; i < BULLET_POOL_SIZE; i++)
                {
                    if (Bullets[i].activeInHierarchy == false)
                    {
                        soundManager.Play(Sound_GunShot);

                        Bullets[i].transform.position = Emitter.position;
                        Bullets[i].SetActive(true);
                        // Recoil
                        float randomAngle = Random.Range(Recoil, -Recoil);
                        Bullets[i].transform.eulerAngles = gameObject.transform.parent.transform.eulerAngles - new Vector3(0, randomAngle, 0);
                        //Debug.Log(gameObject.transform.parent.transform.eulerAngles - new Vector3(0, randomAngle, 0));
                        Bullets[i].GetComponent<Bullet>().Damage = Damage;
                        BulletsInMag--;

                        t_RateOfFireTimer = 0; // Reset ROF timer
                        return;
                    }
                }
            }
            else
            {
                for (int i = 0; i < BULLET_POOL_SIZE; i++)
                {
                    if (Bullets_DD[i].activeInHierarchy == false)
                    {
                        soundManager.Play(Sound_GunShot);

                        Bullets_DD[i].transform.position = Emitter.position;
                        Bullets_DD[i].SetActive(true);
                        // Recoil
                        float randomAngle = Random.Range(Recoil, -Recoil);
                        Bullets_DD[i].transform.eulerAngles = gameObject.transform.parent.transform.eulerAngles - new Vector3(0, randomAngle, 0);
                        //Debug.Log(gameObject.transform.parent.transform.eulerAngles - new Vector3(0, randomAngle, 0));
                        Bullets_DD[i].GetComponent<Bullet>().Damage = Bullets[i].GetComponent<Bullet>().Damage * 2; // Get Regular bullet value and *2 for Double damage
                        BulletsInMag--;

                        t_RateOfFireTimer = 0; // Reset ROF timer
                        return;
                    }
                }
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
            }
        }

        t_RateOfFireTimer += Time.deltaTime;
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
}
