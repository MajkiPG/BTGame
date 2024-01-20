using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseTurretGun : MonoBehaviour
{
    [Header("Internal objects")]
    public GameObject Ammunition;

    [Header("Parameters")]
    [Range(0.2f, 10.0f)]
    public float FireInterval = 1.0f;

    private float _timeToNextFire = 0.0f;
    private DefenseTurret _defenseTurret;

    void Start()
    {
        _defenseTurret = GetComponentInParent<DefenseTurret>();
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 targetPosition = _defenseTurret.GetTargetPosition();

        if (targetPosition != Vector3.zero && CanFireCounter())
        {
            if (Physics.SphereCast(transform.position, 0.5f, targetPosition - transform.position, out hit, 10.0f))
            {
                Fire();
            }
        }
    }

    private bool CanFireCounter()
    {
        if (_timeToNextFire > 0.0f)
        {
            _timeToNextFire -= Time.deltaTime;
            return false;
        }
        else
        {
            _timeToNextFire = FireInterval;
            return true;
        }
    }

    private void Fire()
    {
        Instantiate(Ammunition, transform.position, Quaternion.LookRotation(_defenseTurret.GetTargetPosition() - transform.position));
    }
}
