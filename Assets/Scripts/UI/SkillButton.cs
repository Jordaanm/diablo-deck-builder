using UnityEngine;
using System.Collections;

public class SkillButton : MonoBehaviour {
    public PlayerTeamController controller;
    public Skill skill;

    public void OnMouseEnter() {
        if (controller != null) {
            controller.SkillHoverStart(skill);
        }
    }

    public void OnMouseExit() {
        Debug.Log("On MouseExit");
        if (controller != null) {
            controller.SkillHoverEnd(skill);
        }
    }

    public void OnMouseDown() {
        Debug.Log("On MouseDown");
        if (controller != null) {
            controller.SetActiveSkill(skill);
        }
    }
}
