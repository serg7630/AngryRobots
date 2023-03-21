using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.InteropServices;

public class GameMeneger : MonoBehaviour
{
    public static GameMeneger S;


    [SerializeField] GameObject _buttomPauseUI;
    [SerializeField] GameObject _panelGameOver;
    [SerializeField] GameObject _panelFinish;
    [SerializeField] GameObject _panelPause;
    [SerializeField] GameObject MobileJoystic;
    [SerializeField] GameObject PauseOnStart;
    [SerializeField] GameObject PanelPlayer_2;

    //[SerializeField] GameObject SoundBaground;
    [SerializeField] AudioSource _bagroundSource;
    [SerializeField] bool _miusicInGame;
    [Header("поле с уровнем при паузе")]
    [SerializeField] TMPro.TextMeshProUGUI _levelName;
    //[Header("подсчет кристалов")]
    //[SerializeField] TMPro.TextMeshProUGUI CoinMeheger;
    int Coins;
    bool FinishActive;
    bool GameOverActive;
    public string ActiveScene;
    [SerializeField] string MaxScene;
    private BagroundManeger BM;

    [Header("PlayerPrefs")]
    [SerializeField] int _availibleLevels;
    string KeyStringLevelsAvaileble = "KeyLevels";
   [SerializeField] int _getLevel;
    [SerializeField] AudioSource[] _bagroundSound;
    public static int PlayerOfScene;

    [SerializeField] GameObject _buttomyPause;
    public GameObject[] Players;

    [SerializeField] GameObject[] _enemyGunHelicopters;
    [SerializeField] TMPro.TextMeshProUGUI _numEnemyHelicopter;
    private int _numHelicopter;

    private bool LevelPlusDuble;

                                //реклама яндекс
    [DllImport("__Internal")]
    private static extern void ShowAds();

    [DllImport("__Internal")]       //вывод сообщения в консоли браузера
    private static extern void ShowAdsAlert();

    private float timeShowAdsStatic;
    [SerializeField] float TimeForAds;
    public static float RealTime;
    public static bool ShowAdsTrue;
    void Awake()
    {
        _enemyGunHelicopters = GameObject.FindGameObjectsWithTag("enemyHelic");

        _buttomPauseUI = GameObject.Find("Button_Pause");
        
        _getLevel = PlayerPrefs.GetInt(KeyStringLevelsAvaileble);
        BM = FindObjectOfType<BagroundManeger>();
        
        S = this;
        //PauseOnStart = GameObject.Find("Panel_pause_ON_ADS");
        if(PauseOnStart!=null)PauseOnStart.SetActive(false);
        int RND = Random.Range(0, _bagroundSound.Length);
        //BagroundSound[RND].Play();
        int IndexScene = SceneManager.GetActiveScene().buildIndex;
        print(IndexScene);
        if (IndexScene>=2)
        {
            if (PlayerOfScene==1)
            {
                Players[1].SetActive(false);
                PanelPlayer_2.SetActive(false);
                Players[1] = null;
                //Debug.LogError("setActivePlayer_2");

            }
            else
            {
                
            }

        }
        
        _numHelicopter = _enemyGunHelicopters.Length;
        _numEnemyHelicopter.text = "X " + _numHelicopter;

       
        LevelPlusDuble = false;

        //Debug.LogError(ShowAdsStatic.S.TimeGame);
        ShowAdsInGame();

        if (RealTime == 0) RealTime = -TimeForAds;
    }

