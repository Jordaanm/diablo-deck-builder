using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldShatter : Skill {

    public ShieldShatter(GameUnit owner): base(owner) {
        m_skillController = new LimitedOpponentSkillTarget(1).Init(this, owner);
        m_name = "Shield Shatter";
        m_targetType = ESkillTarget.SINGLE_ENEMY;
    }

    public override void Activate(GameUnit source, List<GameUnit> targets) {
        Debug.Log("Activate ShieldShatter");
        
        source.GetComponent<Animator>().SetBool("swordStrikeTrigger", true);

        foreach (GameUnit target in targets) {
            target.TakeDamage(2);
        }
    }
}
