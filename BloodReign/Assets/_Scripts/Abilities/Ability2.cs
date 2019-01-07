using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability2 : AbilityCommand
{

    Ability2()
    {
        abilCool = 5.0f;
        abilLength = 4.0f;
        lerpSpd = 0.5f;
    }
    public override void AbilityExcecution()
    {
        activate();
    }
    private void activate()
    {
       // if (Input.GetButtonDown(abilButton) && Time.time > nextAbil)
        {
            // set time for when next use of ability available
         //   nextAbil = Time.time + abilCool;
            GameObject child = transform.GetChild(0).gameObject;
            StartCoroutine(Invisible(lerpSpd, abilLength, child));
        }
    }
    private IEnumerator Invisible(float easeInOut, float duration, GameObject affectedObj) // ( timeToActivate, timeToLast, player's gun)
    {
        float current = 0.0f; // ElapsedTime
        Renderer[] bodyParts = new Renderer[0];
        if (affectedObj.transform.childCount > 0)
        {
            bodyParts = GetComponentsInChildren<Renderer>();
        }
        // Ease in to invisiblity
        while (current <= easeInOut)
        {
            if (affectedObj.transform.childCount > 0)
            {
                foreach (Renderer limb in bodyParts)
                {
                    current = current + Time.deltaTime;
                    float percent = Mathf.Clamp01(current / easeInOut);
                    Color alpha = affectedObj.GetComponent<Renderer>().material.color;
                    alpha.a = 1 - percent;
                    limb.material.color = alpha;
                }
            }
            else
            {
                current = current + Time.deltaTime;
                float percent = Mathf.Clamp01(current / easeInOut);
                Color alpha = affectedObj.GetComponent<Renderer>().material.color;
                alpha.a = 1 - percent;
                affectedObj.GetComponent<Renderer>().material.color = alpha;
            }
            yield return null;
        }
        current = 0.0f;
        int activeGun = 0;
        for (int i = 1; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                activeGun = i;
            }
        }
        GameObject child = transform.GetChild(activeGun).gameObject;
        affectedObj.GetComponent<Renderer>().enabled = false;
        child.GetComponent<Renderer>().enabled = false;
        // Duration for invisibility
        while (current <= duration)
        {
            current = current + Time.deltaTime;
            yield return null;
        }
        // Ease back to Visible
        affectedObj.GetComponent<Renderer>().enabled = true;
        child.GetComponent<Renderer>().enabled = true;
        current = 0.0f;
        while (current <= easeInOut)
        {
            if (affectedObj.transform.childCount > 0)
            {
                foreach (Renderer limb in bodyParts)
                {
                    current = current + Time.deltaTime;
                    float percent = Mathf.Clamp01(current / easeInOut);
                    Color alpha = affectedObj.GetComponent<Renderer>().material.color;
                    alpha.a = percent;
                    limb.material.color = alpha;
                }
            }
            else
            {
                current = current + Time.deltaTime;
                float percent = Mathf.Clamp01(current / easeInOut);
                Color alpha = affectedObj.GetComponent<Renderer>().material.color;
                alpha.a = percent;
                affectedObj.GetComponent<Renderer>().material.color = alpha;
            }
            yield return null;
        }
    }
}
