﻿using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class Tower : MonoBehaviour
{
    public TowerCard Model;
    public List<GameObject> EnemiesInRange;

    private float _attackCooldown = 0f;
    private GameObject _currentTarget = null;
    public DrawCircle RangeIndicator;

    private void Start()
    {
        EnemiesInRange = new List<GameObject>();
        SetRange();
    }

    private void Update()
    {
        if (_currentTarget == null && EnemiesInRange.Count > 0)
        {
            _currentTarget = EnemiesInRange[0];
        }

        _attackCooldown -= Time.deltaTime;
        Fire();
    }

    private void Fire()
    {
        if (Model.Damage == 0 || !_currentTarget || !(_attackCooldown <= 0)) return;
        Vector3 vectorToTarget = _currentTarget.transform.position - transform.position;
        float   angle          = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        _attackCooldown = Model.AttackRate;
        GameObject bolt =
            Instantiate(Model.BoltPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        TowerAttack tbolt = bolt.GetComponent<TowerAttack>();
        tbolt.Target        = _currentTarget.transform.position;
        tbolt.MovementSpeed = Model.AttackMovementSpeed;
        tbolt.Damage        = Model.Damage;
    }

    private void SetRange()
    {
        DrawCircle rangeIndicator = GetComponent<DrawCircle>();
        if (rangeIndicator)
        {
            rangeIndicator.Xradius = Model.Range;
            rangeIndicator.Yradius = Model.Range;
            rangeIndicator.CreatePoints();
        }


        CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
        if (circleCollider2D)
        {
            circleCollider2D.radius = Model.Range;
        }
    }

    public void DoStartOfTurnEffects()
    {
        foreach (Effect effect in Model.StartOfTurnEffects)
        {
            effect.Activate();
        }
    }

    //TODO: DO a layer check and setup the layer matrix appropriately
    public void OnTriggerEnter2D(Collider2D col)
    {
        EnemiesInRange.Add(col.gameObject);
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == _currentTarget)
        {
            _currentTarget = null;
        }

        EnemiesInRange.Remove(col.gameObject);
    }
}