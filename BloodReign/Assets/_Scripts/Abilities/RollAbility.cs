using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAbility : AbilityCommand
{
    private float rollDistance;
    public RollAbility() // or use awake
    {
        rollDistance = abilLength; // actual roll distance
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
        //NOTELTime.time Might break networking
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, abilLength, layerMask))
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
                StartCoroutine(Roll(transform.position, hitPoint, lerpSpd));
            }
            else // max distance roll
            {
                rollDistance = abilLength;
                StartCoroutine(Roll(transform.position, transform.position + (transform.forward * rollDistance), lerpSpd));
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

        while (current <= totalTime)
        {
            gameObject.transform.GetComponent<Player>().status = StatusEffect.invincible;

            current += Time.deltaTime; // Elapsed time
            float tValue = Mathf.Clamp01(current / totalTime); // figure out how much of % time has passed of elaped time relative to total time 
            GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(origin, target, tValue));
            yield return null;
        }
        //gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", originColor);
        gameObject.transform.GetComponent<Player>().status = StatusEffect.nothing;
    }

}
