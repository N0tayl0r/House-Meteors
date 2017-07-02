using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Wave 
{

	void Start () 
    {
	    
	}

	void Update () 
    {
	    
	}

    public List<MeteorType> MeteorType
    {
        get { return m_meteorType; }
        set { m_meteorType = value; }
    }

    [SerializeField]
    private List<MeteorType> m_meteorType = new List<MeteorType>();
}
