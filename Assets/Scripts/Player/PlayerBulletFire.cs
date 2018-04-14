using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletFire : MonoBehaviour {
    public GameObject FirePos;
    public BulletManager bulletManager;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            //bulletManager.BaseBulletFire(FirePos.transform);
            //bulletManager.WaveBulletFire(FirePos.transform);
            bulletManager.RotateBulletFire(FirePos.transform);
            //bulletManager.HomingBulletFire(FirePos.transform);
            //Debug.Log(FirePos.transform.forward.ToString() + FirePos.transform.right.ToString() + FirePos.transform.up.ToString());
        }

        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = new Ray(FirePos.transform.position, FirePos.transform.forward);
            RaycastHit rayHit;
            if(Physics.Raycast(ray,out rayHit))
            {
                bulletManager.IceBlockSpawn(rayHit.point);
            }
        }

    }
}
