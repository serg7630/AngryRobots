using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.InteropServices;

public class buttomEnterLevel : MonoBehaviour
{

    //реклама яндекс
    [DllImport("__Internal")]
    private static extern void ShowAds();

    [DllImport("__Internal")]       //вывод сообщения в консоли браузера
    private static extern void ShowAdsAlert();

    void Start()
    {
        
    }

   public void EnterLevel()
    {   
        string nameButtom = gameObject.name;
        if (Time.timeScale != 1) Time.timeScale = 1;

        //ShowAds();
        //ShowAdsAlert();
        //StaticValueShowAds.ShowAds = true;
        //StaticValueShowAds.ValForAds = 1;

        Debug.LogError(nameButtom);
        SceneManager.LoadScene(nameButtom);
        
    }
   
   
}
