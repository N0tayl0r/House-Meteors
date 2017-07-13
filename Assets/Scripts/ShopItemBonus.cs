using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopItemBonus : MonoBehaviour
{
    void Start()
    {
        m_priceText.text = "Price: " + (m_price * m_bonusCountMultiplier).ToString();

        if (m_bonusCountMultiplier == 1)
        {
            m_decButton.interactable = false;
        }
        //    m_bonusMultiplierInputField.onValidateInput += delegate(string s, int charIndex, char addedChar) { return MyValidate(addedChar); };
        //}

        //private char MyValidate(char charToValidate)
        //{
        //    charToValidate = (char)m_bonusCountMultiplier;
        //    if (m_bonusCountMultiplier <= 0)
        //    {
        //        charToValidate = '\0';
        //    }
        //    return charToValidate;
    }

    public void BuyBonus()
    {
        ShopController shopController = FindObjectOfType<ShopController>();
        SceneController sceneController = FindObjectOfType<SceneController>();
        BonusController bonusController = FindObjectOfType<BonusController>();
        m_profile = FindObjectOfType<Profile>();

        if (sceneController.Score >= bonusController.GetPrice(m_type))
        {
            int currScore = sceneController.Score;
            int count = m_profile.GetBoughtBonus(Type);
            count += m_bonusCountMultiplier;
            currScore -= (m_price * m_bonusCountMultiplier);
            m_profile.SetScore(currScore);
            m_profile.SetBoughtBonus(Type, count);
            m_profile.Save();
            sceneController.Score = currScore;
            shopController.BuyBonus(m_type);
        }
        else
        {
            Debug.Log("You don't have enough Points!");
        }
    }

    public void IncBonusCount()
    {
        if (m_bonusCountMultiplier < 99)
        {
            m_bonusCountMultiplier++;
            m_priceText.text = "Price: " + (m_price * m_bonusCountMultiplier).ToString();
            m_bonusMultiplierInputField.text = m_bonusCountMultiplier.ToString();
            if (m_decButton.interactable == false)
            {
                m_decButton.interactable = true;
            }
        }
        if (m_bonusCountMultiplier == 99)
        {
            m_incButton.interactable = false;
        }
    }

    public void DecBonusCount()
    {
        if (m_bonusCountMultiplier > 1)
        {
            m_bonusCountMultiplier--;
            m_priceText.text = "Price: " + (m_price * m_bonusCountMultiplier).ToString();
            m_bonusMultiplierInputField.text = m_bonusCountMultiplier.ToString();
            if (m_incButton.interactable == false)
            {
                m_incButton.interactable = true;
            }
        }
        if (m_bonusCountMultiplier == 1)
        {
            m_decButton.interactable = false;
        }
    }

    public void InputBonusCount(string s)
    {
        Debug.Log(s);
        if (int.TryParse(s, out m_bonusCountMultiplier))
        {
            m_priceText.text = "Price: " + (m_price * m_bonusCountMultiplier).ToString();

            if (m_bonusCountMultiplier == 99)
            {
                m_decButton.interactable = true;
                m_incButton.interactable = false;
            }
            if (m_bonusCountMultiplier == 1)
            {
                m_incButton.interactable = true;
                m_decButton.interactable = false;
            }
        }
        else
        {
            if (m_bonusMultiplierInputField.text == string.Empty)
            {
                return;
            }
            else
            {
                //MyValidate((char)m_bonusCountMultiplier);
                m_bonusMultiplierInputField.text = "1";
            }
        }
    }

    public void InputBonusCountEmpty(string s)
    {
        if (int.TryParse(s, out m_bonusCountMultiplier))
        {
            if (m_bonusMultiplierInputField.text == "0")
            {
                m_bonusMultiplierInputField.text = "1";
            }
        }
        else
        {
            if (m_bonusMultiplierInputField.text == string.Empty)
            {
                m_bonusMultiplierInputField.text = "1";
            }
        }
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
    private InputField m_bonusMultiplierInputField = null;
    [SerializeField]
    private Button m_incButton = null;
    [SerializeField]
    private Button m_decButton = null;
    [SerializeField]
    private int m_price = 0;
    private int m_bonusCountMultiplier = 1;
    private Profile m_profile = null;
}
