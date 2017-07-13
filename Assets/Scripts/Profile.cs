using UnityEngine;
using System.Collections;

public class Profile : MonoBehaviour
{
    //PlayerPrefs.SetInt("active_roof", (int)roof.Type);
    //PlayerPrefs.SetString("all_roofs", "1 2 5");

    //PlayerPrefs.GetString("all_roofs");
    //string s = "1 2 5";
    //string[] strs = s.Split(new char[]{' '});
    //int i = int.Parse(strs[0]);
    //RoofType type = (RoofType)i;
    void Awake()
    {
        m_boughtRoofsIndex = PlayerPrefs.GetString(m_boughtRoofsKey, "0");
        m_activeRoofIndex = PlayerPrefs.GetString(m_activeRoofKey, "0");
        m_currentScoreIndex = PlayerPrefs.GetInt(m_currentScoreKey, 0);
    }

    public void SetBoughtRoofNumber(string b)
    {
        PlayerPrefs.SetString(m_boughtRoofsKey, b);
    }

    public void SetActiveRoof(string a)
    {
        PlayerPrefs.SetString(m_activeRoofKey, a);
    }

    public void SetBoughtBonus(BonusType bonusType, int count)
    {
        PlayerPrefs.SetInt(bonusType.ToString(), count);
    }

    public int GetBoughtBonus(BonusType bonusType)
    {
        return PlayerPrefs.GetInt(bonusType.ToString());
    }

    public void SetScore(int score)
    {
        PlayerPrefs.SetInt(m_currentScoreKey, score);
    }

    public string BoughtRoofIndex
    {
        get { return m_boughtRoofsIndex; }
    }

    public string ActiveRoofIndex
    {
        get { return m_activeRoofIndex; }
    }

    public int CurrentScoreIndex
    {
        get { return m_currentScoreIndex; }
    }

    public void Save()
    {
        PlayerPrefs.Save();
    }


    private const string m_boughtRoofsKey = "bought_roofs";
    private const string m_activeRoofKey = "active_roof";
    private const string m_currentScoreKey = "current_score";

    private string m_boughtRoofsIndex = string.Empty;
    private string m_activeRoofIndex = string.Empty;
    private int m_currentScoreIndex = 0;
}
