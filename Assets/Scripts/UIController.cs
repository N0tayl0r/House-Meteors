using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void OnRoofDestroyed()
    {
        // game over
        m_menuBackground.SetActive(true);
        m_gameOverScreen.SetActive(true);
    }

    public void ClearProfile() // Aka "New Game"
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    public void OnUIMenuButtonClick()
    {
        m_pause = !m_pause;
        if (m_back == ShopBackVariantsEnum.MainMenu)
        {
            if (m_pause)
            {
                m_startWaveButton.SetActive(false);
                Time.timeScale = 0;
                m_menuBackground.SetActive(true);
                m_mainMenu.SetActive(true);
            }
        }
        else if (m_back == ShopBackVariantsEnum.PauseMenu)
        {
            if (m_pause)
            {
                Time.timeScale = 0;
                m_menuBackground.SetActive(true);
                m_pauseMenu.SetActive(true);
            }
        }
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void OnMainMenuContinueButtonClick()
    {
        m_pause = false;
        m_menuBackground.SetActive(false);
        m_mainMenu.SetActive(false);
        m_startWaveButton.SetActive(true);
        Time.timeScale = 1;
    }

    public void OnMainMenuShopButtonCLick()
    {
        m_mainMenu.SetActive(false);
        m_shopMenu.SetActive(true);
        m_back = ShopBackVariantsEnum.MainMenu;
    }

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
        m_back = ShopBackVariantsEnum.PauseMenu;
	}

    public void OnShopBackButtonClick()
    {
		m_shopMenu.SetActive(false);

        if(m_back == ShopBackVariantsEnum.MainMenu)
        {
            m_mainMenu.SetActive(true);
        }
        else
        {
            m_pauseMenu.SetActive(true);
        }
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
            m_sceneController.EventScoreChanged -= OnScoreChanged;
            m_sceneController.EventRoofDestroyed -= OnRoofDestroyed;
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
        m_scoreText.text = "Score: " + m_profile.CurrentScoreIndex;

        m_shopController.EventBonusBought += OnBonusCountChanged;
        m_bonusController.EventBonusCountIncreased += OnBonusCountChanged;
        m_bonusController.EventBonusCountDecreased += OnBonusCountChanged;
        m_sceneController.EventScoreChanged += OnScoreChanged;
        m_sceneController.EventRoofDestroyed += OnRoofDestroyed;
    }

	void Update()
	{
        if (!m_waveController.WaveInProcess)
        {
            m_back = ShopBackVariantsEnum.MainMenu;
        }
        else
        {
            m_back = ShopBackVariantsEnum.PauseMenu;
        }

        //if (m_mainMenu.activeSelf)
        //{
        //    return;
        //}

        if (Input.GetKeyDown(KeyCode.Escape))
		{
            m_pause = !m_pause;
            if (m_back == ShopBackVariantsEnum.MainMenu)
            {
                if (m_pause)
                {
                    m_startWaveButton.SetActive(false);
                    Time.timeScale = 0;
                    m_menuBackground.SetActive(true);
                    m_mainMenu.SetActive(true);
                }
                else
                {
                    m_startWaveButton.SetActive(true);
                    Time.timeScale = 1;
                    m_menuBackground.SetActive(false);
                    m_mainMenu.SetActive(false);
                }
            }
            else if (m_back == ShopBackVariantsEnum.PauseMenu)
            {
                if (m_pause)
                {
                    Time.timeScale = 0;
                    m_menuBackground.SetActive(true);
                    m_pauseMenu.SetActive(true);
                }
                else
                {
                    Time.timeScale = 1;
                    m_menuBackground.SetActive(false);
                    m_pauseMenu.SetActive(false);
                }
            }
		}

	}

	[SerializeField]
	private GameObject m_startWaveButton = null;
	[SerializeField]
	private GameObject m_pauseMenu = null;
    [SerializeField]
    private GameObject m_mainMenu = null;
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
    [SerializeField]
    private GameObject m_gameOverScreen = null;
	private bool m_pause = false;
    private bool m_mainMenuFlag = false;
	private WaveController m_waveController = null;
	private ShopController m_shopController = null;
    private BonusController m_bonusController = null;
    private SceneController m_sceneController = null;
    private Profile m_profile = null;
    private ShopBackVariantsEnum m_back = ShopBackVariantsEnum.MainMenu;
}
