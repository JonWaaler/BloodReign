using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	void OnGUI()
	{
		GUI.Box(new Rect(10 , Screen.height - 100 ,100 ,90), "Change Scene");
		if(GUI.Button( new Rect(20 , Screen.height - 70 ,80, 20), "Next"))
			LoadNextScene();
		if(GUI.Button(new Rect(20 ,  Screen.height - 40 ,80, 20), "Back"))
			LoadPreScene();
	}

	void LoadPreScene()
	{
		//int nextLevel = Application.loadedLevel + 1;
		//if( nextLevel <= 1)
		//	nextLevel = Application.levelCount;

		//Application.LoadLevel(nextLevel);
        
        // JonW replaced deprecated code with new
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel <= 1)
            nextLevel = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(nextLevel);
    }

	void LoadNextScene()
	{
        //int nextLevel = Application.loadedLevel + 1;
        //if( nextLevel >= Application.levelCount)
        //	nextLevel = 1;
        //
        //Application.LoadLevel(nextLevel);

        // JonW replaced deprecated code with new
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel >= SceneManager.sceneCount)
            nextLevel = 1;

        SceneManager.LoadScene(nextLevel);
	}
}
