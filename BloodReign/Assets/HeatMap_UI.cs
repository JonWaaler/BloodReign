using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HeatMap_UI : MonoBehaviour {

    [Header("Heat Map Set-up")]
    public GameObject heatSignature;
    public Transform heatSignature_Parent;
    public TextAsset deathData;

    [Header("Random Amount (debug)")]
    public int amtRandomPlacements = 1000;

    private StreamReader reader;
    private StreamWriter writer;

	void Start ()
    {
        //FileInfo fileInfo = new FileInfo()


        RandomPlacementOfHeatSignatures(amtRandomPlacements);
        GenerateHeatMap();
	}
	
	void Update ()
    {
		
	}

    void RandomPlacementOfHeatSignatures(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject sigInst = Instantiate(heatSignature);
            sigInst.transform.SetParent(heatSignature_Parent);
            sigInst.transform.position = new Vector3(Random.Range(-67.9f, 79.4f), 15, Random.Range(38f, -66.3f));
        }
    }

    // Will run untill no lines left to read
    // places heatSig at x and z location
    void GenerateHeatMap()
    {        
        print(deathData.text);
    }
}
