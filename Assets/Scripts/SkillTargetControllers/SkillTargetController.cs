using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class SkillTargetController {
    public delegate void EnactCallback(Skill skill, GameUnit source, List<GameUnit> targets);

    protected GameUnit m_source;
    protected Skill m_skill;
    protected List<GameUnit> m_targets = new List<GameUnit>();
    protected EnactCallback m_enactCallback;

    public SkillTargetController() {
    }

    public SkillTargetController Init(Skill skill, GameUnit source) {
        m_source = source;
        m_skill = skill;

        return this;
    }

    public SkillTargetController SetCallback(EnactCallback callback) {
        m_enactCallback = callback;
        return this;
    }

    public Skill Skill {
        get { return m_skill; }
    }

    public GameUnit Source {
        get { return m_source; }
    }

    public List<GameUnit> Targets {
        get { return m_targets; }
    }

    public EnactCallback Callback {
        get { return m_enactCallback; }
    }


    /// <summary>
    /// Add or Remove a target to the targets list
    /// </summary>
    /// <param name="unit">Unit to add/remove from the targets list</param>
    /// <returns>Returns this STC, for fluid interfacing / chaining</returns>
    public virtual SkillTargetController ToggleTarget(GameUnit unit) {
        if(m_targets.Contains(unit)) {
            m_targets.Remove(unit);
        } else {
            m_targets.Add(unit);
        }

        return this;
    }

    /// <summary>
    /// Remove all targets from the targets list
    /// </summary>
    /// <returns>Returns this STC, for fluid interfacing / chaining</returns>
    protected SkillTargetController ResetTargets() {
        m_targets.Clear();
        return this;
    }

    /// <summary>
    /// Is this unit ever a valid target?
    /// </summary>
    /// <param name="unit">Unit being tested for targeting</param>
    public abstract bool IsValidTarget(GameUnit unit);

    /// <summary>
    /// Can the unit currently be added to the target list?
    /// </summary>
    /// <param name="unit">Unit to try to add</param>
    public abstract bool CanAddTarget(GameUnit unit);

    /// <summary>
    /// Called when the user selects this skill
    /// </summary>
    public virtual void OnSkillSelected() {
        ResetTargets();
    }

    /// <summary>
    /// Called when the user selects another skill
    /// </summary>
    public virtual void OnSkillDeselected() {

    }

    /// <summary>
    /// Is the skill able to be enacted? (valid amount of targets, etc)
    /// </summary>
    public abstract bool CanEnactSkill();

    /// <summary>
    /// Perform the skill on the selected targets
    /// </summary>
    /// <param name="targets">Targets units to perform the skill on</param>
    public void EnactSkill() {
        m_enactCallback(m_skill, m_source, m_targets);
    }
}
