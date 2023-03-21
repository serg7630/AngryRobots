using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagroundManeger : MonoBehaviour
{
  public static  BagroundManeger S;
    public bool GamePause = false;
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            if (!GamePause)
            {
                Time.timeScale = 1;
                Debug.LogError("Pause");
            }
        }
    }
    public void OnApplicationFocus(bool focus)
    {
        Debug.LogError(GamePause);
        if (focus)
        {
            if (GamePause==true)
            {
                return;
            }
            Time.timeScale = 1;
            GameMeneger.S.BagroundSoundPlay();
            Debug.Log("focus true");

        }
        else
        {
            GameMeneger.S.BagroundSoundPause();
            Time.timeScale = 0;
            Debug.Log("focus false");
        }
    }
    void Start()
    {
        S = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
