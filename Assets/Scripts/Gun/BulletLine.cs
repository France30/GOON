using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLine : MonoBehaviour
{
    [SerializeField] private float resetTime = 3f;

    private float currentResetTime;
    private LineRenderer line;

    private void Awake()
    {
        line = gameObject.GetComponent<LineRenderer>();
        currentResetTime = resetTime;
    }

    // Update is called once per frame
    private void Update()
    {
        if(line.positionCount != 0)
            Reset();
    }

    public void SetPositions(Vector3[] positions)
    {
        line.SetPositions(positions);
    }

    private void Reset()
    {
        if (currentResetTime > 0)
            currentResetTime -= Time.deltaTime;
        else
        {
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
            currentResetTime = resetTime;
        }
    }
}
