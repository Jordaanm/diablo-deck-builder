using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerUI : MonoBehaviour {

    private GameObject m_unitSkills;
    private PlayerTeamController controller;
    private Button btnEnactSkill;

    public static void EndHeroTurn() {
        TurnManager.Find().endTurn(GameUnit.UnitType.HERO);
    }

    void Start() {
        setupEndTurnButton();
        setupEnactSkillButton();
    }

    private void Update() {
        bool enactSkillEnabled = controller.CanEnactSkill();
        if(btnEnactSkill != null) {
            btnEnactSkill.gameObject.SetActive(enactSkillEnabled);
        }
    }

    void setupEndTurnButton() {
        Transform btnEndTurn = transform.Find("BtnEndTurn");
        Button button = btnEndTurn.GetComponent<Button>();
        button.onClick.AddListener(PlayerUI.EndHeroTurn);
    }

    void setupEnactSkillButton() {
        Transform TxEnactSkill = transform.Find("BtnEnactSkill");
        btnEnactSkill = TxEnactSkill.GetComponent<Button>();
        btnEnactSkill.onClick.AddListener(this.EnactSkill);
    }

    void EnactSkill() {
        if(controller != null) {
            controller.EnactSkill();
        }
    }

    GameObject UnitSkills {
        get {
            if (m_unitSkills == null) {
                m_unitSkills = transform.Find("UnitSkills").gameObject;
                Debug.Log(m_unitSkills == null ? "Unit Skills not found" : "Unit Skills Found");
            }
            return m_unitSkills;
        }
    }

    public void SetController(PlayerTeamController controller) {
        this.controller = controller;
    }

    public void PopulateSkillBar(GameUnit gameUnit) {
        foreach (Skill skill in gameUnit.skills) {
            addSkillToUnitUI(skill);
        }
    }

    public void ClearSkillBar() {
        int childCount = UnitSkills.transform.childCount;
        Debug.Log("Clearing Skill Bar, " + childCount.ToString() + " Items");
        for (int i = 0; i < childCount; ++i) {
            Transform skillT = UnitSkills.transform.GetChild(0);
            skillT.SetParent(null);
            GameObject.Destroy(skillT.gameObject);
        }
    }

    private void addSkillToUnitUI(Skill skill) {
        Debug.Log("Adding " + skill.Name + " to the UI");

        GameObject skillBtn = (GameObject)GameObject.Instantiate(Resources.Load("SkillBtn"));
        skillBtn.transform.SetParent(UnitSkills.transform);
        skillBtn.transform.Find("Label").GetComponent<Text>().text = skill.Name;

        SkillButton skillButton = skillBtn.GetComponent<SkillButton>();
        if (skillButton != null) {
            skillButton.skill = skill;
            skillButton.controller = this.controller;
        }
    }


}
