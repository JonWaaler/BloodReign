using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability Settings", menuName = "Ability Settings")]
public class AbilitySettings : ScriptableObject
{
    [Header("Roll Ability")]
    public float abilCool_1;
    public float abilLength_1;
    public float lerpSpd_1; // smaller number is faster

    [Header("Teleport Ability")]
    public float abilCool_2;
    public float abilLength_2;
    public float lerpSpd_2; // smaller number is faster

    [Header("Hook Ability")]
    public float abilCool_3;
    public float abilLength_3;
    public float lerpSpd_3; // smaller number is faster
    public float lerpReelSpd;
    public float grappleDmg;

    [Header("Invisible Ability")]
    public float abilCool_4;
    public float abilLength_4;
    public float lerpSpd_4; // smaller number is faster

    [Header("Teleport Ball")]
    public GameObject collisionSphereInit_1;

    [Header("Grapple Ball")]
    public GameObject collisionSphereInit_2;

    [Header("Invisibility Particles")]
    public GameObject invisPart;

    [Header("Teleportation Particles")]
    public GameObject telePart;

}