using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [Header("Gun Options")]
    public float rateOfFire = 0.5f;
    public float t_rateOfFire = 0.5f;
    public float damage = 30f;
    public float t_ReloadTime = 3f;
    public int ammo = 5;
    public int ammoCapacity = 5;
    public Sounds.SoundName Sound_GunShot;

    [Header("Particle Effects")]
    public ParticleSystem Gun_Shot;
    public ParticleSystem Gun_Smoke;

    [Header("Bullet Pooling")]
    public int BULLET_POOL_SIZE;
    public List<GameObject> Bullets;

    [Header("Gun Set-Up")]
    public GameObject Prefab_Bullet;
    public Transform Emitter;

    [Header("Reload UI")]
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

    private void Start()
    {
        player = GameObject.Find("Player");
        for (int i = 0; i < BULLET_POOL_SIZE; i++)
        {
            // Spawn in a specified bullets
            GameObject myInstance = Instantiate<GameObject>(Prefab_Bullet);
            myInstance.SetActive(false);
            Bullets.Add(myInstance);
            myInstance.GetComponent<Bullet>().Damage = damage;
            myInstance.GetComponent<Bullet>().ID = RT_PNum.Substring(RT_PNum.Length - 1);

        }
    }
}
