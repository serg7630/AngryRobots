using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamFor_2_Players : MonoBehaviour
{
    public static bool OnePlayer = false;

    public static bool PlayGame = true;

    public GameObject followObject;
    [SerializeField] GameObject POI_2;
    [SerializeField] GameMeneger GM;
    [SerializeField] float DistancesOfPlayer_X;

    [SerializeField] GameObject Player_1=null;
    [SerializeField] GameObject Player_2=null;

    [SerializeField] Transform startPosition;
    [SerializeField] Transform lastPosition;

    [SerializeField] float _distances;

    public Vector2 followOffset;
    public float speed = 3f;
    private Vector2 threshold;
    [SerializeField] Vector2 follow;



    // Start is called before the first frame update
    void Start()
    {
        Player_1 = GM.Players[0];
        Player_2 = GM.Players[1];
        threshold = calculateThreshold();
        //followObject = Player_1;
        if (GM.Players[1] == null)
        {
            OnePlayer = true;
        }
        else
        {
            OnePlayer = false;
        }
        print(PlayGame);
        print(OnePlayer);

        startPosition = GameObject.FindGameObjectWithTag("startPosition").transform;
        lastPosition = GameObject.FindGameObjectWithTag("lastPosition").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PlayGame) return;

        if (Player_1.activeSelf)
        {
             follow = Player_1.transform.position;
            //followObject = Player_2;
        }
        else
        {
             follow = Player_2.transform.position;
            //followObject = Player_1;
        }

        

        if (!OnePlayer)
        {
            
            if (Player_1.transform.position.x>Player_2.transform.position.x)
            {
                followObject = Player_1;
                follow = Player_1.transform.position;
                POI_2 = Player_2;
            }
            else
            {
                followObject = Player_2;
                follow = Player_2.transform.position;
                POI_2 = Player_1;
            }

            Vector2 follow_POI = POI_2.transform.position;

            float distancesX = follow.x - follow_POI.x;


            if (distancesX >= DistancesOfPlayer_X)
            {
                follow.x = follow_POI.x + DistancesOfPlayer_X;
                followObject.transform.position = follow;

            }



        }

        //print(follow);
       
        
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);
        

        Vector3 newPosition = transform.position;
        if (Mathf.Abs(xDifference) >= threshold.x)
        {
            newPosition.x = follow.x;
        }
        if (Mathf.Abs(yDifference) >= threshold.y)
        {
            newPosition.y = follow.y;
        }


        if (newPosition.x<startPosition.position.x)
        {
            newPosition.x = startPosition.position.x;
        }

        if (newPosition.x > lastPosition.position.x)
        {
            newPosition.x = lastPosition.position.x;
        }

        if (newPosition.y < 0|| newPosition.y > 0)
        {
            newPosition.y = 0;
        }

        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
       
        
        
    }
    private Vector3 calculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}
