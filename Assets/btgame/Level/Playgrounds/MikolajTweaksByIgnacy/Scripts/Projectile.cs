using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement parameters")]
    [Range(0, 1000)]
    public float _travelSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float travelStep = Time.fixedDeltaTime * _travelSpeed;
        this.transform.position = transform.position + (transform.forward.normalized * travelStep);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<MovingCharacter>().AddDamage(40);

        }
        Destroy(gameObject);
    }
}
