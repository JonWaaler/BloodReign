using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAbility : AbilityCommand
{
    public SoundManager soundManager;
    public bool rollActive;
    private float rollDistance;
    [SerializeField]
    private GameObject rollPartIns = null;

    public RollAbility() // or use awake
    {
        if (abilSettings != null)
        {
            abilCool = abilSettings.abilCool_1;
            rollDistance = abilSettings.abilLength_1; // actual roll distance
        }
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
    // Update is called once per frame
    private void activate()
    {
        rollActive = true;
        soundManager.Play(Sounds.SoundName.Dodge);
        //NOTELTime.time Might break networking
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, abilSettings.abilLength_1, layerMask))
            {
                if (hit.collider.tag.Equals("Wall"))
                {
                    if (Mathf.Abs(hit.distance) < 1.25f)
                    {
                        rollDistance = 0.0f;
                    }
                    else
                    {
                        rollDistance = hit.distance;
                    }
                }
                // reduce roll length just incase player rolls into wall
                Vector3 hitPoint = transform.position + (transform.forward * (rollDistance * .8f));
                StartCoroutine(Roll(transform.position, hitPoint, abilSettings.lerpSpd_1));
            }
            else // max distance roll
            {
                rollDistance = abilSettings.abilLength_1;
                StartCoroutine(Roll(transform.position, transform.position + (transform.forward * rollDistance), abilSettings.lerpSpd_1));
            }
        }

    }
    // Roll lerps between origin point, target point, and a vel which controlls lerp speed
    private IEnumerator Roll(Vector3 origin, Vector3 target, float velocity)
    {
        //Color originColor = transform.GetChild(0).GetComponent<Renderer>().material.color;
        //gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        float current = 0.0f;//Elapsed time
        float rollLengh = (target - origin).magnitude; //Distance
        float totalTime = rollLengh / velocity; // Total time to finish distance at said velocity with: T = D/V

        // Spawn Particle System
        if (rollPartIns == null)
            rollPartIns = Instantiate(abilSettings.rollPart);
        while (current <= totalTime)
        {
            gameObject.transform.GetComponent<Player>().status = StatusEffect.invincible;
            // Place System
            rollPartIns.transform.position = transform.position;

            current += Time.deltaTime; // Elapsed time
            float tValue = Mathf.Clamp01(current / totalTime); // figure out how much of % time has passed of elaped time relative to total time 
            GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(origin, target, tValue));
            yield return null;
        }
        // Set a destroy for system
        Destroy(rollPartIns, 1);

        //gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", originColor);
        gameObject.transform.GetComponent<Player>().status = StatusEffect.nothing;
        rollActive = false;
    }

}
