using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Text;

public class HeatMap_UI : MonoBehaviour {

    [Header("Heat Map Set-up")]
    public GameObject heatSignature;
    public Transform heatSignature_Parent;
    public string deathData;

    [Header("Random Amount (debug)")]
    public int amtRandomPlacements = 1000;

    // Auto set-up
    private FileInfo fileInfo;
    public List<string> lines;
    private StreamReader reader;
    private StreamWriter writer;
    
    // In player scripts add death coords to lines
    // Before closing game, write the lines to the file

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GenerateHeatMap();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WriteSigCoords(19.4f, 1235.1f);
        }
    }

    public void AddLines(string str)
    {
        lines.Add(str);
    }

    public void RandomPlacementOfHeatSignatures(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject sigInst = Instantiate(heatSignature);
            sigInst.transform.SetParent(heatSignature_Parent);
            sigInst.transform.position = new Vector3(UnityEngine.Random.Range(-67.9f, 79.4f), 15, UnityEngine.Random.Range(38f, -66.3f));
            
        }
    }

    // Will run untill no lines left to read
    // places heatSig at x and z location
    // READING PART
    public void GenerateHeatMap()
    {
        // Text file in as file info
        fileInfo = new FileInfo("Assets/" + deathData + ".txt");
        reader = fileInfo.OpenText();
        //Resources.Load()

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
                    print("X:" + x + "        Z:" + z);

                    GameObject sigInst = Instantiate(heatSignature);
                    sigInst.transform.SetParent(heatSignature_Parent);
                    sigInst.transform.position = new Vector3(x, 15, z);
                }

            }

            //print(str_coords);
        } while (str_coords != null);
    }

    // WRITING PART
    public void WriteSigCoords(float x, float z)
    {
        //FileStream fs = new FileStream("E:/Source Tree/BloodReign/BloodReign/Assets/" + deathData + ".txt", FileMode.Append, FileAccess.Write, FileShare.Write);
        //fs.Close();
        StreamWriter sw = new StreamWriter("Assets/" + deathData + ".txt", true, Encoding.ASCII);
        //string NextLine = x.ToString("G") + " " + z.ToString("G");
        //print(NextLine);

        sw.Write(x);
        sw.Write(" ");
        sw.Write(z);
        sw.Write(sw.NewLine);
        sw.Close();
    }
}
