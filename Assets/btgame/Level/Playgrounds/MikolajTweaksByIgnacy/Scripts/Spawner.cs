using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("External objects")]
    public GameObject SpawnObject = null;
    public GameObject MovementTarget = null;

    [Header("Spawn parameters")]
    [Range(0.2f, 10.0f)]
    public float SpawnInterval = 2.0f;

    private float _spawnClock = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_spawnClock < 0.0f)
        {
            Spawn();
            _spawnClock = SpawnInterval;
        }
        _spawnClock -= Time.deltaTime;
    }

    private void Spawn()
    {
        if (SpawnObject != null)
        {
            GameObject spawned = Instantiate(SpawnObject, new Vector3(transform.position.x + Random.Range(-10.0f, 10.0f), 1.0f, transform.position.z + Random.Range(-10.0f, 10.0f)), SpawnObject.transform.rotation);
            spawned.GetComponent<MovingCharacter>().MovementTarget = MovementTarget;
        }
    }
}
