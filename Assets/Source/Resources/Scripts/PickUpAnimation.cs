using System.Collections;
using UnityEngine;

public class PickUpAnimation : MonoBehaviour
{
    [SerializeField] Transform _p0, _p1, _p2, _p3;
    [SerializeField] Transform _card;
    [SerializeField] float _duration = 2f;
    [SerializeField]
    AnimationCurve _translationInterpolation = new AnimationCurve(
        new Keyframe(0, 0), new Keyframe(0.03f, 0.03f), new Keyframe(0.3f, 0.03f), new Keyframe(1, 1, 4.3f, 0)
    );
    [SerializeField]
    AnimationCurve _rotationInterpolation = new AnimationCurve(
        new Keyframe(0, 0), new Keyframe(0.82f, 5.7f, 26f, 26f), new Keyframe(1, 1)
    );

    void OnEnable()
    {
        StartCoroutine(AnimateCard());
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (_p1) { Gizmos.color = Color.red; Gizmos.DrawCube(_p1.position, Vector3.one * 0.5f); }
        if (_p2) { Gizmos.color = Color.blue; Gizmos.DrawCube(_p2.position, Vector3.one * 0.5f); }
        if (_p0 && _p1 && _p2 && _p3)
        {
            Vector3 a = _p0.position, b = _p1.position, c = _p2.position, d = _p3.position;
            const int numSteps = 20;
            float dt = 1f / (float)numSteps;
            Gizmos.color = Color.white;
            for (int i = 0; i < numSteps; i++)
                Gizmos.DrawLine(SampleBezierCurve(i * dt, a, b, c, d), SampleBezierCurve((i + 1) * dt, a, b, c, d));
            Gizmos.color = Color.red; Gizmos.DrawLine(a, b);
            Gizmos.color = Color.green; Gizmos.DrawLine(b, c);
            Gizmos.color = Color.blue; Gizmos.DrawLine(c, d);
        }
    }
#endif

    IEnumerator AnimateCard()
    {
        float time = 0;
        yield return null;
        while (time < _duration)
        {
            float t = time / _duration;
            _card.position = SampleBezierCurve(_translationInterpolation.Evaluate(t), _p0.position, _p1.position, _p2.position, _p3.position);
            _card.rotation = Quaternion.SlerpUnclamped(_p0.rotation, _p3.rotation, _rotationInterpolation.Evaluate(t));

            yield return null;
            time += Time.deltaTime;
        }
        _card.position = _p3.position;
        _card.rotation = _p3.rotation;

        StartCoroutine(AnimateCard());
    }

    static Vector3 SampleBezierCurve(float t, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        => a * Mathf.Pow((1f - t), 3) + b * (3f * t * Mathf.Pow((1f - t), 2f)) + c * (3f * Mathf.Pow(t, 2f) * (1f - t)) + d * Mathf.Pow(t, 3f);
}