using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseTurret : MonoBehaviour
{
    [Header("Movement parameters")]
    [Range(0, 360)]
    public float RotationSpeed = 100.0f;
    [Range(0, 360)]
    public float PivotSpeed = 100.0f;
    [Range(0, 90)]
    public float PivotMaxAngle = 45.0f;

    [Header("Internal objects")]
    public GameObject Head;
    public GameObject Pivot;

    private float _initialRotation = 0.0f;
    private float _initialPivot = 0.0f;
    private List<GameObject> _targetsList;


    // Start is called before the first frame update
    void Start()
    {
        _targetsList = new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (_targetsList.Count > 0)
        {
            if (_targetsList[0] == null)
            {
                _targetsList.RemoveAt(0);
                return;
            }
            Vector3 directionToTarget = transform.InverseTransformPoint(_targetsList[0].transform.position).normalized;
            float targetHeadAngle = Vector3.Angle(new Vector3(transform.forward.x, 0, transform.forward.z), new Vector3(directionToTarget.x, 0, directionToTarget.z));
            float targetPivotAngle = Vector3.Angle(new Vector3(0, transform.forward.y, transform.forward.z), new Vector3(0, directionToTarget.y, directionToTarget.z));
            bool left = transform.position.x > _targetsList[0].transform.position.x ? true : false;
            SetHeadPosition(targetHeadAngle, left);
            //SetPivotPosition(targetPivotAngle);
        }
    }

    private void SetHeadPosition(float angle, bool left)
    {
        if (left) angle = 360 - angle;
        Debug.Log(angle);
        Head.transform.eulerAngles = new Vector3(0.0f, angle, 0.0f);
    }

    private void SetPivotPosition(float angle)
    {
        angle = Mathf.Clamp(angle, 0.0f, PivotMaxAngle);
        Pivot.transform.eulerAngles = new Vector3(angle, 0.0f, 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            _targetsList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            _targetsList.Remove(other.gameObject);
        }
    }

}
