using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopController : MonoBehaviour
{
	public System.Action<int, BonusType> EventBonusBought = null;

	void Start()
	{
		m_bonusController = FindObjectOfType<BonusController>();
		m_houseController = FindObjectOfType<HouseController>();
		m_profile = FindObjectOfType<Profile>();

		m_activeRoof = m_profile.ActiveRoofIndex;
		//Debug.Log(m_activeRoof + " on Awake!");

		m_allBoughtRoofs = m_profile.BoughtRoofIndex;
		string[] strs = m_allBoughtRoofs.Split(new char[] { ' ' });
		int i;
		foreach (var s in strs)
		{
			i = int.Parse(s);
			m_listBought.Add(i);
		}
		
		CreateShop();
		ApplyRoof((RoofType)int.Parse(m_profile.ActiveRoofIndex));
		//Debug.Log(m_allBoughtRoofs);
	}

	void Update()
	{

	}

	public void BuyRoofUpgrade(RoofType roofType)
	{
		foreach (var setting in m_houseController.RS)
		{
			if (setting.m_type == roofType)
			{
				ApplyRoof(roofType);

				m_allBoughtRoofs += " " + (int)m_roof.GetComponent<Roof>().RoofType;
				m_activeRoof = ((int)m_roof.GetComponent<Roof>().RoofType).ToString();

				m_profile.SetBoughtRoofNumber(m_allBoughtRoofs);
				m_profile.SetActiveRoof(m_activeRoof);
				//Debug.Log(m_activeRoof + " on Buy!");

				m_listBought.Add((int)roofType);
			}
		}
	}

	public void ApplyRoof(RoofType roofType)
	{
		foreach (var setting in m_houseController.RS)
		{
			if (setting.m_type == roofType)
			{
				m_roof = Instantiate(setting.m_roofSpawnPrefab) as GameObject;
				FindObjectOfType<SceneController>().SetRoof(m_roof);

				foreach (var element in m_shopItems)
				{
					if (m_listBought.Contains((int)element.Type))
					{
						element.Bought = true;
						element.Apply = element.Type == roofType;						
						//Debug.Log(element.Type + " roof is bought.");
					}
					else
					{
						element.Bought = false;
						element.Apply = false;
						//Debug.Log(element.Type + " roof is not bought.");
					}
					element.ButtonChange();
				}
			}
		}
		m_activeRoof = ((int)m_roof.GetComponent<Roof>().RoofType).ToString();
		m_profile.SetActiveRoof(m_activeRoof);
	}

	public void BuyBonus(BonusType bonusType)
	{
		foreach (var setting in m_bonusController.BS)
		{
			if (setting.m_type == bonusType)
			{
                int count = m_profile.GetBoughtBonus(setting.m_type);
                if (EventBonusBought != null)
				{
					EventBonusBought(count, bonusType);
				}
			}
		}
	}

	public void CreateShop()
	{
		m_content.sizeDelta = new Vector2(0.0f, m_content.sizeDelta.y);
		foreach (var setting in m_houseController.RS)
		{
			GameObject item = Instantiate(m_image) as GameObject;
			RectTransform rt = item.GetComponent<RectTransform>();
			m_content.sizeDelta += new Vector2(rt.sizeDelta.x + m_content.GetComponent<HorizontalLayoutGroup>().spacing, 0.0f);
			item.transform.SetParent(m_content.transform);
			rt.localScale = Vector2.one;

			ShopItem shopItem = item.GetComponent<ShopItem>();
			shopItem.Type = setting.m_type;
			shopItem.Image.sprite = setting.m_roofImage;
			shopItem.Price.text = setting.m_price.ToString();
			shopItem.Description.text = setting.m_descriptionText;

			m_shopItems.Add(shopItem);
		}
		m_content.sizeDelta -= new Vector2(m_image.GetComponent<RectTransform>().sizeDelta.x + m_content.GetComponent<HorizontalLayoutGroup>().spacing, 0.0f);
		m_image.SetActive(false);

		m_bonusShopContent.sizeDelta = new Vector2(0.0f, m_bonusShopContent.sizeDelta.y);
		foreach (var setting in m_bonusController.BS)
		{
			GameObject item = Instantiate(m_bonusShopImage) as GameObject;
			RectTransform rt = item.GetComponent<RectTransform>();
			m_bonusShopContent.sizeDelta += new Vector2(rt.sizeDelta.x + m_bonusShopContent.GetComponent<HorizontalLayoutGroup>().spacing, 0.0f);
			item.transform.SetParent(m_bonusShopContent.transform);
			rt.localScale = Vector2.one;

			ShopItemBonus shopItemBonus = item.GetComponent<ShopItemBonus>();
			shopItemBonus.Type = setting.m_type;
			shopItemBonus.Image.sprite = setting.m_bonusImage;
			shopItemBonus.Price.text = setting.m_price.ToString();
			shopItemBonus.Description.text = setting.m_descriptionText;
		}
		m_bonusShopContent.sizeDelta -= new Vector2(m_bonusShopImage.GetComponent<RectTransform>().sizeDelta.x + m_bonusShopContent.GetComponent<HorizontalLayoutGroup>().spacing, 0.0f);
		m_bonusShopImage.SetActive(false);
	}


	[SerializeField]
	private RectTransform m_content = null;
	[SerializeField]
	private GameObject m_image = null;
	[SerializeField]
	private RectTransform m_bonusShopContent = null;
	[SerializeField]
	private GameObject m_bonusShopImage = null;
	private GameObject m_roof = null;
	private HouseController m_houseController = null;
	private BonusController m_bonusController = null;
	private Profile m_profile = null;
	private string m_allBoughtRoofs = string.Empty;
	private string m_activeRoof = string.Empty;

	List<int> m_listBought = new List<int>();

	List<ShopItem> m_shopItems = new List<ShopItem>();
}
