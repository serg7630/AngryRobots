using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocDestroyd : MonoBehaviour
{
    [SerializeField] int _healt;
    [SerializeField] Sprite damaged;
    [SerializeField] bool damagActive;
    [SerializeField] bool firstSprit;

    void Start()
    {
        damagActive = true;

    }

    
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!damagActive) return;
        GameObject go = collision.gameObject;
        if (go.tag=="projectile")
        {
            damagActive = false;
            Invoke("InvoceDamag",0.3f);

            WeaponType typeDamag = go.GetComponent<projectile>().weapon_type;

            switch (typeDamag)
            {
                case WeaponType.minigun:
                    _healt -= 1;
                    break;
                case WeaponType.grenade:
                    _healt -= 3;
                    break;
                case WeaponType.EnemuHelicopter:
                    _healt -= 8;
                    break;
            }

            
        }
        if ( go.tag == "ProjectileGun")
        {
            damagActive = false;
            Invoke("InvoceDamag", 0.3f);

            WeaponType typeEnemy = go.GetComponent<projectileEnemyGun>().enemy_type;
            switch (typeEnemy)
            {
                case WeaponType.EnemuHelicopter:
                    _healt -= 8;
                    break;

            }
        }

        if (_healt <= 6 && firstSprit)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = damaged;
            firstSprit = false;

        }
        else if (_healt <= 0)
        {
            Destroy(gameObject);
        }
    }
    void InvoceDamag()
    {
        damagActive = true;
    }
   
}
