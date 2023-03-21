
using UnityEngine;


public class EnemyGunMeneger : MonoBehaviour
{
    [SerializeField] int _healt;
    [SerializeField] GameObject _explosionGun;
    private GameObject go;
    private GameObject _bonus_up;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Material _blincMat;
    [SerializeField] Material _defaultMat;
    [SerializeField] GameObject[] _bonus_variant;
    public bool Is_bomb;
    public GameObject Bomb;

    void Start()
    {


        //_spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMat = _spriteRenderer.material;

    }



    public void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject go = collision.gameObject;

        TakeDamage(go);
    }
    void ResetMat()
    {
        _spriteRenderer.material = _defaultMat;
    }

    public void TakeDamage(GameObject go)
    {
        //Debug.LogError(go.tag);

        if (go.tag == "projectile")
        {
            _spriteRenderer.material = _blincMat;
            WeaponType typeDamage = go.GetComponent<projectile>().weapon_type;
            switch (typeDamage)
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


            ChekHealt();

        }
    }
    public void TakeDamageOnBomb()
    {
        _healt -= 15;
        ChekHealt();
    }

    void ChekHealt()
    {
        if (_healt <= 0)
        {

            go = Instantiate<GameObject>(_explosionGun);
            go.transform.position = transform.position;
            Destroy(go, 0.4f);
            int rnd = Random.Range(0, _bonus_variant.Length);
            _bonus_up = Instantiate(_bonus_variant[rnd]);
            _bonus_up.transform.position = transform.position;
            if (Is_bomb & Bomb) Bomb.GetComponent<BombExplorer>().ExplosionBomb();
            GameMeneger.S.DeadHelicopter();
            gameObject.SetActive(false);


            //Destroy(gameObject);


        }
        else
        {
            Invoke("ResetMat", .1f);
        }
    }
}
