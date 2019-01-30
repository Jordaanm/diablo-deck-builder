using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block : Skill {

    public Block(GameUnit owner): base(owner) {
        m_skillController = new SelfSkillTarget().Init(this, owner);
        m_name = "Block";
        m_targetType = ESkillTarget.SELF;
    }

    public override void Activate(GameUnit source, List<GameUnit> targets) {
        Debug.Log("Activate Block");
        source.GetComponent<Animator>().SetBool("helloTrigger", true);
        source.Shield += 5;
    }
}
