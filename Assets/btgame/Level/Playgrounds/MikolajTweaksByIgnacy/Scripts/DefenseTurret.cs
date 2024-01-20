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

    void Start()
    {
        _targetsList = new List<GameObject>();
    }

    void FixedUpdate()
    {
        if (_targetsList.Count > 0)
        {
            if (_targetsList[0] == null)
            {
                _targetsList.RemoveAt(0);
                return;
            }

            Vector3 targetPosition = PredictTargetPosition(_targetsList[0].transform.position, _targetsList[0].GetComponent<Rigidbody>().velocity);

            Vector3 directionToTarget = transform.InverseTransformPoint(targetPosition).normalized;
            float targetHeadAngle = Vector3.Angle(new Vector3(transform.forward.x, 0, transform.forward.z), new Vector3(directionToTarget.x, 0, directionToTarget.z));
            float targetPivotAngle = Vector3.Angle(new Vector3(0, transform.forward.y, transform.forward.z), new Vector3(0, directionToTarget.y, directionToTarget.z));
            bool left = transform.position.x > targetPosition.x;

            SetHeadPosition(targetHeadAngle, left);
            SetPivotPosition(targetPivotAngle);
        }
    }

    private void SetHeadPosition(float angle, bool left)
    {
        if (left) angle = 360 - angle;
        Quaternion targetRotation = Quaternion.Euler(0.0f, angle, 0.0f);
        Head.transform.rotation = Quaternion.RotateTowards(Head.transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
    }

    private void SetPivotPosition(float angle)
{
    angle = Mathf.Clamp(angle, -PivotMaxAngle, PivotMaxAngle);
    Quaternion targetRotation = Quaternion.Euler(angle, 0.0f, 0.0f);
    Pivot.transform.localRotation = Quaternion.RotateTowards(Pivot.transform.localRotation, targetRotation, PivotSpeed * Time.deltaTime);
}



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _targetsList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _targetsList.Remove(other.gameObject);
        }
    }

    public Vector3 GetTargetPosition()
    {
        if (_targetsList.Count > 0 && _targetsList[0] != null)
        {
            return PredictTargetPosition(_targetsList[0].transform.position, _targetsList[0].GetComponent<Rigidbody>().velocity);
        }
        return Vector3.zero;
    }

    public Vector3 PredictTargetPosition(Vector3 currentPosition, Vector3 targetVelocity)
    {
        float timeToReachTarget = Vector3.Distance(transform.position, currentPosition) / targetVelocity.magnitude;
        return currentPosition + targetVelocity * timeToReachTarget;
    }
}
