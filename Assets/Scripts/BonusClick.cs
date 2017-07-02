using UnityEngine;
using System.Collections;

public class BonusClick : MonoBehaviour
{
    public void OnBonusClick()
    {
        BonusController controller = FindObjectOfType<BonusController>();
        controller.CreateBonus(m_type);
        controller.UseBonus(m_type);
    }

    [SerializeField]
    private BonusType m_type = BonusType.DoublePoints;
}
