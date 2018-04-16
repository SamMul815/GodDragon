using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour {

    private Transform _firePos;
    public GameObject _breathParticle;

    public void OnBreath(Transform pos)
    {
        _firePos = pos;
        _breathParticle.SetActive(true);
    }

    public void OffBreath()
    {

    }


          

}
