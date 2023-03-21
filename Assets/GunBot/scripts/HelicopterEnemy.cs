using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterEnemy : MonoBehaviour
{
   [SerializeField] GameObject trigerGamobject = null;

    [SerializeField] Sprite _activeEnemy;
    [SerializeField] Sprite _idleEnemy;

    [SerializeField] GameObject _leftGun;
    [SerializeField] GameObject _rightGun;

    [SerializeField] GameObject _helicEnemy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go==trigerGamobject)
        {
            return;
        }
        trigerGamobject = go;
        if (go.tag=="Player")
        {
            Debug.LogError("HEnemyActive");
            _helicEnemy.GetComponent<SpriteRenderer>().sprite = _activeEnemy;
            GunActive();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag=="Player"&&trigerGamobject!=null)
        {
            trigerGamobject = null;
            Debug.LogError(":HelEnemyExit");

            Invoke("GunFalseActive", 2f);
        }
    }

    public void GunActive()
    {
        _leftGun.SetActive(true);
        _rightGun.SetActive(true);
    }
    public void GunFalseActive()
    {
        _helicEnemy.GetComponent<SpriteRenderer>().sprite = _idleEnemy;
        _leftGun.SetActive(false);
        _rightGun.SetActive(false);
    }
}
