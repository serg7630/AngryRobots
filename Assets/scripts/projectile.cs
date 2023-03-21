using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    none,
    minigun,
    grenade,
    bomb,
    EnemuHelicopter,
    EnemyBomb,
    Explosives


}
public class projectile : MonoBehaviour
{
    [Header("выбор оружия")]

    public WeaponType weapon_type=WeaponType.none;

    [SerializeField] GameObject projectileTreil;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float _speedBulet;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * _speedBulet);
    }

   
  public  void FixedUpdate()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") return;
        if (collision.gameObject.tag == "enemyBomb") collision.gameObject.GetComponent<projectileEnemyGun>().Explosive();
        
        GameObject go = Instantiate<GameObject>(projectileTreil);
        go.transform.position = transform.position;
        Destroy(go.gameObject, .2f);
        Destroy(this.gameObject);
    }
}
