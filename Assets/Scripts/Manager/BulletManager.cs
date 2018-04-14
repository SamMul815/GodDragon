using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    public GameObject BaseBulletPrefab;
    public GameObject WaveBulletPrefab;
    public GameObject RotateBulletPrefab;
    public GameObject HomingBulletPrefab;
    public GameObject IceBlockPrefab;

    public GameObject Dragon;
    public GameObject Player;

    public void BaseBulletFire(Transform _firepos)
    {
        GameObject bullet = Instantiate(BaseBulletPrefab, _firepos.position, Quaternion.identity);
        bullet.GetComponent<BulletBase>().SetBulletValue(_firepos, 30.0f);
    }

    public void WaveBulletFire(Transform _firePos)
    {
        GameObject bullet = Instantiate(WaveBulletPrefab, _firePos.position, Quaternion.identity);
        bullet.GetComponent<BulletWave>().SetBulletValue(_firePos, 30.0f, 1, 1);

        bullet = Instantiate(WaveBulletPrefab, _firePos.position, Quaternion.identity);
        bullet.GetComponent<BulletWave>().SetBulletValue(_firePos, 30.0f, 1, 1);
        bullet.GetComponent<BulletWave>().Revers();

        bullet = Instantiate(WaveBulletPrefab, _firePos.position, Quaternion.identity);
        bullet.GetComponent<BulletWave>().SetBulletValue(_firePos, 30.0f, 0, 0, 1, 1);

        bullet = Instantiate(WaveBulletPrefab, _firePos.position, Quaternion.identity);
        bullet.GetComponent<BulletWave>().SetBulletValue(_firePos, 30.0f, 0, 0, 1, 1);
        bullet.GetComponent<BulletWave>().Revers();
    }

    public void RotateBulletFire(Transform _firePos)
    {

        BulletRotate bullet = Instantiate(RotateBulletPrefab, _firePos.position, Quaternion.identity).GetComponent<BulletRotate>();
        bullet.SetBulletValue(_firePos, 30, 1, 360, RotAxis.ZRot);


        bullet = Instantiate(RotateBulletPrefab, _firePos.position, Quaternion.identity).GetComponent<BulletRotate>();
        bullet.SetBulletValue(_firePos, 30, 1, 360, RotAxis.ZRot);
        bullet.SetTime((float)1 / 3);

        bullet = Instantiate(RotateBulletPrefab, _firePos.position, Quaternion.identity).GetComponent<BulletRotate>();
        bullet.SetBulletValue(_firePos, 30, 1, 360, RotAxis.ZRot);
        bullet.SetTime((float)2 / 3);

        bullet = Instantiate(RotateBulletPrefab, _firePos.position, Quaternion.identity).GetComponent<BulletRotate>();
        bullet.SetBulletValue(_firePos, 30, 1, 360, RotAxis.XRot);

        bullet = Instantiate(RotateBulletPrefab, _firePos.position, Quaternion.identity).GetComponent<BulletRotate>();
        bullet.SetBulletValue(_firePos, 30, 1, 360, RotAxis.XRot);
        bullet.SetTime((float)1 / 3);

        bullet = Instantiate(RotateBulletPrefab, _firePos.position, Quaternion.identity).GetComponent<BulletRotate>();
        bullet.SetBulletValue(_firePos, 30, 1, 360, RotAxis.XRot);
        bullet.SetTime((float)2 / 3);
    }

    public void HomingBulletFire(Transform _firePos)
    {
        BulletHoming bullet = Instantiate(HomingBulletPrefab, _firePos.position, Quaternion.identity).GetComponent<BulletHoming>();

        bullet.SetBulletValue(_firePos, 30, 0.8f, Dragon.transform);
        bullet.SetTag("Player");
    }

    public void IceBlockSpawn(Vector3 SpawnPos)
    {
        //GameObject ice = Instantiate(IceBlockPrefab, SpawnPos, IceBlockPrefab.transform.rotation);
        //ice.GetComponent<IceBlock>().Spawn();
        StartCoroutine("CorIceBreath");
    }

    IEnumerator CorIceBreath()
    {
        for(int x=-10; x<10; x++)
        {
            GameObject ice = Instantiate(IceBlockPrefab, new Vector3(x * 5,0,x * 5), IceBlockPrefab.transform.rotation);
            ice.GetComponent<IceBlock>().Spawn();
            yield return new WaitForSeconds(0.1f);
              
        }
    }


}
