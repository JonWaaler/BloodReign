using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DummyDamage : MonoBehaviour
{

    public Slider health;
    public GameObject canvas_health;
    private Transform canvas_health_T;
    public Transform dummy;

    void Start()
    {
        canvas_health_T = Instantiate(canvas_health).transform;
    }

    private void Update()
    {
        health.value += Time.deltaTime * 25f;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            //Debug.Log(other.gameObject.activeSelf);
            other.gameObject.SetActive(true);
            if (other.GetComponent<Bullet>())
            {
                health.value -= other.GetComponent<Bullet>().Damage;

            }
            else
            {
                other.gameObject.AddComponent<Bullet>();
                health.value -= other.GetComponent<Bullet>().Damage;

            }
            other.gameObject.SetActive(false);

        }
    }
}
