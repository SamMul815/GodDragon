﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoming : BulletBase
{
    float _homingPower;
    Vector3 _direction;
    private Transform _target;
    private Transform Target { get { return _target; } }

  
    protected override void UpdateBulletPos()
    {
        UpdateBulletValue();
        this.transform.position = BasePosition;
        _direction = ((_target.position + (Vector3.up * 0.5f)) - transform.position).normalized;
        BulletForward = Vector3.Lerp(BulletForward, _direction, _homingPower * Time.fixedDeltaTime);
        this.transform.rotation = Quaternion.LookRotation(-_direction, Vector3.up);
    }

    public virtual void SetBulletValue(Transform firePos, float moveSpeed, float homingPower, Transform target)
    {
        SetBaseValue(firePos, moveSpeed);
        _direction = firePos.forward;
        _homingPower = homingPower; 
        _target = target;
    }
    public void SetTarget(Transform _t)
    {
        _target = _t;
    }
}
