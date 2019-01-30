using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerTeamController : TeamController {

    private Canvas canvas;
    private PlayerUI playerUI;
    private GameObject unitSkills;

    private GameUnit selectedUnit;
    private Skill activeSkill;

    private List<GameUnit> highlitTargets = new List<GameUnit>();

    public PlayerTeamController(CombatTeam owner):base(owner) {}

    protected override void Init() {
        this.name = "Player Team Controller";
        canvas = GameObject.FindObjectOfType<Canvas>();
        spawnPlayerUI();
        disableUI();
    }

    private void spawnPlayerUI() {
        GameObject playerUIgo = (GameObject)GameObject.Instantiate(Resources.Load("PlayerUI"));
        playerUI = playerUIgo.GetComponent<PlayerUI>();

        //Attach to canvas
        playerUI.transform.SetParent(canvas.transform);

        //Set position
        playerUI.transform.localPosition = Vector3.zero;

        //Attach behaviour
        playerUI.SetController(this);
    }

    public void UnitSelected(GameUnit unit) {
        Debug.Log(activeSkill == null ? "No active skill" : "active skill, checking for target validity");

        if (activeSkill != null) {
            bool isValidTarget = activeSkill.SkillController.IsValidTarget(unit);
            Debug.Log("Unit Selected " + (isValidTarget ? "is valid" : "is not valid"));
            if (isValidTarget) {
                activeSkill.SkillController.ToggleTarget(unit);
            }
        } else if (unit.m_unitType == GameUnit.UnitType.HERO) {
            SelectUnit(unit);
        }
    }

    private void SelectUnit(GameUnit unit) {
        Debug.Log("Source Unit Selected");
        if (selectedUnit != null) {
            Debug.Log("Deselecting Unit");
            selectedUnit.SetUnitSelected(false);
            playerUI.ClearSkillBar();
        }
        selectedUnit = unit;
        selectedUnit.SetUnitSelected(true);
        playerUI.PopulateSkillBar(unit);
    }

    public bool CanEnactSkill() {
        if (activeSkill == null) { return false; }
        return activeSkill.SkillController.CanEnactSkill();
    }

    public void EnactSkill() {
        if(activeSkill != null) {
            SkillTargetController skillController = activeSkill.SkillController;
            if(skillController.CanEnactSkill()) {
                skillController.EnactSkill();
            }
        }
    }
    
    private void Enact(Skill skill, GameUnit source, List<GameUnit> targets) {
        skill.Activate(source, targets);
        activeSkill = null;
    }

    public void SetActiveSkill(Skill skill) {
        Debug.Log("Setting Active Skill: " + skill.Name);
        activeSkill = skill;
        skill.SkillController
            .SetCallback(this.Enact)
            .OnSkillSelected();
    }

    public void SkillHoverStart(Skill skill) {
        Debug.Log("Hovering on Skill: " + skill.Name);
        foreach(GameUnit unit in UnitManager.Find().m_units) {
            bool isValidTarget = skill.SkillController.IsValidTarget(unit);
            if (isValidTarget) {
                unit.SetHighlit(true);
            }
        }
    }

    public void SkillHoverEnd(Skill skill) {
        Debug.Log("No longer hovering on Skill: " + skill.Name);
        foreach (GameUnit unit in UnitManager.Find().m_units) {
           unit.SetHighlit(false);
        }
    }

    public override IEnumerator OnStartTurn() {
        base.OnStartTurn();
        //Enable Controls
        //Enable UI
        enableUI();
        
        //Enable Skills
        //Reduce Cooldowns

        return null;
    }

    public override void OnEndTurn() {
        base.OnEndTurn();
        //Disable Controls
        //Disable UI
        disableUI();
        //Disable Skills
    }

    private void enableUI() {
        if (playerUI != null) {
            playerUI.gameObject.SetActive(true);
        }
    }

    private void disableUI() {
        if(playerUI != null) {
            playerUI.gameObject.SetActive(false);            
            playerUI.ClearSkillBar();
        }
    }
}
