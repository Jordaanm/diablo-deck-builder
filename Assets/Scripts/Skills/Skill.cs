using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ESkillTarget {
    ANY,
    SELF,
    SINGLE_ALLY,
    ALL_ALLY,
    SINGLE_ENEMY,
    ALL_ENEMY
}

public abstract class Skill {

    protected GameUnit m_owner;
    protected string m_name = "Base Skill";
    protected ESkillTarget m_targetType = ESkillTarget.ANY;
    protected SkillTargetController m_skillController;


    public Skill(GameUnit owner) {
        m_owner = owner;
    }

    public string Name {
        get { return m_name; }
    }

    public GameUnit Owner {
        get { return m_owner; }
    }

    public ESkillTarget getTargetType() {
        return m_targetType;
    }

    public SkillTargetController SkillController {
        get { return m_skillController; }
    }

    public abstract void Activate(GameUnit source, List<GameUnit> targets);

    public void PostOutcome(List<OutcomeUnitSummary> outcomes) {
        Debug.Log("PostOutcome");
    }

}
