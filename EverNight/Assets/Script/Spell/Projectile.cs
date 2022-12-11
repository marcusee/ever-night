using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    // The target marker.
    public Transform target;
    // Speed in units per sec.
    public float speed;

    void Update()
    {
        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;
        if (target == null)
        {
            Debug.Log("gone");
            Destroy(this.gameObject);
            return;

        }
        // Move our position a step closer to the     target.
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        float dist = Vector3.Distance(target.position, transform.position);

        if (dist < 1)
            Destroy(this.gameObject);

    }
}
