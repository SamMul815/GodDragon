using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotAxis
{
    XRot = 1,
    YRot,
    ZRot
}

public class BulletRotate : BulletBase
{
    private RotAxis AngleAxis;
    private float Radius;    
    private float AngleSpeed;

    float _CurrentRadius = 0.0f;
    float _LerpTime = 1.0f;
    float _Time = 0.0f;

    protected override void UpdateBulletPos()
    {
        base.UpdateBulletValue();
        float forwardValue = 0;
        float upValue = 0;
        float rightValue = 0;

        switch(AngleAxis)
        {
            case RotAxis.XRot:
                forwardValue  = Mathf.Sin(MoveTime * AngleSpeed * Mathf.Deg2Rad ) * ( _isRevers ? -1 : 1);
                upValue        = Mathf.Cos(MoveTime * AngleSpeed * Mathf.Deg2Rad) ;
                rightValue = 0;
                break;
            case RotAxis.YRot:
                forwardValue = Mathf.Cos(MoveTime * AngleSpeed * Mathf.Deg2Rad);
                upValue = 0;
                rightValue = Mathf.Sin(MoveTime * AngleSpeed * Mathf.Deg2Rad) * (_isRevers ? -1 : 1);

                break;
            case RotAxis.ZRot:
                forwardValue = 0;
                upValue = Mathf.Sin(MoveTime * AngleSpeed * Mathf.Deg2Rad) ;
                rightValue = Mathf.Cos(MoveTime * AngleSpeed * Mathf.Deg2Rad) * (_isRevers ? -1 : 1);
                break;
        }

        if (_Time < _LerpTime)
        {
            _CurrentRadius = Mathf.Lerp(0, Radius, _Time / _LerpTime);
            _Time += Time.deltaTime;
            //_radius += _radiusLerpSpeed * Time.fixedDeltaTime;
        }
        else
            _CurrentRadius = Radius;

        this.transform.position = BasePosition +
            (forwardValue * BulletForward * _CurrentRadius) +
            (upValue * BulletUp * _CurrentRadius) +
            (rightValue * BulletRight * _CurrentRadius);
    }

    protected void SetRotValue(RotAxis _axis, float _radius, float _angleSpeed)
    {
        AngleAxis = _axis;
        Radius = _radius;
        AngleSpeed = _angleSpeed;
    }

    public virtual void SetBulletValue(Transform _firePos, float moveSpeed, float _radius, float _angleSpeed, RotAxis _axis)
    {
        SetBaseValue(_firePos, moveSpeed);
        SetRotValue(_axis, _radius, _angleSpeed);
    }
}
