using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BonusType
{
    None,
    Bullet_up,
    Healt_up,


}
public class bonus_up : MonoBehaviour
{
    public BonusType BonusType = BonusType.None;
    GameObject colisionGameobject = null;
    [SerializeField] GameObject SoundBonusUp;
    [SerializeField] GameObject _explosionBonusBox;
    [SerializeField] GameObject go;
    private bool _active;

    void Start()
    {
        _active = false;
        Invoke("ActiveBonus", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        if (go==colisionGameobject)
        {
            return;
        }
        colisionGameobject = go;
        if (go.tag=="Player")
        {
            go.GetComponent<PlayerMeneger>().AddBonus(BonusType);
            GameObject SG = Instantiate<GameObject>(SoundBonusUp);
            Destroy(SG, 1f);
            Destroy(this.gameObject);
        }
        if (go.tag=="projectile" ||go.tag=="ProjectileGun")
        {
            Explorer();
            
        }
    }
    private void ActiveBonus()
    {
        _active = true;
    }
    public void Explorer()
    {
        if (_active)
        {
            //GetComponent<Explorer2D>().Explosion2D(this.transform.position);
            go = Instantiate<GameObject>(_explosionBonusBox);
            go.transform.position = transform.position;
            Destroy(go, 0.4f);
            Destroy(this.gameObject);
        }
    }
}
