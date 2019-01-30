using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Attack : Skill {

    public Attack(GameUnit owner): base(owner) {
        m_skillController = new LimitedOpponentSkillTarget(1).Init(this, owner);
        m_name = "Attack";
        m_targetType = ESkillTarget.SINGLE_ENEMY;
    }

    public override void Activate(GameUnit source, List<GameUnit> targets) {
        Debug.Log("Activate Attack");
        
        source.GetComponent<Animator>().SetBool("swordStrikeTrigger", true);

        foreach (GameUnit target in targets) {
            target.TakeDamage(1);
        }
    }
}
