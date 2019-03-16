using UnityEngine;

public class MoveHorsieUpDown : MonoBehaviour
{
    private Vector3 _startPosition;
    private float _offset;

    public void Start()
    {
        _startPosition = transform.position;
        _offset = Random.value * 2;
    }

    public void Update()
    {
        transform.position = _startPosition + Vector3.up * 0.3f + Mathf.Sin(Time.time + _offset) * 0.5f * Vector3.up;
    }
}
