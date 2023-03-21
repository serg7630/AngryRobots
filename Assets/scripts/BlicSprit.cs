using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlicSprit : MonoBehaviour
{
    [SerializeField] bool RenderParent=false;

    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Material _blincMat;
    [SerializeField] Material _defaultMat;

    void Start()
    {
        if (RenderParent)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        _defaultMat = _spriteRenderer.material;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag=="projectile"||go.tag=="ProjectileGun")
        {
            //Debug.LogError("gun");
            blic_mateial();
        }

    }
    void DefaultMat()
    {
        _spriteRenderer.material = _defaultMat;
    }
    public void blic_mateial()
    {
        _spriteRenderer.material = _blincMat;
        Invoke("DefaultMat", .1f);
    }
}
