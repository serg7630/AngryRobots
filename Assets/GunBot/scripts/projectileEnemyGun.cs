using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum EnemyType
//{
//    none,
//    helicopter,
//    liner,
//    tank
//}

public class projectileEnemyGun : MonoBehaviour
{
    
    [Header("выбор Врага")]

    public WeaponType enemy_type = WeaponType.none;

    [SerializeField] GameObject projectileTreil;
    //[SerializeField] Rigidbody2D rb;
    //[SerializeField] float _speedBulet;


    void Start()
    {
        if (enemy_type == WeaponType.EnemyBomb) Invoke("Explosive", 1.5f);
    }


    public void FixedUpdate()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemy_type == WeaponType.EnemyBomb) return;
        if (collision.gameObject.tag == "Player")
        {

            GameObject Player = collision.gameObject;
            Player.GetComponent<PlayerMeneger>().Takedamage(enemy_type);

        }
        

        //print(collision.gameObject.name);
        GameObject go = Instantiate<GameObject>(projectileTreil);

        go.transform.position = transform.position;
        Destroy(go.gameObject, .2f);
        Destroy(this.gameObject);
    }
   public void Explosive()
    {
        //Debug.LogError("explorer");
        GetComponent<Explorer2D>().Explosion2D(this.transform.position);
        GameObject go = Instantiate<GameObject>(projectileTreil);

        go.transform.position = transform.position;
        Destroy(go.gameObject, .2f);
        Destroy(this.gameObject);
    }
}
