using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class BalanceEditor : EditorWindow
{
	[MenuItem("Window/Balance Editor")]
	static void Init()
	{
		BalanceEditor be = (BalanceEditor)EditorWindow.GetWindow(typeof(BalanceEditor));
		be.Show();
	}

	void OnEnable()
	{
		// полчаешь
		m_waveController = Object.FindObjectOfType<WaveController>();
	}

	void OnGUI()
	{
		m_scrollPosition = EditorGUILayout.BeginScrollView(m_scrollPosition);

		WaveSettings = EditorGUILayout.Foldout(WaveSettings, "Wave Settings");
		if (WaveSettings)
		{
			int WaveCount = m_waveController.Waves.Count;
			WaveCount = EditorGUILayout.IntField("Total Waves Count", m_waveController.Waves.Count);

			if (WaveCount != m_waveController.Waves.Count)
			{
				List<Wave> temp = new List<Wave>(WaveCount);

				for (int i = 0; i < WaveCount; ++i)
				{
					if (i < m_waveController.Waves.Count)
					{
						temp.Add(m_waveController.Waves[i]);
					}
					else
					{
						temp.Add(new Wave());
					}
				}
				m_waveController.Waves = temp;
			}

			int number = 1;
			int foldoutIndex = 0;
			foreach (var wave in m_waveController.Waves)
			{
				if (foldoutIndex >= m_foldouts.Count)
				{
					m_foldouts.Add(false);
				}

				m_foldouts[foldoutIndex] = EditorGUILayout.Foldout(m_foldouts[foldoutIndex], "Wave " + number++);
				if (m_foldouts[foldoutIndex])
				{
					//int count = wave.MeteorType.Count;
					int count = EditorGUILayout.IntField("Total Meteors Count ", wave.MeteorType.Count);

					if (count != wave.MeteorType.Count)
					{
						List<MeteorType> temp = new List<MeteorType>(count);

						for (int j = 0; j < count; ++j)
						{
							if (j < wave.MeteorType.Count)
							{
								temp.Add(wave.MeteorType[j]);
							}
							else
							{
								temp.Add(MeteorType.Small);
							}
						}
						wave.MeteorType = temp;
					}

					for (int k = 0; k < wave.MeteorType.Count; ++k)
					{
						wave.MeteorType[k] = (MeteorType)EditorGUILayout.EnumPopup("Meteor Type ", wave.MeteorType[k]);
					}

				}
				foldoutIndex++;
			}
		}

		EditorGUILayout.EndScrollView();
	}

	private bool WaveSettings;
	private WaveController m_waveController = null;
	private Vector2 m_scrollPosition = Vector2.zero;
	private List<bool> m_foldouts = new List<bool>(100);
}
