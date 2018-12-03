using UnityEngine;
using System.Collections;

public class LimitedOpponentSkillTarget : SkillTargetController {

    protected int m_limit;

    public LimitedOpponentSkillTarget(int limit) {
        m_limit = limit;
    }

    public override SkillTargetController ToggleTarget(GameUnit unit) {
        if(m_limit > m_targets.Count) {
            base.ToggleTarget(unit);
        }
        Debug.Log("LOST ToggleTarget, " + m_targets.Count.ToString());

        return this;
    }

    public override bool IsValidTarget(GameUnit unit) {
        return unit.m_unitType != Source.m_unitType;
    }

    public override bool CanAddTarget(GameUnit unit) {
        return m_limit < m_targets.Count && IsValidTarget(unit);
    }

    public override bool CanEnactSkill() {
        return m_targets.Count > 0;
    }
}
