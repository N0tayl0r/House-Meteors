using UnityEngine;
using System.Collections;

public class BonusDoublePoints : BonusBase
{
	public override void Activate()
	{
		base.Activate();
		SceneController sc = FindObjectOfType<SceneController>();
		sc.ScoreMultiplier = 2;
	}

	public override void DeActivate()
	{
		base.DeActivate();
		SceneController sc = FindObjectOfType<SceneController>();
		sc.ScoreMultiplier = 1;
	}
}
