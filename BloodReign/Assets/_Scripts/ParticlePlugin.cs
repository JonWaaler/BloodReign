using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ParticlePlugin : MonoBehaviour
{

    public GameObject particleObj;
    [SerializeField] private GameObject[] particles;

    const string DLL_NAME_Part = "HelloWorldParticle";

    [DllImport(DLL_NAME_Part)]
    public static extern float Random(float min, float max);
    // Emitter Control
    [DllImport(DLL_NAME_Part)] // returns true if id out of bounds
    public static extern bool Sort(int id); // DO NOT USE
    [DllImport(DLL_NAME_Part)]
    public static extern void AddEmitter();
    [DllImport(DLL_NAME_Part)]
    public static extern void DeleteEmitter(int id);
    [DllImport(DLL_NAME_Part)] // returns int of emitter size
    public static extern int EmitterSize();
    [DllImport(DLL_NAME_Part)]
    public static extern void SetEmitterPos(int id, float x, float y, float z);
    [DllImport(DLL_NAME_Part)]
    public static extern void InitializeParticles(int id, int numbOfParticles);
    [DllImport(DLL_NAME_Part)]
    public static extern void UpdateEmitter(int id, float time); // Call every update frame

    // Particle INFO
    [DllImport(DLL_NAME_Part)]
    public static extern uint GetNumbOfParticles(int id);
    [DllImport(DLL_NAME_Part)]
    public static extern System.IntPtr GetParticlePos(int id); // array size is numb of Particles * 3 // every call deletes old save // X,Y,Z order
    [DllImport(DLL_NAME_Part)]
    public static extern System.IntPtr GetParticleLife(int id); // array size is numb of Particles // every call deletes old save
    [DllImport(DLL_NAME_Part)]
    public static extern void DeleteParticlePosXYZ();
    [DllImport(DLL_NAME_Part)]
    public static extern void DeleteParticleLife();

    // Particle Settings (void functions)
    [DllImport(DLL_NAME_Part)]
    public static extern void EmitterPlaying(int id, bool b);
    [DllImport(DLL_NAME_Part)]
    public static extern void VelocityMinRange(int id, float x1, float y1, float z1);
    [DllImport(DLL_NAME_Part)]
    public static extern void VelocityMaxRange(int id, float x2, float y2, float z2);
    [DllImport(DLL_NAME_Part)]
    public static extern void LifeMinRange(int id, float x1);
    [DllImport(DLL_NAME_Part)]
    public static extern void LifeMaxRange(int id, float x2);
    [DllImport(DLL_NAME_Part)]
    public static extern void SpawnMinRange(int id, float x1);
    [DllImport(DLL_NAME_Part)]
    public static extern void SpawnMaxRange(int id, float x2);
    [DllImport(DLL_NAME_Part)]
    public static extern void BoxEmissionOn(int id, bool b);
    [DllImport(DLL_NAME_Part)]
    public static extern void BoxEmissionVol(int id, float x, float y, float z);
    [DllImport(DLL_NAME_Part)]
    public static extern void DeleteMaxParticle();
    [DllImport(DLL_NAME_Part)]
    public static extern void CreateMaxParticle();

    /* // System.IntPtr EXAMPLE
       float[] stackP = new float[3];
       System.IntPtr ptr = PopStack();
       stackP = new float[3];
       Marshal.Copy(ptr, stackP, 0, 3);
       // seach for object with ID = stackP[3]
       // set pos of objectID.transform.x.y = [0], [1]
       Vector3 newPos = new Vector3(stackP[0], stackP[1], -10);
    */
    public uint numbOfParticles = 0;
    public Vector3 velMin = new Vector3();
    public Vector3 velMax = new Vector3();
    public float lifeMin = 1;
    public float lifeMax = 3;
    public bool emitterPlay = true;
    public Vector3 emitterPos = new Vector3();
    public float spawnMin = 0;
    public float spawnMax = 0;
    public bool boxEmitter = true;
    public Vector3 boxLWH = new Vector3();

    // Use this for initialization
    void Start()
    {
        //print("emitter START size: " + EmitterSize());
        DeleteMaxParticle();
        CreateMaxParticle();
        AddEmitter();
        InitializeParticles(0, 400);
//        VelocityMinRange(0, 15, -1, -50);
//        VelocityMaxRange(0, 40, 1, 30);
        VelocityMinRange(0, velMin.x, velMin.y, velMin.z);
        VelocityMaxRange(0, velMax.x, velMax.y, velMax.z);
        //LifeMinRange(0, 4);
        //LifeMaxRange(0, 6);
        LifeMinRange(0, lifeMin);
        LifeMaxRange(0, lifeMax);
        //EmitterPlaying(0, true);
        EmitterPlaying(0, emitterPlay);
        //SetEmitterPos(0, -90, -10, 0);
        SetEmitterPos(0, emitterPos.x, emitterPos.y, emitterPos.z);
        //SpawnMinRange(0, 10);
        //SpawnMaxRange(0, 30);
        SpawnMinRange(0, spawnMin);
        SpawnMaxRange(0, spawnMax);
        // BoxEmissionOn(0, true);
        BoxEmissionOn(0, boxEmitter);
        //BoxEmissionVol(0, 20, 10, 20);
        BoxEmissionVol(0, boxLWH.x, boxLWH.y, boxLWH.z);
        //print("emitter particles last" + ":" + GetNumbOfParticles(EmitterSize() - 1));
        particles = new GameObject[GetNumbOfParticles(0)];
        // put spawn range
        // put vel range

        for (int i = 0; i < particles.Length; i++)
        {
            particles[i] = particleObj;
            particles[i] = Instantiate<GameObject>(particleObj);
        }
    }

    public Vector3[] allvecPos;
    public float[] allvecLife;
    // Update is called once per frame
    void Update()
    {
        VelocityMinRange(0, velMin.x, velMin.y, velMin.z);
        VelocityMaxRange(0, velMax.x, velMax.y, velMax.z);
        LifeMinRange(0, lifeMin);
        LifeMaxRange(0, lifeMax);
        EmitterPlaying(0, emitterPlay);
        SetEmitterPos(0, emitterPos.x, emitterPos.y, emitterPos.z);
        SpawnMinRange(0, spawnMin);
        SpawnMaxRange(0, spawnMax);
        BoxEmissionOn(0, boxEmitter);
        BoxEmissionVol(0, boxLWH.x, boxLWH.y, boxLWH.z);


        UpdateEmitter(0, Time.deltaTime);
        Sort(0);
        System.IntPtr ptr = GetParticlePos(0);
        System.IntPtr ptrLife = GetParticleLife(0);
        uint numbOfParticles = GetNumbOfParticles(0);
        float[] allParticlePos = new float[numbOfParticles * 3];
        Marshal.Copy(ptr, allParticlePos, 0, allParticlePos.Length);
        allvecPos = new Vector3[numbOfParticles];
        float[] allParticleLife = new float[numbOfParticles];
        Marshal.Copy(ptrLife, allParticleLife, 0, allParticleLife.Length);
        allvecLife = new float[numbOfParticles];
        int j = 0;
        for (int i = 0; i < allParticlePos.Length; i += 3)
        {
            {
                allvecPos[j].x = allParticlePos[i];
                allvecPos[j].y = allParticlePos[i + 1];
                allvecPos[j].z = allParticlePos[i + 2];
                particles[j].transform.position = allvecPos[j];
            }
            Color alpha = particles[j].GetComponent<Renderer>().material.color;
            if (allParticleLife[j] > 0.0f)
            {
                alpha.a = 1.0f;
                particles[j].transform.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                alpha.a = 0.0f;
                particles[j].transform.GetComponent<MeshRenderer>().enabled = false;
            }
            particles[j].transform.GetComponent<Renderer>().material.color = alpha;

            j++;
        }
        DeleteParticlePosXYZ();

    }

    void OnApplicationQuit()
    {
        DeleteMaxParticle();
    }


}
