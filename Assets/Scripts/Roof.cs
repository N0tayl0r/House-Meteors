using UnityEngine;
using System.Collections;

public class Roof : MonoBehaviour 
{

	void Start () 
    {
	    
	}

	void Update () 
    {
	    
	}

    public int RoofDurability
    {
        get { return m_roofDurability; }
    }

    [SerializeField]
    private int m_roofDurability = 0;
	[SerializeField]
	private RoofType m_roofType = RoofType.Wooden;

	public RoofType RoofType
	{
		get { return m_roofType; }
		set { m_roofType = value; }
	}
}
