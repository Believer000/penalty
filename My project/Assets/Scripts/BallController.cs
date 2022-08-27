using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class BallController : MonoBehaviour
{
    [SerializeField, Min(1)] private float _kickStrength = 150.0f;
    [SerializeField] private uint _scorePerHit = 50;
    [SerializeField, Min(0)] private float _timeToDestroyAfterKick = 3.0f;

    private bool _missedKick = true;

    private bool _onBallPressed = false;

    private Rigidbody _rigidbody = null;

    [SerializeField] private LineRenderer _lineRenderer = null;
    private Camera _cacheCamera = null;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _cacheCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        _onBallPressed = true;
    }

    private void Update()
    {
        Ray ray = _cacheCamera.ScreenPointToRay(Input.mousePosition);
        bool raycasted = Physics.Raycast(ray, out RaycastHit hit);

        if (_onBallPressed)
        {
            var position = raycasted && hit.transform != transform ?
                new Vector3(hit.point.x, hit.point.y, hit.point.z + Mathf.Abs(transform.position.z)) : Vector3.zero;

            _lineRenderer.SetPosition(1, position);
        }
    }

    private void OnMouseUp()
    {
        var direction = _lineRenderer.GetPosition(1);
        if (direction != Vector3.zero)
        {
            _rigidbody.AddForce(direction * _kickStrength);

            Destroy(this, _timeToDestroyAfterKick);
            Destroy(gameObject, _timeToDestroyAfterKick);
        }

        _onBallPressed = false;
        _lineRenderer.SetPosition(1, Vector3.zero);
    }

    private void OnTriggerEnter(Collider other)
    {
        Game.Player.IncreaseScore(_scorePerHit);
        _missedKick = false;
        Destroy(this);
    }

    private void OnDestroy()
    {
        if (_missedKick)
        {
            Game.Player.TakeDamage(1);
        }

        if (Game.Player.health > 0)
        {
            Game.NextRound();
        }
    }
}