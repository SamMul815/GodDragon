using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWave : BulletBase {

    //계산에 관련된 수치값 수평, 수직
    private float WaveTimeX = 0.0f;
    private float VerticalX = 0.0f;

    private float WaveTimeY = 0.0f;
    private float VerticalY = 0.0f;

    private float WaveTimeZ = 0.0f;
    private float VerticalZ = 0.0f;

   // float SCValue = 1.0f / 180.0f;  //계산을 위한 수치값 

    protected override void UpdateBulletPos()
    {
        base.UpdateBulletValue();

        Vector3 dir;
        dir.x = Mathf.Approximately(WaveTimeX, 0) | Mathf.Approximately(VerticalX, 0) ? 0 : Mathf.Sin(MoveTime / WaveTimeX * Mathf.Deg2Rad * 180.0f) * VerticalX * (_isRevers ? -1 : 1) ; 
        dir.y = Mathf.Approximately(WaveTimeY, 0) | Mathf.Approximately(VerticalY, 0) ? 0 : Mathf.Sin(MoveTime / WaveTimeY * Mathf.Deg2Rad * 180.0f) * VerticalY * (_isRevers ? -1 : 1) ;
        dir.z = Mathf.Approximately(WaveTimeZ, 0) | Mathf.Approximately(VerticalZ, 0) ? 0 : Mathf.Sin(MoveTime / WaveTimeZ * Mathf.Deg2Rad * 180.0f) * VerticalZ * (_isRevers ? -1 : 1) ;

        ////Debug.Log(dir.y);
        this.transform.position = BasePosition + dir.x * BulletRight + dir.y * BulletUp + dir.z * BulletForward;
    }

    public virtual void SetBulletValue(Transform _firePos, float moveSpeed, float Wx = 0.0f, float Vx = 0.0f, float Wy = 0.0f, float Vy = 0.0f, float Wz = 0.0f, float Vz = 0.0f)
    {
        SetBaseValue(_firePos, moveSpeed);
        SetWV(Wx, Vx, Wy, Vy, Wz, Vz);
    }

    public void SetWV(float Wx = 0.0f, float Vx = 0.0f, float Wy = 0.0f, float Vy = 0.0f, float Wz= 0.0f, float Vz= 0.0f)
    {
        WaveTimeX = Wx;       VerticalX = Vx;
        WaveTimeY = Wy;       VerticalY = Vy;
        WaveTimeZ = Wz;       VerticalZ = Vz;

    }

    public void SetWVX(float Wx, float Vx)
    {
        WaveTimeX = Wx; VerticalX = Vx;
    }

    public void SetWVY(float Wy, float Vy)
    {
        WaveTimeY = Wy; VerticalY = Vy;
    }

    public void SetWVZ(float Wz, float Vz)
    {
        WaveTimeZ = Wz; VerticalZ = Vz;
    }
}
