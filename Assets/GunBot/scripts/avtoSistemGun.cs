using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avtoSistemGun : MonoBehaviour
{
   [SerializeField] WeaponType _enemy_type = WeaponType.none;
    [SerializeField] float _max_speed;
    [SerializeField] float _min_speed;
    private float realSpeed;
    [SerializeField] float _range;
    [SerializeField] GameObject _target;
    [SerializeField] Transform _moveGunPosition;
    [SerializeField] bool _detected;
    public bool NullPlayer=false;
    [SerializeField] GameObject _helicopter;

    [SerializeField] Vector2 _directions;

    

    [Header("              Стрельба")]
    [SerializeField] float _shootingRange;
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] GameObject _projectile;
    [SerializeField] float _timeShooting;
    private float nextTimeShoot=0;
    [SerializeField] Transform[] PointGun;
    [SerializeField] int NumGun;
    [SerializeField] float SpeedProjectile;
    [SerializeField] AudioSource _helicopterFire;


    [Header("             Визуал врага")]
    [SerializeField] Sprite _activeEnemy;
    [SerializeField] Sprite _idleEnemy;

    [SerializeField] GameObject _leftGun;
    [SerializeField] GameObject _rightGun;

    

    void Start()
    {
        NumGun = 0;
        GetTarget();
        nextTimeShoot = Time.time + 1 / _timeShooting;
         realSpeed = Random.Range(_min_speed, _max_speed);
        
    }

    
    void Update()
    {
        if (NullPlayer) return;
        GetTarget();
        if (!_target)
        {

            _target = GetComponent<Nearest>().nearest;
            GunActive();
            return;
        }
        Vector2 targetPos = _target.transform.position;

        _directions = targetPos - (Vector2)transform.position;
        if (_enemy_type == WeaponType.EnemyBomb) _directions.y += 1f;

        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, _directions, _range);

       float distance= Vector2.Distance(_target.transform.position, transform.position);
        

        if (distance<=_range/*&&distance>=_shootingRange*/)
        { 

            
            if (_target.gameObject.tag=="Player")//движение к игроку
            {
                Vector2 HelicopterPos = transform.position;
               HelicopterPos = Vector2.MoveTowards(this.transform.position, _moveGunPosition.position, realSpeed * Time.deltaTime);
                if (HelicopterPos.y>=4)
                {
                    HelicopterPos.y = 4f;
                }
                RotateGun();

                transform.position = HelicopterPos;
                Shoot();


                if (!_detected)//активация орудия
                {
                    _detected = true;
                    
                    //print(transform.position);
                    //print("Player" + _target.transform.position);
                    
                    GunActive();

                }
            
            
            }
        }
        //else if (distance <= _shootingRange)
        //{       
        //    Shoot();
        //    RotateGun();
        //    print("shRange");
            
        //}
        else
        {
                if (_detected)//состояние покоя
                {
                    _detected = false;

                    GunActiveFalse();
                }
        }  
    }

    public void RotateGun()
    {
        _leftGun.transform.right = _directions;
        _rightGun.transform.right = _directions;
    }
    public void Shoot()
    {
        if (Time.time > nextTimeShoot)//стрельба
        {
            nextTimeShoot = Time.time + 1 / _timeShooting;
            NumGun++;
            if (NumGun>=PointGun.Length)
            {
                  NumGun = 0;
            }
               GameObject _projectile=  Instantiate(_projectilePrefab, PointGun[NumGun].position, Quaternion.identity);
            _directions.Normalize();

            if (_enemy_type==WeaponType.EnemyBomb)
            {
                _projectile.GetComponent<Rigidbody2D>().velocity = _directions * SpeedProjectile/*/40f*/;
                Destroy(_projectile, 3f);
            }
            else
            {
                _projectile.GetComponent<Rigidbody2D>().velocity = _directions * SpeedProjectile;
                Destroy(_projectile, 2f);
            }
            //_projectile.GetComponent<Rigidbody2D>().AddForce(_directions * SpeedProjectile);
            
            _helicopterFire.Play();
        }

       
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
    public void GunActive()
    {
        //Debug.LogError("GunActive");
        _helicopter.GetComponent<SpriteRenderer>().sprite = _activeEnemy;
        _leftGun.SetActive(true);
        _rightGun.SetActive(true);
    }
    public void GunActiveFalse()
    {
        //Debug.LogError("GunFalseActive");
        _leftGun.SetActive(false);
        _rightGun.SetActive(false);
        _helicopter.GetComponent<SpriteRenderer>().sprite = _idleEnemy;
    }
    public void GetTarget()
    {
        if (GetComponent<Nearest>().nearest)
        {
            _target = GetComponent<Nearest>().nearest;
            if (_enemy_type==WeaponType.EnemyBomb)
            {
                _moveGunPosition = _target.transform.Find("GunPositionBomb").transform;
            }
            else
            {
                _moveGunPosition = _target.transform.Find("GunPosition").transform;
            }
            
        }
        else
        {
            GunActiveFalse();
        }

        //Invoke("GetTarget", 0.2f);
        //print("GetTarget");
    }
}
