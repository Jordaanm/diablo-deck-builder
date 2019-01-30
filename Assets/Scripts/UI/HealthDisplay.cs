using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    public GameUnit unit;
    private Text m_text;
	// Use this for initialization
	void Start () {
        m_text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if(unit != null && m_text != null) {
            m_text.text = unit.Health.ToString() + " HP/ " + unit.Shield.ToString() + " Shield";
        } else if(unit == null) {
            Debug.Log("No Unit Found");
        } else if(m_text == null) {
            Debug.Log("No Text Found");
        }
    }
}
