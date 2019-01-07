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
    public string Sound_GunShot = "Sniper Shot";

    [Header("SHOTGUN")]
    [TextArea]
    public string subtitle = "NOTE: Shotgun requires larger bullet pool";
    public bool isShotGun = false;
    public int minBullets = 4;
    public int maxBullets = 10;

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

    [Header("ReloadUI")]
    public UnityEngine.UI.Slider Slider_Reload;

    private GameObject player;
    private bool requestReload = false;
    [HideInInspector]
    public float t_Reload = 0;

    public string RT_PNum;
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

        }
    }


    void Update()
    {
        if (Input.GetAxisRaw(RT_PNum) > 0.5)
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;

            // Play shot smoke particles
        }

        if ((isShooting) && (t_RateOfFireTimer >= RateOfFire) && (BulletsInMag > 0) && !requestReload)
        {
            //Then search through bullet list and fire the first inactive
            if (isShotGun)
            {
                int sprayAmount = Random.Range(minBullets, maxBullets);
                int foundCounter = 0;
                soundManager.Play(Sound_GunShot);
                for (int i = 0; i < BULLET_POOL_SIZE; i++)
                {
                    if (Bullets[i].activeInHierarchy == false)
                    {
                        foundCounter++;
                        Bullets[i].SetActive(true);
                        Bullets[i].transform.position = Emitter.position;
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


            for (int i = 0; i < BULLET_POOL_SIZE; i++)
            {
                if (Bullets[i].activeInHierarchy == false)
                {
                    // Play shot sound
                    
                    soundManager.Play(Sound_GunShot);
                    // Play shot anim
                    // Play shot particles
                    Bullets[i].SetActive(true);
                    Bullets[i].transform.position = Emitter.position;
                    // Recoil
                    float randomAngle = Random.Range(Recoil, -Recoil);
                    Bullets[i].transform.eulerAngles = gameObject.transform.parent.transform.eulerAngles - new Vector3(0, randomAngle, 0);
                    Bullets[i].GetComponent<Bullet>().Damage = Damage;
                    BulletsInMag--;

                    t_RateOfFireTimer = 0; // Reset ROF timer
                    return;
                }
            }
        }

        if ((Input.GetButtonDown(xButton_PNum)) && (!requestReload))
        {
            // Play Reload Sound
            // Also play a reload graphic on screen

            tempReloading_Str = xButton_PNum;
            //print("This: " + tempReloading_Str);
            soundManager.Play("Shotgun Reload");
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
}
