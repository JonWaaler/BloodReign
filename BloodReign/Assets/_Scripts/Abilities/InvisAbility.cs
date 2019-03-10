using System.Collections;
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
        StartCoroutine(Invisible(lerpSpd, abilLength, transform.gameObject));
    }
    private IEnumerator Invisible(float easeInOut, float duration, GameObject affectedObj) // ( timeToActivate, timeToLast, player's gun)
    {
        float current = 0.0f; // ElapsedTime

        // Skip the guns and get to the armature or w.e
        GameObject childBody = transform.GetChild(4).gameObject;
        // Any Child with skinnedMeshRenderer under the parent will have it disabled
        SkinnedMeshRenderer[] bodyParts = childBody.GetComponentsInChildren<SkinnedMeshRenderer>(true);
        // Ease in to invisiblity
        while (current <= easeInOut)
        {   // if there are multiple parts in the dumb model
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
            else // the model is nicely made with 1 obj
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
        // Keep track of which gun I will disable to re-enable later
        current = 0.0f;
        int activeGun = 0;
        for (int i = 1; i < transform.childCount-1; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                activeGun = i;
            }
        }
        // Disable gun model
        GameObject child = transform.GetChild(activeGun).gameObject;
        child.GetComponent<Renderer>().enabled = false;
 
        // Duration for invisibility
        while (current <= duration)
        {
            current = current + Time.deltaTime;
            yield return null;
        }
        // Ease-in to Visible
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
