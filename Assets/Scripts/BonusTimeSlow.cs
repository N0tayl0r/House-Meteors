using UnityEngine;
using System.Collections;

public class BonusTimeSlow : BonusBase
{
	public override void Activate()
	{
		base.Activate();
        Time.timeScale = 0.5f;
	}

	public override void DeActivate()
	{
		base.DeActivate();
		Time.timeScale = 1.0f;
	}
}
