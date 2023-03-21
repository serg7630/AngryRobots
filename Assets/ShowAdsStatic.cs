using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ShowAdsStatic : MonoBehaviour
{
    public static ShowAdsStatic S;
    
    public float TimeGame;
    public static bool ADSActive;

    
    void Start()
    {
        S = this;
        ADSActive = GameMeneger.ShowAdsTrue;
        Debug.LogError(ADSActive);
    }

   
   public  void Update()
    {
        //print( TimeGame);
        TimeGame = Time.realtimeSinceStartup;
    }
}
