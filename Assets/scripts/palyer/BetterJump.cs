using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BetterJump : MonoBehaviour
{
    [SerializeField] string IDInputJump;
 
    [SerializeField] float falMultipl = 2.5f;
    [SerializeField] float jumpMultipl = 2f;
    [SerializeField] 

    Rigidbody2D RB;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (RB.velocity.y<0)
        {
            RB.velocity += Vector2.up * Physics2D.gravity.y * (falMultipl - 1) * Time.deltaTime;
        }
        else if (RB.velocity.y > 0 &&! Input.GetButton("Jump"+IDInputJump))
        {
            RB.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultipl - 1) * Time.deltaTime;
        }
        
    }
}
