using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToPlayer : MonoBehaviour {

    public bool AttachOnStart;

    public Vector3 Offset = new Vector3 (0, 0, -10);

    Transform playerTransform;

    void Start () {
        if (AttachOnStart)
            playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
        else
            enabled = false;
    }

    void LateUpdate () {
        transform.position = playerTransform.position + Offset;
    }

	public void Attach () {
        playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
        Debug.Log (playerTransform);
        enabled = true;
    }

    public void Detach () {
        playerTransform = null;
        enabled = false;
    }
}
