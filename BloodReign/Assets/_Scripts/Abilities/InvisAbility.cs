﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisAbility : AbilityCommand
{
    public InvisAbility()
    {
    }
    public override void AbilityExcecution()
    {
        activate();
    }
    public override void ResetSphere()
    {
    }
    private void Start()
    {


    }
    private void activate()
    {
       // if (Input.GetButtonDown(abilButton) && Time.time > nextAbil)
        {
            // set time for when next use of ability available
         //   nextAbil = Time.time + abilCool;
            StartCoroutine(Invisible(lerpSpd, abilLength, transform.gameObject));
        }
    }
    private IEnumerator Invisible(float easeInOut, float duration, GameObject affectedObj) // ( timeToActivate, timeToLast, player's gun)
    {
        float current = 0.0f; // ElapsedTime
        GameObject childBody = transform.GetChild(3).gameObject;

        SkinnedMeshRenderer[] bodyParts = childBody.GetComponentsInChildren<SkinnedMeshRenderer>(true);
        // Ease in to invisiblity
        while (current <= easeInOut)
        {
            if (affectedObj.transform.childCount > 0)
            {
                foreach (SkinnedMeshRenderer limb in bodyParts)
                {
                    current = current + Time.deltaTime;
                    float percent = Mathf.Clamp01(current / easeInOut);
                    Color alpha = limb.GetComponent<SkinnedMeshRenderer>().material.color;
                    alpha.a = 1 - percent;
                    limb.material.color = alpha;
                    limb.GetComponent<SkinnedMeshRenderer>().enabled = false;

                }
            }
            else
            {
                current = current + Time.deltaTime;
                float percent = Mathf.Clamp01(current / easeInOut);
                Color alpha = affectedObj.GetComponent<SkinnedMeshRenderer>().material.color;
                alpha.a = 1 - percent;
                affectedObj.GetComponent<SkinnedMeshRenderer>().material.color = alpha;
                affectedObj.GetComponent<SkinnedMeshRenderer>().enabled = false;
            }
            yield return null;
        }
        current = 0.0f;
        int activeGun = 0;
        for (int i = 1; i < transform.childCount-1; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                activeGun = i;
            }
        }
        GameObject child = transform.GetChild(activeGun).gameObject;
        //        affectedObj.GetComponent<SkinnedMeshRenderer>().enabled = false;
        child.GetComponent<Renderer>().enabled = false;
        // Duration for invisibility
        while (current <= duration)
        {
            current = current + Time.deltaTime;
            yield return null;
        }
        // Ease back to Visible
//        affectedObj.GetComponent<SkinnedMeshRenderer>().enabled = true;
        child.GetComponent<Renderer>().enabled = true;
        current = 0.0f;
        while (current <= easeInOut)
        {
            if (affectedObj.transform.childCount > 0)
            {
                foreach (SkinnedMeshRenderer limb in bodyParts)
                {
                    current = current + Time.deltaTime;
                    float percent = Mathf.Clamp01(current / easeInOut);
                    Color alpha = limb.GetComponent<SkinnedMeshRenderer>().material.color;
                    alpha.a = percent;
                    limb.material.color = alpha;
                    limb.GetComponent<SkinnedMeshRenderer>().enabled = true;
                }
            }
            else
            {
                current = current + Time.deltaTime;
                float percent = Mathf.Clamp01(current / easeInOut);
                Color alpha = affectedObj.GetComponent<SkinnedMeshRenderer>().material.color;
                alpha.a = percent;
                affectedObj.GetComponent<SkinnedMeshRenderer>().material.color = alpha;
                affectedObj.GetComponent<SkinnedMeshRenderer>().enabled = true;
            }
            yield return null;
        }
    }
}
