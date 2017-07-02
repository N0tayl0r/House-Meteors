using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopItemBonus : MonoBehaviour
{
    public void BuyBonus()
    {
        ShopController shopController = FindObjectOfType<ShopController>();
        SceneController sceneController = FindObjectOfType<SceneController>();
        BonusController bonusController = FindObjectOfType<BonusController>();
        m_profile = FindObjectOfType<Profile>();

        if (sceneController.Score >= bonusController.GetPrice(m_type))
        {
            int count = m_profile.GetBoughtBonus(Type);
            count += BonusCountMultiplier;
            m_profile.SetBoughtBonus(Type, count);

            shopController.BuyBonus(m_type);
        }
    }

    public void IncBonusCount()
    {
        m_bonusCountMultiplier++;
        m_priceText.text = (m_price * m_bonusCountMultiplier).ToString();
        m_bonusesCount.text = m_bonusCountMultiplier.ToString();
    }

    public void DecBonusCount()
    {
        m_bonusCountMultiplier--;
        m_priceText.text = (m_price * m_bonusCountMultiplier).ToString();
        m_bonusesCount.text = m_bonusCountMultiplier.ToString();
    }

    public BonusType Type
    {
        get { return m_type; }
        set { m_type = value; }
    }

    public Image Image
    {
        get { return m_roofImage; }
        set { m_roofImage = value; }
    }

    public Text Price
    {
        get { return m_priceText; }
        set { m_priceText = value; }
    }

    public Text Description
    {
        get { return m_descriptionText; }
        set { m_descriptionText = value; }
    }

    public int BonusCountMultiplier
    {
        get { return m_bonusCountMultiplier; }
        set { m_bonusCountMultiplier = value; }
    }

    [SerializeField]
    private BonusType m_type = BonusType.DoublePoints;
    [SerializeField]
    private Image m_roofImage = null;
    [SerializeField]
    private Text m_priceText = null;
    [SerializeField]
    private Text m_descriptionText = null;
    [SerializeField]
    private InputField m_bonusesCount = null;
    [SerializeField]
    private Button m_incBonusesCount = null;
    [SerializeField]
    private Button m_decBonusesCount = null;
    [SerializeField]
    private int m_price = 0;
    private int m_bonusCountMultiplier = 1;
    private Profile m_profile = null;
}
