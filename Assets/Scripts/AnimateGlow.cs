using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateGlow : MonoBehaviour {


    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;
    
    void Awake() {
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
    }

	void Update () {
        _renderer.GetPropertyBlock(_propBlock);

        _propBlock.SetFloat("_Intensity", Random.value * 0.2f + 0.4f);

        _renderer.SetPropertyBlock(_propBlock);
	}
}
