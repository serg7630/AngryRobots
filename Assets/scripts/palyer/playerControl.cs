
using UnityEngine;

public class playerControl : MonoBehaviour
{
    [SerializeField] string IDInputHorizont;
    [SerializeField] string IDInputVertical;
    [SerializeField] string IDJumpInput;

    [SerializeField] AudioSource _jumpSource;

    [SerializeField] float speed;
    [SerializeField] Rigidbody2D RB;
    [SerializeField] float _move_input;
    [SerializeField] float _vertical_input;
    [SerializeField] bool _isGrounder;
    [SerializeField] Transform _groundChek;
    [SerializeField] LayerMask _groundMask;
    [SerializeField] float _radiusIsGrounder;
    [SerializeField] int _extraJump;
    [SerializeField] GunHero _gunHero;
    public int ExtraJumpValue;
    [SerializeField] float _jumpForce;
    public bool GunActive;

    [SerializeField] Animator animator;

    [SerializeField] bool _gun_right;
    [SerializeField] bool facingRight;
    public bool Is_bomb;
    public GameObject Bomb;

    public bool _noShells=false;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        
    }

    public void Update()
    {

        if (Input.GetButton("Jump"+IDJumpInput)&& _isGrounder && !GunActive)
        {
            _jumpSource.Play();
            RB.velocity = Vector2.up * _jumpForce;
        }
        


        if (_move_input>0&&GunActive&&_gun_right)
        {
            if (_gunHero==null)
            {
                
            }
            else
            {
                _gunHero.FiredGun(this.gameObject);
            }
            
        }else if(_move_input < 0 && GunActive && !_gun_right)
        {
            if (_gunHero == null)
            {

            }
            else
            {
                _gunHero.FiredGun(this.gameObject);
            }
        }
        _vertical_input = Input.GetAxis("Vertical"+IDInputVertical);
        if (_vertical_input != 0 && GunActive) _gunHero.Rotate(_vertical_input);
        if (_vertical_input < 0 && Is_bomb && Bomb)
        {
            
            Bomb.GetComponent<BombExplorer>().GetRidOfBomb();
            Is_bomb = false;
            Bomb = null;

        }


        _move_input = Input.GetAxis("Horizontal"+IDInputHorizont);
        animator.SetFloat("horizontalMove", Mathf.Abs(_move_input));
        if (_isGrounder)
        {
            animator.SetBool("jump 0", false);
        }
        else if (!_isGrounder)
        {
            animator.SetBool("jump 0", true);
        }

        if (Is_bomb & GunActive) ResetGunActive();
    }
    void FixedUpdate()
    {

        _isGrounder = Physics2D.OverlapCircle(_groundChek.position, _radiusIsGrounder,_groundMask);
        
        RB.velocity =new Vector2(_move_input * speed, RB.velocity.y);
        if (!facingRight&&_move_input>0)
        {
            Flip();
        }
        else if (facingRight && _move_input < 0)
        {
            Flip();
        }
        if (transform.position.y<=-10f)
        {
            GetComponent<PlayerMeneger>().PlayerSetActiveFalse();
        }
    }
    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 locascele = transform.localScale;
        locascele.x *= -1;
        transform.localScale = locascele;
    }

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject Go = collision.gameObject;
        if (Go.tag == "Gun"&&!Is_bomb)
        {
            _gunHero = Go.GetComponent<GunHero>();
            if (Go.transform.rotation.y<0)
            {
                _gun_right = false;
            }
            else
            {
                _gun_right = true;
            }
        }
       
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        GameObject Go = collision.gameObject;
        if (Go.tag == "Gun") _gunHero = null;
    }
    private void ResetGunActive()
    {
        GunActive = false;
    }
}
