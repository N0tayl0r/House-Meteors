//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

//public class Shop : MonoBehaviour
//{
//	[System.Serializable]
//	public class HammerSettings // класс настроек
//	{
//		public HammerType m_type = HammerType.stone; // энам HammerType
//		public GameObject m_hammerPrefab = null; // префаб ГО молотка
//		public int m_strength = 30; // некое значение силы
//		public int m_damage = 1; // некое значение урона
//		public int m_mass = 1; // некое значение массы
//		public Sprite m_image; // спрайт картинки крыши
//	}

//	void Start()
//	{
//		CreateShop(); // вызов метода по созданию магазина
//	}

//	public void ByuUpgrate(HammerType Type) // метод покупки апгрейда молотка
//	{
//		foreach (var settings in m_settings) // цикл, который пробегается по листу настроек HammerSettings
//		{
//			if (settings.m_type == Type) // проверка на равенство типа из листа настроек с типом, который передаётся в метод покупки апгрейда молотка
//			{
//				m_hammer = Instantiate(settings.m_hammerPrefab) as GameObject; // создание клона ГО молотка из класса настроек HammerSettings по m_hammerPrefab 
//				FindObjectOfType<GameController>().SetHammer(m_hammer); // видимо, вызов метода по смене молотка (SetHammer) на игровом экране путём поиска на сцене объекта 
//																		// типа GameController и последующего вызова метода
//			}
//		}
//	}

//	public void CreateShop() // метод создания магазина
//	{
//		m_content.sizeDelta = new Vector2(0.0f, m_content.sizeDelta.y); // ???
//		foreach (var settings in m_settings) { // цикл, который пробегается по листу настроек HammerSettings
//			GameObject item = Instantiate (m_image) as GameObject; // создание клона спрайта m_image; запись этого клона в локальную переменную item
//			RectTransform rt = item.GetComponent<RectTransform>(); // получение компоненты RectTransform у item'a и присвоение результата локальной переменной rt
//			m_content.sizeDelta += new Vector2(rt.sizeDelta.x, 0.0f); // ???
//			item.transform.SetParent(m_content.transform); // ???

//			ShopItem shopItem = item.GetComponent<ShopItem>(); // получение компоненты ShopItem у item'a и присвоение результата локальной переменной shopItem
//			shopItem.mass.text = "Mass: " + settings.m_mass; // изменение текста для настройки массы шоп айтема
//			shopItem.strength.text = "Strength: " + settings.m_strength; // изменение текста для настройки силы шоп айтема
//			shopItem.damage.text = "Damage: " + settings.m_damage; // изменение текста для настройки урона шоп айтема
//			shopItem.Image.sprite = settings.m_image; // изменение спрайта для настройки Image шоп айтема
//			shopItem.Type = settings.m_type; // изменение типа шоп айтема
//		}
//		m_image.gameObject.SetActive(false); // выключение ГО m_image
//	}

//	[SerializeField]
//	private List<HammerSettings> m_settings = new List<HammerSettings>();

//	[SerializeField]
//	private GameObject m_image;

//	[SerializeField]
//	private RectTransform m_content = null;

//	private GameObject m_hammer;
//}
// 1. Создать настройки и настроить на объекте
// 2. Вызвать метод CreateShop и адаптировать его под себя.
// 3. Показать панель магазина в нужный момент