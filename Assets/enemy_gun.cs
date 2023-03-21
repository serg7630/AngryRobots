using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_gun : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _distance;
    [SerializeField] bool _gun_active;
    [SerializeField] GameObject _gun_rotation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       float distanceTarget = Vector2.Distance(transform.position, _target.position);

        if (distanceTarget<=_distance)
        {
            _gun_active = true;
            _gun_rotation.transform.right = _target.position-transform.position;

        }
        else { _gun_active = false; }
    }
}
