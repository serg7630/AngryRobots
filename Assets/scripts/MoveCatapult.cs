using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCatapult : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputHorizon = Input.GetAxis("Horizontal");
        Vector2 pos = transform.position;
        pos.x += inputHorizon * Time.deltaTime;
        transform.position = pos;
    }
}
