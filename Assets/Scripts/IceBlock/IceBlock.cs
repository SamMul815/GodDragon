using System.Collections;
using UnityEngine;

public class IceBlock : MonoBehaviour {

    public float SizeupSpeed;
    public bool isSpawn;
    public float _Hp;
    public GameObject _PrismBrokenPrefab;

    bool isDead = false;

    // Use this for initialization
    void Start ()
    {
		
	}
    public void GetDamage(float Damage)
    {
        _Hp -= Damage;
        if(_Hp <= 0 && !isDead)
        {
            isDead = true;
            Instantiate(_PrismBrokenPrefab,this.transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    public void Spawn()
    {
        StartCoroutine("CorSpawnEvent");
    }

    IEnumerator CorSpawnEvent()
    {
        float _sizeTime = 0.0f;
        while(_sizeTime < 1)
        {
            _sizeTime += Time.fixedDeltaTime * SizeupSpeed;
            this.GetComponent<Renderer>().material.SetFloat("_Metallic", _sizeTime);
            yield return new WaitForEndOfFrame();
        }
        isSpawn = true;
    }
}
