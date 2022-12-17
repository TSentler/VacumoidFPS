using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Money
{
    [RequireComponent(typeof(TrailRenderer))]
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _speed = 10f,
            _g = 1f;

        private TrailRenderer _trail;
        private Vector3 _direction;
        private float _floorPoint,
            _minAngleX = 10f,
            _maxAngleX = 25f,
            _minDistance = 0.9f,
            _bounceSpeedDivide = 4f;

        private void Awake()
        {
            _trail = GetComponent<TrailRenderer>();
        }

        private IEnumerator Start()
        {
            CalcDirection();
            _floorPoint = transform.position.y;
            transform.Rotate(Vector3.up, Random.Range(0f, 180f));
            yield return StartCoroutine(MoveUpCoroutine());
            var speed = _speed;
            _speed /= _bounceSpeedDivide;
            _floorPoint = transform.position.y;
            yield return StartCoroutine(MoveUpCoroutine());
            _speed = speed;
            yield return StartCoroutine(MoveToPointCoroutine());

        }

        private void CalcDirection()
        {
            var angleX = Random.Range(_minAngleX, _maxAngleX);
            var angleY = Random.Range(0f, 360f);
            _direction =
                Quaternion.AngleAxis(angleX, Vector3.right) * Vector3.up;
            _direction =
                Quaternion.AngleAxis(angleY, Vector3.up) * _direction;
        }

        private IEnumerator MoveUpCoroutine()
        {
            var direction = _direction;
            do
            {
                transform.Translate(
                    direction * _speed * Time.deltaTime,
                    Space.World);
                yield return null;
                direction += Vector3.down * _g * Time.deltaTime;
            } while (_floorPoint < transform.position.y);
        }

        private IEnumerator MoveToPointCoroutine()
        {
            var startTrailTime = _trail.time;
            var direction = _target.position - transform.position;
            var startDistance = direction.magnitude;
            while (_minDistance < direction.magnitude)
            {
                var rate = direction.magnitude / startDistance;
                _trail.time = Mathf.Lerp(0f, startTrailTime, rate);
                var step = direction.normalized * _speed *
                           Time.deltaTime;
                transform.Translate(step, Space.World);
                yield return null;
                direction = _target.position - transform.position;
            }

            gameObject.SetActive(false);
        }
    }
}