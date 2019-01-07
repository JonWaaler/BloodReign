using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiageticHealth : MonoBehaviour {

    [Header("IN GAME HEALTH")]
    public Slider diageticHealth_1;
    public Slider diageticHealth_2;
    public Slider diageticHealth_3;
    public Slider diageticHealth_4;

    [Header("Health In Corners")]
    public Slider UI_Health_1;
    public Slider UI_Health_2;
    public Slider UI_Health_3;
    public Slider UI_Health_4;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    

    public void DiageticUI_Update_1()
    {
        diageticHealth_1.value = UI_Health_1.value;
        // if (UI_Health_1.value <= 0.1f)
        //     diageticHealth_1.gameObject.SetActive(false);
    }
    public void DiageticUI_Update_2()
    {
        diageticHealth_2.value = UI_Health_2.value;
        // if (UI_Health_2.value <= 0.1f)
        //     diageticHealth_2.gameObject.SetActive(false);
    }
    public void DiageticUI_Update_3()
    {
        diageticHealth_3.value = UI_Health_3.value;
        // if (UI_Health_3.value <= 0.1f)
        //     diageticHealth_3.gameObject.SetActive(false);
    }
    public void DiageticUI_Update_4()
    {
        diageticHealth_4.value = UI_Health_4.value;
        // if (UI_Health_4.value <= 0.1f)
        //     diageticHealth_4.gameObject.SetActive(false);
    }
}
