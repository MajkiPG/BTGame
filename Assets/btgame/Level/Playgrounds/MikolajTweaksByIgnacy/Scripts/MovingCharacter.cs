using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCharacter : MonoBehaviour
{
    [Header("External objects")]
    public GameObject MovementTarget = null;

    [Header("Movement parameters")]
    [Range(0, 10)]
    public float MovementSpeed = 2.0f;

    private Rigidbody _body;
    private int _health = 100;

    public void AddDamage(int damage)
    {
        _health -= damage;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _body = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_health < 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (MovementTarget != null)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 direction = (MovementTarget.transform.position - _body.transform.position).normalized;
        direction.y = 0.0f;


        _body.MovePosition(transform.position + direction * MovementSpeed * Time.fixedDeltaTime );
    }
}
