
using UnityEngine;

public class BombExplorer : MonoBehaviour
{
    WeaponType weaponType = WeaponType.bomb;
    [SerializeField] TimerForBomb _TMB;
    [SerializeField] GameObject _projectileTreil;

    [SerializeField] Explorer2D _eplorer2D;
    GameObject go;
    GameObject _player;
    [SerializeField] bool PlayerIsBomb = false;
    Transform _playerpositionBomb;
    Rigidbody2D _RB;
    [SerializeField] float _force;
    bool _bombOnHelicopter;
    public MenegerBomb MB;
    [SerializeField] GameObject[] _players;  //масив с игроками для сбрасывания переменнной IsBomb;
    void Start()
    {
        _bombOnHelicopter = false;
        _RB = GetComponent<Rigidbody2D>();
        _players = GameObject.FindGameObjectsWithTag("Player");
    }

   
    void Update()
    {
        if (PlayerIsBomb) transform.position = _playerpositionBomb.position;
        if (transform.position.y <= -10f) ExplosionBomb();



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_bombOnHelicopter) return;
         go = collision.gameObject;
        if (go.tag=="Player")       //если это игрок, взять бомбу и запустить корутину
        {
            if (go == _player) return;
            if(_player!=null) _player.GetComponent<playerControl>().Is_bomb = false;
            //print("Player");
           
           if(!_TMB.ActiveCorutine) _TMB.StartCorutineExplosion();
            _TMB.ActiveCorutine=true;
             PlayerIsBomb = true;
            _playerpositionBomb = go.transform.Find("BombPosition");
            go.GetComponent<playerControl>().Is_bomb = true;
            go.GetComponent<playerControl>().Bomb = this.gameObject;
            _RB.mass = 0.0001f;
            _player = go;
            Invoke("ResertPlayer", 3f);
        }
        if (go.tag=="enemyHelic")       //если это враг
        {
            if (_player) _player.GetComponent<playerControl>().Is_bomb = false;
            _TMB.StartCorutineExplosion();
            _playerpositionBomb = go.transform.Find("BombPosition");
            _RB.mass = 0.0001f;
            go.GetComponent<EnemyGunMeneger>().Is_bomb = true;
            go.GetComponent<EnemyGunMeneger>().Bomb = this.gameObject;
            Debug.LogError("bomb");
            Invoke("ResertPlayer", 3f);
            _bombOnHelicopter = true;
        }
        
    }
    public void GetRidOfBomb()      //сбрасить бомбу с игрока нажатием вниз
    {
        PlayerIsBomb = false;
        Debug.LogError("ridBomb");
        _RB.AddForce(Vector2.up * _force);
        _RB.mass = 1;
        ResertPlayer();

    }
    void ResertPlayer()     
    {
        _player = null;
    }
    public void ExplosionBomb()     //взрыв бомбы
    {
        if (_player) _player.GetComponent<playerControl>().Is_bomb = false;
        GameObject go = Instantiate<GameObject>(_projectileTreil);
        MB.StartBomb();
        go.transform.position = transform.position;
        foreach (GameObject Player in _players)
        {
            Player.GetComponent<playerControl>().Is_bomb = false;
        }
        Destroy(go.gameObject, .2f);
        Destroy(this.gameObject);

    }
}
