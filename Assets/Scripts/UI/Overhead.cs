using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overhead : MonoBehaviour {
    public Camera m_Camera;
    public GameObject target;
    public Canvas m_Canvas;
    public float above;
    public bool is2D = true;
	// Use this for initialization
	void Start () {
		if (m_Canvas == null) {
            m_Canvas = GetComponentInParent<Canvas>();
        }

        if (m_Camera == null) {
            m_Camera = Camera.main;
        }
	}
	
	// Update is called once per frame
	void Update () {
        //Orient to face camera
        if(!is2D) {
            transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
                       m_Camera.transform.rotation * Vector3.up);
        }

        //Position above target
        MeshRenderer mr = target.GetComponent<MeshRenderer>();
        SkinnedMeshRenderer smr = target.GetComponent<SkinnedMeshRenderer>();

        Vector3 basePosition = target.transform.position;
        float top = 0f;
        if (mr != null) {
            top = mr.bounds.max.y;
        } else if (smr != null) {
            top = smr.bounds.max.y;
        } else {
            top = basePosition.y;
        }
        Vector3 position = new Vector3(basePosition.x, top, basePosition.z);

        Vector2 projection = m_Camera.WorldToViewportPoint(position);
        RectTransform canvasRect = m_Canvas.GetComponent<RectTransform>();
        Vector2 newPosition = new Vector2(
            (projection.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
            (projection.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f) + above
        );

        GetComponent<RectTransform>().anchoredPosition = newPosition;

    }
}
