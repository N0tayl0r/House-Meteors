using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HouseController : MonoBehaviour 
{
	[System.Serializable]
	public class RoofSettings
	{
		public RoofType m_type = RoofType.Wooden;
		public GameObject m_roofSpawnPrefab = null;
		public Sprite m_roofImage = null;
		public int m_price = 0;
		public string m_descriptionText = null;
	}

    public int GetPrice(RoofType type)
    {
        foreach (var b in m_roofSettings)
        {
            if (b.m_type == type)
            {
                return b.m_price;
            }
        }
        return 0;
    }

    public List<RoofSettings> RS
	{
		get { return m_roofSettings;}
		set { m_roofSettings = value;}
	}

	[SerializeField]
	private List<RoofSettings> m_roofSettings = new List<RoofSettings>();
}
