using UnityEngine;
using System.Collections;

public class BonusBase : MonoBehaviour 
{
	void Update()
	{
		m_timer += Time.deltaTime;
        if (m_timer >= 3)
        {
            m_timer = 0.0f;
			DeActivate();
			Destroy(gameObject);
        }
	}
	public virtual void Activate()
	{
	}

	public virtual void DeActivate()
	{
	}
	public BonusType Type
	{
		get { return m_type; }
	}

	[SerializeField]
	private BonusType m_type = BonusType.DoublePoints;
	private float m_timer = 0.0f;

}
