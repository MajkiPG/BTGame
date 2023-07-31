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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;

        if (CanFireCounter())
        {
            if (Physics.SphereCast(transform.position, 0.5f, transform.forward, out hit, 10.0f))
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
            if (_timeToNextFire <= 0.0f)
            {
                _timeToNextFire = 0.0f;
                return true;
            }
            else
                return false;
        }
        return true;
    }

    private void Fire()
    {
        _timeToNextFire = FireInterval;
        Instantiate(Ammunition, transform.position, transform.rotation);
    }


}
