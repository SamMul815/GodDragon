using System.Collections;
using UnityEngine;

public class IceBlock : MonoBehaviour {

    public float SizeupSpeed;
    bool isSpawn;
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
            if(_PrismBrokenPrefab == null)
            {
                Debug.LogWarning("Not Found _PrismBrokenPrefab");
                return;
            }
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
        while(_sizeTime < 2)
        {
            //this.GetComponent<Renderer>().material.SetFloat("_Metallic", _sizeTime);
            this.transform.localScale = new Vector3(_sizeTime,_sizeTime,_sizeTime);
            //this.transform.localScale.
            _sizeTime += Time.fixedDeltaTime * SizeupSpeed;
            yield return new WaitForEndOfFrame();
        }
        this.transform.localScale = new Vector3(2, 2, 2);
        isSpawn = true;
    }
}
