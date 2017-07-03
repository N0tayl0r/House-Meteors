using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour
{
	public void OnPauseResumeButtonClick()
	{
		if (!m_waveController.WaveInProcess)
		{
			m_startWaveButton.SetActive(true);
		}
		m_pause = false;
		Time.timeScale = 1;
		m_menuBackground.SetActive(false);
		m_pauseMenu.SetActive(false);
	}

	public void OnRoofShopBackButtonClick()
	{
		m_shopMenu.SetActive(true);
		m_roofShopMenu.SetActive(false);
	}

	public void OnBonusShopBackButtonClick()
	{
		m_shopMenu.SetActive(true);
		m_bonusShopMenu.SetActive(false);
	}

    public void OnRoofShopButtonClick()
    {
        m_shopMenu.SetActive(false);
        m_roofShopMenu.SetActive(true);
    }

	public void OnBonusShopButtonClick()
	{
		m_shopMenu.SetActive(false);
		m_bonusShopMenu.SetActive(true);
	}

	public void OnShopButtonCLick()
	{
		m_pauseMenu.SetActive(false);
        m_shopMenu.SetActive(true);
	}

    public void OnShopBackButtonClick()
    {
		m_shopMenu.SetActive(false);
		m_pauseMenu.SetActive(true);
    }

	public void StartWaveUI()
	{
		WaveController controller = FindObjectOfType<WaveController>();
		controller.StartWave();
		m_startWaveButton.SetActive(false);
		controller.EventWaveFinished += OnWaveFinished;
	}

	void OnDestroy()
	{
		WaveController controller = FindObjectOfType<WaveController>();
		if (controller != null)
		{
			controller.EventWaveFinished -= OnWaveFinished;
		}

		if (m_shopController != null)
		{
			m_shopController.EventBonusBought -= OnBonusCountChanged;
		}

        if (m_bonusController != null)
        {
            m_bonusController.EventBonusCountDecreased -= OnBonusCountChanged;
        }

        if (m_sceneController != null)
        {
            m_sceneController.EventMeteorDestroyed -= OnScoreChanged;
        }
	}

	private void OnWaveFinished()
	{
		m_startWaveButton.SetActive(true);
	}

	private void OnBonusCountChanged(int count, BonusType bonusType)
	{
		if (bonusType == BonusType.DoublePoints)
		{
			m_counterDoublePointsText.text = "" + count;
		}

		if (bonusType == BonusType.TimeSlow)
		{
			m_counterTimeSlowText.text = "" + count;
		}
	}

    public void OnScoreChanged()
    {
        m_scoreText.text = "Score: " + m_sceneController.Score;
    }

    void Start()
	{
		m_waveController = FindObjectOfType<WaveController>();
		m_shopController = FindObjectOfType<ShopController>();
        m_bonusController = FindObjectOfType<BonusController>();
        m_sceneController = FindObjectOfType<SceneController>();
        m_profile = FindObjectOfType<Profile>();

        m_counterDoublePointsText.text = "" + m_profile.GetBoughtBonus(BonusType.DoublePoints);
        m_counterTimeSlowText.text = "" + m_profile.GetBoughtBonus(BonusType.TimeSlow);
        m_scoreText.text = "Score: " + m_profile.GetScore(m_sceneController.Score);

        m_shopController.EventBonusBought += OnBonusCountChanged;
        m_bonusController.EventBonusCountIncreased += OnBonusCountChanged;
        m_bonusController.EventBonusCountDecreased += OnBonusCountChanged;
        m_sceneController.EventMeteorDestroyed += OnScoreChanged;
    }

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			m_pause = !m_pause;
			if (m_pause)
			{
				if (!m_waveController.WaveInProcess)
				{
					m_startWaveButton.SetActive(false);
				}
				m_pause = true;
				Time.timeScale = 0;
				m_menuBackground.SetActive(true);
				m_pauseMenu.SetActive(true);
			}
			else
			{
				if (!m_waveController.WaveInProcess)
				{
					m_startWaveButton.SetActive(true);
				}
				m_pause = false;
				Time.timeScale = 1;
				m_menuBackground.SetActive(false);
				m_roofShopMenu.SetActive(false);
				m_bonusShopMenu.SetActive(false);
				m_pauseMenu.SetActive(false);
			}

		}

	}

	[SerializeField]
	private GameObject m_startWaveButton = null;
	[SerializeField]
	private GameObject m_pauseMenu = null;
	[SerializeField]
	private GameObject m_roofShopMenu = null;
	[SerializeField]
	private GameObject m_bonusShopMenu = null;
	[SerializeField]
	private GameObject m_menuBackground = null;
    [SerializeField]
    private GameObject m_shopMenu = null;
	[SerializeField]
	private GameObject m_doublePointsUI = null;
	[SerializeField]
	private GameObject m_timeSlowUI = null;
	[SerializeField]
	private Text m_counterDoublePointsText = null;
	[SerializeField]
	private Text m_counterTimeSlowText = null;
    [SerializeField]
    private Text m_scoreText = null;
	private bool m_pause = false;
	private WaveController m_waveController = null;
	private ShopController m_shopController = null;
    private BonusController m_bonusController = null;
    private SceneController m_sceneController = null;
    private Profile m_profile = null;
}
