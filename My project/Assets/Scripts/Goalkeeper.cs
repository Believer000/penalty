using UnityEngine;

public sealed class Goalkeeper : Singleton<Goalkeeper>
{
    [Header("Move Settings")]
    [SerializeField, Min(0.1f)] private float _moveSpeed = 2.5f;
    [SerializeField, Min(0)] private float _deltaSpeedPerKick = 0.16f;
    [SerializeField] private float _maxMoveSpeed = 30.0f;

    [Header("Points")]
    [SerializeField] private Vector3 _leftPosition = new Vector3(-2.0f, 0, 0);
    [SerializeField] private Vector3 _rightPosition = new Vector3(2.0f, 0, 0);
    private float _stoppingDistance = 0.01f;
    private bool _moveToLeft = true;

    private void Update()
    {
        var position = _moveToLeft ? _leftPosition : _rightPosition;

        transform.position = Vector3.MoveTowards(transform.position, position, _moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, position) <= _stoppingDistance)
        {
            _moveToLeft = !_moveToLeft;
        }
    }

    public void IncreaseMoveSpeed()
    {
        _moveSpeed += _deltaSpeedPerKick;
        _moveSpeed = Mathf.Clamp(_moveSpeed, 0, _maxMoveSpeed);
    }
}