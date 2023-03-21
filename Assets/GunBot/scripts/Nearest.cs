using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Nearest : MonoBehaviour
{

    [SerializeField] GameObject[] arreyPlayers;
    [SerializeField] List<GameObject> Players = new List<GameObject>();
    GameObject closest;

    public GameObject nearest;

    void Start()
    {
        arreyPlayers = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < arreyPlayers.Length; i++) //добавление игроков в масив
        {
            Players.Add(arreyPlayers[i]);
        }

        GetNearset();
    }

    GameObject FindClosestEnemy()  //Поиск ближфйшего игрока
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in Players)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    void Update()
    {
        //nearest = FindClosestEnemy().gameObject;
        //print(nearest);
    }
    void GetNearset()
    {
        nearest = FindClosestEnemy().gameObject;
        Invoke("GetNearset", 2f);
    }
    public void ReloadPlayers( GameObject player)
    {
        //arreyPlayers = GameObject.FindGameObjectsWithTag("Player");
        Players.Remove(player);
        
        if (Players.Count==0)
        {
            Debug.LogError("nullPlayers");
            NullPlayer();
        }
    }
    void NullPlayer()
    {
        GameObject[] guns = GameObject.FindGameObjectsWithTag("enemyHelic");
        print(guns.Length);
        foreach (GameObject gun in guns)
        {
            gun.GetComponent<avtoSistemGun>().NullPlayer = true;
            gun.GetComponent<avtoSistemGun>().GunActiveFalse();
        }
    }
}