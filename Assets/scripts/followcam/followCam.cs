using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCam : MonoBehaviour
{
    [SerializeField] GameObject Player_1;
    [SerializeField] GameObject Player_2;
    [SerializeField] GameMeneger GM;
    [SerializeField] GameObject POI=null;
    [SerializeField] GameObject POI_2=null;

    [SerializeField] float camMinX = 0f;
    [SerializeField] float camMinY = 0f;
    [SerializeField] float camZ;

    void Start()
    {
        camZ = Camera.main.transform.position.z;
        
            Player_1 = GM.Players[0];
            Player_2 = GM.Players[1];
    }

    
    void Update()
    {
        if (Player_2!=null)
        {
            if (Player_1.transform.position.x > Player_2.transform.position.x)
            {
                POI = Player_1;
                POI_2 = Player_2;
            }
            else
            {
                POI = Player_2;
                POI_2 = Player_1;
            }
        }
       

        Vector3 camPos = POI.transform.position;
        camPos.z = camZ;

        camPos.x = Mathf.Max(camMinX, POI_2.transform.position.x);
        camPos.y = Mathf.Max(camMinY, POI_2.transform.position.y);

        transform.position = camPos;




    }
}