   public void ShowAdsInGame()
    {

        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            
            

            if (ShowAdsStatic.ADSActive)
            {
                //Debug.LogError(RealTime + TimeForAds);
                ShowAdsTrue = false;
                //ShowAdsAlert();
                
                Debug.LogError("ShowAds();");
                
                ShowAdsStatic.ADSActive = false;
                RealTime = ShowAdsStatic.S.TimeGame;
                //Pause();
                _bagroundSource.Pause();
                ShowAds();
                PauseOnStarts();
               
                //print(ShowAdsStatic.S.TimeGame);
            }
        }
    }

    public void MenegerVoid(string Void = "")
    {
        

         Coins = 0;



        switch (Void)
        {
            case "GameOver":
                Invoke("GameOver", 0.5f);
                if (_buttomPauseUI.activeInHierarchy)
                {
                    _buttomPauseUI.SetActive(false);
                }
                
                break;
            case "Restart":
                if (Time.timeScale < 1) Time.timeScale = 1;
                //PlayerMove.S.MoveForward = 0;
                _panelPause.SetActive(false);
                _buttomPauseUI.SetActive(false);
                Invoke("Restart", 1f);
                break;
            case "GameFinish":
                Invoke("GameFinish", 2f);
                _buttomPauseUI.SetActive(false);
                break;
        }
    }
    //загрузка с меню уровней
    //public void LoadSceneInMenuLevel(string NameScene)
    //{
    //    Debug.LogError(NameScene);
    //    SceneManager.LoadScene(NameScene);
       
    //}
    //проигрыш
    public void GameOver()
    {
        //if (MobileJoystic.activeInHierarchy == true) MobileJoystic.SetActive(false);
        if (Time.timeScale != 1) Time.timeScale = 1;
        
        if (FinishActive) return;
        FollowCamFor_2_Players.PlayGame = false;
        FollowCamFor_2_Players.OnePlayer = false;
        _panelGameOver.SetActive(true);
        GameOverActive = true;
        //BagroundSource.Pause();
        _buttomyPause.SetActive(false);
        //PlayerMove.S.HideMobileJoystic();
    }
    //рестарт
    public void Restart()
    {

        //StaticValueShowAds.S.PlusShowAdsVal();
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        FollowCamFor_2_Players.OnePlayer = false;
        FollowCamFor_2_Players.PlayGame = true;
        if (Time.timeScale != 1) Time.timeScale = 1;

        //ShowAds();      //реклама яндекс
        //ShowAdsAlert();
    }

    //финиш и победа
    public void GameFinish()
    {
        if (Time.timeScale != 1) Time.timeScale = 1;
      
        _bagroundSource.Pause();
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i]!=null)
            {
                Players[i].GetComponent<playerControl>().enabled=false;
            }
            
        }

        if (GameOverActive) return;
        _panelFinish.SetActive(true);
        FinishActive = true;
         ActiveScene = SceneManager.GetActiveScene().name;
        LevelPlus(ActiveScene);
        _buttomyPause.SetActive(false);
        
    }
    //пауза
    public void Pause()
    {
        //string ActiveScene = SceneManager.GetActiveScene().name;
        //LevelName.text = ActiveScene;
        _panelPause.SetActive(true);
        //if (PlayerMove.S.MobilePlatform==true)
        //{
        //    PlayerMove.S.HideMobileJoystic();
        //}
        Time.timeScale = 0f;
        BM.GamePause = true;
        BM.OnApplicationFocus(false);
        //ButtomyJump.SetActive(false);
        _bagroundSource.Pause();
    }
    //снятие с паузы
   public void continium()
    {
        _bagroundSource.Play();
        _panelPause.SetActive(false);
        //if (PlayerMove.S.MobilePlatform == true)
        //{
        //    PlayerMove.S.ShoweMobileJoystic();
        //}
        Time.timeScale = 1;
        _buttomyPause.SetActive(true);
        BM.GamePause = false;    }

    //выход в меню уровней
    public void loadSceneLevels()
    {
        LevelPlus(ActiveScene);
        if (Time.timeScale < 1) Time.timeScale = 1;
        SceneManager.LoadScene("LevelMenu");

    }
   

    //увеличение доступных уровней при победе
    public void LevelPlus(string ActiveScene)
    {
        if (LevelPlusDuble) return;
        LevelPlusDuble = true;
        getLastLevel();
        _availibleLevels = _getLevel;
         MaxScene = LevelMenu.MaxScene;
        Debug.Log(MaxScene);
        Debug.Log(ActiveScene);
        Debug.Log(_availibleLevels);
        Debug.Log(_getLevel);
        Debug.Log(LevelMenu.LevelAvaileble);
        if (ActiveScene == LevelMenu.MaxScene)
        {
            _availibleLevels++;
            SetLevels();
        }

    }
    public void ContiniumVictory()
    {

    }

    //запуск последнего уровня
    public void StartTheLastLevel()
    {
        int Lev = _getLevel + 1;
        if (Lev==1)
        {
            Lev = 2;
        }
        if (Lev==SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("allScenes");
            Lev--;
        }
        if (Time.timeScale < 1) Time.timeScale = 1;
        //StaticValueShowAds.ShowAds = true;
        //StaticValueShowAds.ValForAds = 1;

        SceneManager.LoadScene(Lev);
    }
    // получаем последний уровень
    public void getLastLevel()
    {
        _getLevel = PlayerPrefs.GetInt(KeyStringLevelsAvaileble);
        if (_getLevel == 0)
        {
            _getLevel = 1;
        }
    }

    //устанавливаем данные в преферс
    public void SetLevels()
    {
        PlayerPrefs.SetInt(KeyStringLevelsAvaileble, _availibleLevels);
        PlayerPrefs.Save();
    }
    //сброс преферса только для теста
    public void PlayerPreferReset()
    {
        _availibleLevels = 1;
        PlayerPrefs.SetInt(KeyStringLevelsAvaileble,_availibleLevels);
        PlayerPrefs.Save();
        _getLevel = 0;
    }
    //public void CoinMeneger()
    //{
    //    Coins++;
    //    CoinMeheger.text = Coins.ToString();
    //}
    public void DeadPlayer(GameObject Player)
    {
        int NumPlayer=0;
        for (int i = 0; i < Players.Length; i++)
        {
            if (Player==Players[i])
            {
                Players[i] = null;

            }
            if (Players[i]==null)
            {
                NumPlayer++;
            }
        }
        if (NumPlayer==2)
        {
            GameOver();
        }

    }
    
   
    public void PauseOnStarts()
    {
        if (SceneManager.GetActiveScene().buildIndex==0)
        {
            return;
        }
        Time.timeScale = 0f;
        //ShowAds();
        _buttomyPause.SetActive(false);
        Debug.Log("tryPause");
        BM.GamePause = true;
        if (PauseOnStart==null)
        {
            Invoke("PauseOnStarts", 0.5f);
            return;
        }
        Debug.Log("pauseActive");
        PauseOnStart.SetActive(true);
        _buttomPauseUI.SetActive(false);
        
        //StaticValueShowAds.ShowAds = false;

    }
    public void ExitPauseOnStarts()
    {
        PauseOnStart.SetActive(false);
        Time.timeScale = 1f;
        _buttomyPause.SetActive(true);
        _buttomPauseUI.SetActive(true);
        BM.GamePause = false;
        _bagroundSource.Play();

    }


    void Update()
    {
       

        timeShowAdsStatic= ShowAdsStatic.S.TimeGame = Time.realtimeSinceStartup;
        if (RealTime + TimeForAds < timeShowAdsStatic)
        {
            ShowAdsStatic.ADSActive = true;
            ShowAdsTrue = true;
        }
        else
        {
            ShowAdsStatic.ADSActive = false;
            ShowAdsTrue = false;
        }
        //print(timeShowAdsStatic);
        //Debug.LogError(RealTime);
        if (PauseOnStart!=null&& PauseOnStart.activeInHierarchy)
        {
            _bagroundSource.Pause();
        }
    }
  
    public void ButtonMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void BagroundSoundPause()
    {
        _bagroundSource.Pause();
    }
    public void BagroundSoundPlay()
    {
        _bagroundSource.Play();
    }
    public void DeadHelicopter()
    {
        _numHelicopter--;
        if (_numHelicopter==0)
        {
            GameFinish();
        }
        _numEnemyHelicopter.text = "X " + _numHelicopter;
    }

}
