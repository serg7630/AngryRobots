using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerMeneger : MonoBehaviour
{
    [SerializeField] int addShells;
    [SerializeField] float addHealt;
    [SerializeField] GameMeneger _gameMeneger;
    [SerializeField] int TakeDamageEnemyGun;
    [SerializeField] int TakeDamageEnemyBomb;
    [SerializeField] int TakeDamageExplosives;
 
    [SerializeField] float _max_health;
    [SerializeField] float _healt;
    [SerializeField] Image _imageHealthBar;
    [SerializeField] playerControl PlContr;
    public int Shells;
    [SerializeField] TMP_Text _shellsCount;

    void Start()
    {
        SetShells();
    }

   
    void Update()
    {
        _imageHealthBar.fillAmount = _healt / _max_health;
    }
    public void DamagPlayer()
    {

    }
    void SetShells()
    {
        string shellsSTR = "X  " + Shells.ToString();
        _shellsCount.text = shellsSTR;
        if (Shells>0)
        {
            PlContr._noShells = false;
        }
    }
    public void ShellsGet(GunId GunType)
    {
        switch (GunType)
        {
            case GunId.Minigun:
                Shells--;
                break;
            case GunId.Grenade:
                Shells -= 3;
                break;
            case GunId.Bomb:
                Shells -= 10;
                break;
        }
        if (Shells <= 0)
        {
            Shells = 0;
            PlContr._noShells = true;
        }
        SetShells();

    }
    public void AddBonus(BonusType bonusType)
    {
        switch (bonusType)
        {
            case BonusType.Bullet_up:
                Shells += addShells;
                SetShells();
                break;
            case BonusType.Healt_up:
                _healt += addHealt;
                if (_healt>_max_health)
                {
                    _healt = _max_health;
                }
                break;

        }

    }
    public void Takedamage(WeaponType typeEnemy)
    {
        switch (typeEnemy)
        {
            case WeaponType.EnemuHelicopter:
                _healt -= TakeDamageEnemyGun;
                break;
            case WeaponType.EnemyBomb:
                _healt -= TakeDamageEnemyBomb;
                break;
            case WeaponType.Explosives:
                _healt -= TakeDamageExplosives;
                break;
        }
        if (_healt<=0)
        {
            PlayerSetActiveFalse();
        }
    }

    public void PlayerSetActiveFalse()
    {
        if (_healt != 0) _healt = 0;
        
        _imageHealthBar.fillAmount = _healt / _max_health;
        ReloaderdestroyPlayer();
        _gameMeneger.DeadPlayer(this.gameObject);
        this.gameObject.SetActive(false);
    }
    public void ReloaderdestroyPlayer()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("enemyHelic");
        foreach (GameObject goEnemy in enemys)
        {
            //Debug.LogError("RealoadDestroy");
            goEnemy.GetComponent<Nearest>().ReloadPlayers(this.gameObject);
        }
        FollowCamFor_2_Players.OnePlayer = true;
    }
}
