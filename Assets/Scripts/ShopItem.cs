using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopItem : MonoBehaviour
{

    public void BuyRoofUpgrade()
    {
        ShopController shopController = FindObjectOfType<ShopController>();
        SceneController sceneController = FindObjectOfType<SceneController>();
        HouseController houseController = FindObjectOfType<HouseController>();
        if (sceneController.Score >= houseController.GetPrice(m_type))
        {
            shopController.BuyRoofUpgrade(m_type);
            Bought = true;
            Apply = true;
            ButtonChange();
        }
    }

    public void ApplyRoof()
    {
        ShopController sc = FindObjectOfType<ShopController>();
        sc.ApplyRoof(m_type);
    }

    public RoofType Type
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
        get { return m_price; }
        set { m_price = value; }
    }

    public Text Description
    {
        get { return m_descriptionText; }
        set { m_descriptionText = value; }
    }

    public bool Bought
    {
        get { return m_boughtFlag; }
        set
        {
            m_boughtFlag = value;
            m_buyButton.gameObject.SetActive(!m_boughtFlag);
            m_appliedButton.gameObject.SetActive(m_boughtFlag);
        }
    }

    public bool Apply
    {
        get { return m_applyFlag; }
        set
        {
            m_applyFlag = value;
            m_appliedButton.gameObject.SetActive(!m_applyFlag);
            m_buyButton.gameObject.SetActive(!m_applyFlag);
            m_applyButton.gameObject.SetActive(m_applyFlag);
        }
    }

    public void ButtonChange()
    {
        if (!Bought)
        {
            m_buyButton.gameObject.SetActive(true);
            m_applyButton.gameObject.SetActive(false);
            m_appliedButton.gameObject.SetActive(false);
        }
        else if (Bought && !Apply)
        {
            m_buyButton.gameObject.SetActive(false);
            m_applyButton.gameObject.SetActive(true);
            m_appliedButton.gameObject.SetActive(false);
        }
        else if (Bought && Apply)
        {
            m_buyButton.gameObject.SetActive(false);
            m_applyButton.gameObject.SetActive(false);
            m_appliedButton.gameObject.SetActive(true);
        }
    }

    [SerializeField]
    private Button m_buyButton = null;
    [SerializeField]
    private Button m_applyButton = null;
    [SerializeField]
    private Button m_appliedButton = null;
    [SerializeField]
    private RoofType m_type = RoofType.Wooden;
    [SerializeField]
    private Image m_roofImage = null;
    [SerializeField]
    private Text m_price = null;
    [SerializeField]
    private Text m_descriptionText = null;
    private bool m_boughtFlag = false;
    private bool m_applyFlag = false;
}
