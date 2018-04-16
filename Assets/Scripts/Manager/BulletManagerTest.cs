using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManagerTest : MonoBehaviour {

    public BulletManager manager;

    public float bulletFireTime = 5.0f;
    float fireDelay = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(fireDelay < 0)
        {
            manager.HomingBulletFire(this.transform);
            fireDelay = bulletFireTime;
        }
        fireDelay -= Time.fixedDeltaTime;
	}
}
