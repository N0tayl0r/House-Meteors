using UnityEngine;
using System.Collections.Generic;

public class BonusController : MonoBehaviour
{
    public System.Action<int, BonusType> EventBonusCountDecreased = null;
    public System.Action<int, BonusType> EventBonusCountIncreased = null;

    [System.Serializable]
	public class BonusSettings
	{
		public BonusType m_type = BonusType.DoublePoints;
		public Sprite m_bonusImage = null;
		public int m_price = 0;
		public string m_descriptionText = null;
	}

	void Start()
	{
        m_profile = FindObjectOfType<Profile>();
    }

	void Update()
	{

	}

	public void CreateBonusActivator(Vector2 position)
	{
		float currentChance = Random.Range(0.0f, 1.0f);
		if (currentChance <= m_chance)
		{
			int index = Random.Range(0, m_bonusesPrefabs.Count);
			GameObject go = Instantiate(m_bonusesPrefabs[index]);
			go.transform.position = position;
		}
	}

	public void CreateBonus(BonusType type)
	{
		foreach (var b in m_bonuses)
		{
			if (b.Type == type)
			{
				GameObject go = Instantiate(b.gameObject);
                BonusBase bb = go.GetComponent<BonusBase>();
                bb.Activate();
            }
		}
	}

    public int GetPrice(BonusType type)
    {
        foreach (var b in m_bonusSettings)
        {
            if (b.m_type == type)
            {
                return b.m_price;
            }
        }
        return 0;
    }

    public void AddBonus(BonusType type)
    {
        int count = m_profile.GetBoughtBonus(type);
        count++;
        m_profile.SetBoughtBonus(type, count);
        if (EventBonusCountIncreased != null)
        {
            EventBonusCountIncreased(count, type);
        }
    }

    public void UseBonus(BonusType type)
    {
        int count = m_profile.GetBoughtBonus(type);
        count--;
        m_profile.SetBoughtBonus(type, count);
        if (EventBonusCountDecreased != null)
        {
            EventBonusCountDecreased(count, type);
        }
    }

    public List<BonusSettings> BS
	{
		get { return m_bonusSettings; }
		set { m_bonusSettings = value; }
	}

    public List<BonusBase> Bonuses
    {
        get { return m_bonuses; }
    }

	[SerializeField]
	private List<BonusSettings> m_bonusSettings = new List<BonusSettings>();
	[SerializeField]
	private List<GameObject> m_bonusesPrefabs = new List<GameObject>();
	[SerializeField]
    private List<BonusBase> m_bonuses = new List<BonusBase>();
	[SerializeField]
	private float m_chance = 0.2f;
    private Profile m_profile = null;
}
