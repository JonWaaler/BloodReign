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

    private FileInfo fileInfo;
    private StreamReader reader;
    private StreamWriter writer;

	void Start ()
    {


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
    // READING PART
    void GenerateHeatMap()
    {
        // Text file in as file info
        fileInfo = new FileInfo("E:/Source Tree/BloodReign/BloodReign/Assets/" + deathData.name + ".txt");
        reader = fileInfo.OpenText();


        string str_coords;

        do
        {
            str_coords = reader.ReadLine();
            if (str_coords != null)
            {
                // Get index of where the space is seperating x and z coords
                int spaceIndex = str_coords.IndexOf(' ');
                //print("Index: " + spaceIndex);

                float x;
                bool workedX = float.TryParse(str_coords.Substring(0, spaceIndex), out x);
                float z;
                bool workedZ = float.TryParse(str_coords.Substring(spaceIndex, str_coords.Length - spaceIndex), out z);

                if (workedX && workedZ)
                {
                    //Debug
                    //print("X:" + x + "        Z:" + z);

                    GameObject sigInst = Instantiate(heatSignature);
                    sigInst.transform.SetParent(heatSignature_Parent);
                    sigInst.transform.position = new Vector3(x, 15, z);
                }

            }

            //print(str_coords);
        } while (str_coords != null);
    }

    // WRITING PART
    void WriteSigCoords(float x, float z)
    {
        // Text file in as file info
        fileInfo = new FileInfo("E:/Source Tree/BloodReign/BloodReign/Assets/" + deathData.name + ".txt");
        //writer = fileInfo.OpenWrite();
    }
}
