using UnityEngine;
using System.Collections;

public class BonusActivator : MonoBehaviour
{
	public BonusType Type
	{
		get { return m_type; }
	}

	[SerializeField]
	private BonusType m_type = BonusType.DoublePoints;
}
