using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InvisAbility : AbilityCommand
{
    // Spawn Particle System
    [SerializeField]
    private GameObject invisPartIns = null;

    public InvisAbility()
    {
        if (abilSettings != null)
            abilCool = abilSettings.abilCool_4;
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
        StartCoroutine(Invisible(abilSettings.lerpSpd_4, abilSettings.abilLength_4, transform.gameObject));
    }
    private IEnumerator Invisible(float easeInOut, float duration, GameObject affectedObj) // ( timeToActivate, timeToLast, player's gun)
    {
        float current = 0.0f; // ElapsedTime

        // Skip the guns and get to the armature or w.e
        GameObject childBody = transform.GetChild(4).gameObject;
        // Any Child with skinnedMeshRenderer under the parent will have it disabled
        SkinnedMeshRenderer[] bodyParts = childBody.GetComponentsInChildren<SkinnedMeshRenderer>(true);
        // Ease in to invisiblity

        // Spawn Particle System
        if (invisPartIns == null)
            invisPartIns = Instantiate(abilSettings.invisPart);
        // Place System
        invisPartIns.transform.position = transform.position;
        //        invisPartIns.GetComponent<ParticleSystem>().Play();

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
        for (int i = 1; i < transform.childCount - 1; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                activeGun = i;
            }
        }
        // Disable Everything

        // Set a destroy for system
        Destroy(invisPartIns, 1);

        // Disable gun model
        GameObject child = transform.GetChild(activeGun).gameObject;
        child.GetComponent<Renderer>().enabled = false;
        child.GetComponentInChildren<LineRenderer>(true).enabled = false;
        Player player_script = GetComponent<Player>();
        player_script.elementRef.gameObject.SetActive(false);
        WinDetection windection = GetComponent<WinDetection>();
        windection.slider_PlayerHealth.gameObject.SetActive(false);

        GameObject gunsThingy = player_script.elementRef.GetComponent<Element_FireAnimation>().gunBehavior.transform.GetChild(2).gameObject;
        gunsThingy.SetActive(false);
        GameObject sliderstuff = transform.GetChild(0).GetComponent<GunBehavior>().Slider_Reload.transform.parent.gameObject;
        sliderstuff.gameObject.SetActive(false);
        // Duration for invisibility
        while (current <= duration)
        {
            current = current + Time.deltaTime;
            yield return null;
        }
        // Enable Enerything
        // Spawn Particle System
        invisPartIns = Instantiate(abilSettings.telePart);
        // Place System
        invisPartIns.transform.position = transform.position;
        // Ease-in to Visible
        child.GetComponent<Renderer>().enabled = true;
        child.GetComponentInChildren<LineRenderer>(true).enabled = true;
        player_script.elementRef.gameObject.SetActive(true);
        windection.slider_PlayerHealth.gameObject.SetActive(true);
        gunsThingy.SetActive(true);
        sliderstuff.gameObject.SetActive(true);
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
        // Set a destroy for system
        Destroy(invisPartIns, 1);

    }
}
