
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenegerMainMenu : MonoBehaviour
{
    private GameMeneger GM;
    private void Awake()
    {
       
    }
    public void ButtomStartGame_1_Player()
    {
       GameMeneger.PlayerOfScene = 1;
        FollowCamFor_2_Players.OnePlayer = true;
        SceneManager.LoadScene("LevelMenu");
    }
    public void ButtomStartGame_2_Player()
    {
       GameMeneger.PlayerOfScene = 2;
        SceneManager.LoadScene("LevelMenu");
    }

}
