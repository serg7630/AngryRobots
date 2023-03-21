using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenegerBomb : MonoBehaviour
{
    [SerializeField] GameObject _prefabBomb;
    [SerializeField] GameObject[] _bonusUpPrefabs;
    [SerializeField] GameObject _bombAsScene;
   
    void Start()
    {
        NewBonus();
    }

    public void StartBomb()
    {
        Invoke("NewBomb", 20f);
    }
    
    private void NewBomb()
    {
        if (_bombAsScene) return;
         _bombAsScene = Instantiate<GameObject>(_prefabBomb);
        _bombAsScene.GetComponent<BombExplorer>().MB = this;
        float RND_X = Random.Range(-7f, 17f);
        Vector2 bombPos = new Vector2(RND_X, 15f);
        _bombAsScene.transform.position = bombPos;
    }
    private void NewBonus()
    {
        int rnd = Random.Range(0, 2);
        GameObject bonus = Instantiate<GameObject>(_bonusUpPrefabs[rnd]);
        float RND_X = Random.Range(-7f, 17f);
        Vector2 bombPos = new Vector2(RND_X, 15f);
        bonus.transform.position = bombPos;
        Invoke("NewBonus", 30f);
    }
}
