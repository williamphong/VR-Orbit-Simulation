using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOrbit : MonoBehaviour
{

    public float currentTime = 0f;
    public float orbitSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime * orbitSpeed;
        transform.position = getPosition(currentTime);

        if (currentTime >= 2*Mathf.PI) currentTime = 0;
        
    }


    Vector3 getPosition(float time)
    {
        
        Vector3 position = new Vector3(50 * Mathf.Cos(time), 0.0f, 50 * Mathf.Sin(time));
        if (time >= Mathf.PI) position.y += 20 * Mathf.Sin(time * 8);

        return position;
    }

}


