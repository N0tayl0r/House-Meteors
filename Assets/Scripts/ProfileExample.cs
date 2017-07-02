using UnityEngine;
using System.Collections;

public class ProfileExample : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        Debug.Log(PlayerPrefs.GetFloat(m_floatKey, 1.0f));
		Time.timeScale = PlayerPrefs.GetFloat(m_floatKey, 1.0f);
	}
	
    void OnGUI()
    {
        if(GUI.Button(new Rect(100, 100, 100, 100), "Save"))
        {
			PlayerPrefs.SetFloat(m_floatKey, Time.timeScale);
            PlayerPrefs.Save();
            Debug.Log("Save Done");
        }
        if (GUI.Button(new Rect(100, 200, 100, 100), "+"))
        {
            Time.timeScale += 0.5f;
            Debug.Log("Time Inc");
        }
        if (GUI.Button(new Rect(100, 300, 100, 100), "-"))
        {
            Time.timeScale -= 0.5f;
            Debug.Log("Time Dec");
        }
        GUI.Label(new Rect(100, 400, 1000, 100), "TimeScale = " + Time.timeScale);
    }

    private const string m_floatKey = "float_key";

	// Update is called once per frame
	void Update () 
    {
	    
	}
}
