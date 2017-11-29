using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ScrollRect : MonoBehaviour {

    public bool ScrollAtStart;
    public bool AutoUpdate;
    public float StartDelay = 0f;
    public float ScrollDuration = 4f;

    public AnimationCurve Curve = AnimationCurve.EaseInOut (0, 0, 1, 1);

    [Range (0, 1)]
    public float ScrollPercent;

    [CustomContextMenu ("From Object", "SetStartFromObject")]
    public Rect StartRect;
    [CustomContextMenu ("From Object", "SetTargetFromObject")]
    public Rect TargetRect;

    Coroutine scrollRoutine;

    private void Start () {
        if (ScrollAtStart)
            StartCoroutine (Scroll ());
    }

    public void StartScroll () {
        scrollRoutine = StartCoroutine (Scroll ());
    }

    IEnumerator Scroll () {
        var rectTransform = GetComponent<RectTransform> ();
        yield return new WaitForSeconds (StartDelay);
        while (ScrollPercent < 1) {
            rectTransform.position = StartRect.position + (TargetRect.position - StartRect.position) * Curve.Evaluate (ScrollPercent);
            ScrollPercent += Time.deltaTime / ScrollDuration;
            yield return null;
        }
        ScrollPercent = 1;
        rectTransform.position = StartRect.position + (TargetRect.position - StartRect.position) * Curve.Evaluate (ScrollPercent);
        scrollRoutine = null;
    }

    void SetStartFromObject () {
        StartRect = GetComponent<RectTransform> ().rect;
        StartRect.x = -StartRect.x;
        //TargetRect.y = -TargetRect.y;
    }

    void SetTargetFromObject () {
        TargetRect = GetComponent<RectTransform> ().rect;
        TargetRect.x = -TargetRect.x;
        //TargetRect.y = -TargetRect.y;
    }

    private void OnValidate () {
        if (!AutoUpdate)
            return;

        var rectTransform = GetComponent<RectTransform> ();
        rectTransform.position = StartRect.position + (TargetRect.position - StartRect.position) * Curve.Evaluate (ScrollPercent);
    }
}
