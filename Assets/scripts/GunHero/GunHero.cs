using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunId
{
    None,
    Minigun,
    Grenade,
    Bomb
}
public class GunHero : MonoBehaviour
{
    public  GunHero S;

         public GunId GunType = GunId.None;
    [Header("��������� �����")]
        [SerializeField] bool _gunActive;
        [SerializeField] bool _queueTry;
        [SerializeField] float _velosityMult;
        [SerializeField] float RateOfFire;
        
        [SerializeField] float offset;
        //private float inputVertical;
        [SerializeField] GameObject _projectilePrefab;
     [Header("���������� �����")]
      
        
        [SerializeField] GameObject _haloEfect;
        public Transform _gunRotation;
        [SerializeField] GameObject _projectile;
        public Transform _point;
        [SerializeField] Quaternion _rotationZ;



    [SerializeField] AudioSource Turret_Fire;
    [SerializeField] AudioSource _noShells;

    [SerializeField] GunHero gunHero;
    [SerializeField] float _spreadMin;
    [SerializeField] float _spreadMax;

    
    
    
    
    
    

    void Start()
    {
        S = this;
        

        _haloEfect.SetActive(false);

       


        _queueTry = true;
       

    }

  
    void Update()
    {   

        if (!_gunActive) return;
         //inputVertical = Input.GetAxis("Vertical");

       
        //Turret_move.Play();

        //print(_gunRotation.eulerAngles.z);
        //Debug.LogError(_gunRotation.transform.root);
    }

    public void Rotate(float verInput)
    {
        verInput *= offset;
        this._gunRotation.Rotate(0, 0, verInput);
    }
    public void FiredGun(GameObject Player)
    {
        
        if (_queueTry)
        {
            playerControl PC = Player.GetComponent<playerControl>();
            if (PC._noShells)
            {
                NoSheels();
                _queueTry = false;
                Invoke("SetQueue", 1f);
                return;
            }

           Player.GetComponent<PlayerMeneger>().ShellsGet(GunType);

            Vector3 rotation_projectile = _gunRotation.eulerAngles;
            rotation_projectile.z+=Random.Range(_spreadMin, _spreadMax);

            
            _projectile = Instantiate(_projectilePrefab, _point.position, Quaternion.Euler(rotation_projectile));  //создание снаряда
            Turret_Fire.Play();

            

            //print(_projectile.transform.rotation);
            //print(_point.transform.root);           
            //Debug.LogError(GunType);
 
            _projectile = null;
            _queueTry = false;
            Invoke("SetQueue", RateOfFire);
        }

       
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;

        if (go.tag == "Player"&& !go.GetComponent<playerControl>().Is_bomb)
        {
            _gunRotation = null;
            _point = null;
            go.GetComponent<playerControl>().GunActive = false;
            _gunActive = false;
            _haloEfect.SetActive(false);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag == "Player"&&!go.GetComponent<playerControl>().Is_bomb)
        {
            _point = transform.Find("RotGun/pixil-frame-0/point");
            _gunRotation = transform.Find("RotGun");
            go.GetComponent<playerControl>().GunActive = true;
            _gunActive = true;
            _haloEfect.SetActive(true);
            //enemy_Gun.enabled = false;
        }
    }
    
   
  
   
   
    void NoSheels()
    {
        _noShells.Play();
    }
    void SetQueue()
    {
        _queueTry = true;
    }
}
