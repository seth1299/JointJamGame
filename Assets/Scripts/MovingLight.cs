using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLight : MonoBehaviour
{
    // Start is called before the first frame update
    Transform startTransform;
    float spin = 0;
    void Start()
    {
        startTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = new Vector3(Mathf.Sin(Time.time*2f)*4, 0, 5);
        transform.rotation = Quaternion.AngleAxis(spin, new Vector3(0.2f, 1, 0));
        spin += 0.1f;
    }
}
