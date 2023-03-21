using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFired : MonoBehaviour
{
    [SerializeField] GameObject _haloEfect;
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] GameObject _projectile;
    [SerializeField] Vector2 _launcPos;
    [SerializeField] GameObject _point;
    [SerializeField] bool _aiming;
    [SerializeField] Rigidbody2D _RB_Proj;
    [SerializeField] GameObject _gunRotation;
    [SerializeField] float offset;
    [SerializeField] float _velosityMult;
    [SerializeField] bool _queueTry=true;
    [SerializeField] bool _gunActive;
    public float RateOfFire;

    
    [SerializeField] float minR;
    [SerializeField] float maxR;
 


    private void Start()
    {
        _haloEfect = GameObject.Find("halo");
        _haloEfect.SetActive(false);
        _launcPos = _haloEfect.transform.position;
        _point = GameObject.Find("point");
        _gunRotation = GameObject.Find("RotGun");
        
    }

    private void Update()
    {
        if (!_aiming) return;
        //Vector2 MousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        //Vector3 GunRotation = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //float rotationZ = Mathf.Atan2(GunRotation.y, GunRotation.x) * Mathf.Rad2Deg;
        ////Debug.LogError(rotationZ);
        //rotationZ = Mathf.Clamp(rotationZ, minR, maxR);
        //_gunRotation.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ+ offset);




        //float maxMagnitud = this.GetComponent<CircleCollider2D>().radius;
        //Vector2 mousDelta =MousPos- _launcPos;
        //if (mousDelta.magnitude>maxMagnitud)
        //{
        //    mousDelta.Normalize();
        //    mousDelta *= maxMagnitud;
        //}
        //Vector2 projectPos = _launcPos + mousDelta;
        
        //_projectile.transform.position = projectPos;
        //Debug.Log(projectPos);
        if (Input.GetMouseButtonUp(0))
        {
            
            _aiming = false;
           
            _RB_Proj.isKinematic = false;
            _projectile.transform.position = _point.transform.position;
            _projectile.transform.rotation = _point.transform.rotation;


            //_RB_Proj.velocity = -mousDelta * _velosityMult;
            _RB_Proj.AddForce(Vector2.right*_velosityMult);
            _projectile.GetComponent<CircleCollider2D>().isTrigger = false;
            _RB_Proj = null;
            _projectile = null;
            _queueTry = false;
            Invoke("SetQueue",RateOfFire);
        }
        


    }

    private void OnMouseDown()
    {
        if (_queueTry==false) return;
        if (_gunActive == false) return;
        _aiming = true;
        _projectile = Instantiate(_projectilePrefab) as GameObject;
        _projectile.transform.position = _launcPos;
        _RB_Proj = _projectile.GetComponent<Rigidbody2D>();
        _RB_Proj.isKinematic = true;
        _projectile.GetComponent<CircleCollider2D>().isTrigger = true;
        

    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
         GameObject go = collision.gameObject;
        if (go.tag=="Player")
        {
            _gunActive = true;
            //HeroKnight.S._gunEnter = true;
            _haloEfect.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag == "Player")
        {
            _gunActive = false;
            //HeroKnight.S._gunEnter = false;
            _haloEfect.SetActive(false);
        }
    }
    void SetQueue()
    {
        _queueTry = true;
    }
    public void FiredGun()
    {
        _aiming = false;

        _RB_Proj.isKinematic = false;
        _projectile.transform.position = _point.transform.position;
        _projectile.transform.rotation = _point.transform.rotation;


        //_RB_Proj.velocity = -mousDelta * _velosityMult;
        _RB_Proj.AddForce(Vector2.right * _velosityMult);
        _projectile.GetComponent<CircleCollider2D>().isTrigger = false;
        _RB_Proj = null;
        _projectile = null;
        _queueTry = false;
        Invoke("SetQueue", RateOfFire);
    }
}