using UnityEngine;
using System.Collections;

public class SelfSkillTarget : SkillTargetController {

    public override void OnSkillSelected() {
        base.OnSkillSelected();
        //EnactSkill(null);
    }

    public override bool IsValidTarget(GameUnit unit) {
        return unit == Source;
    }

    public override bool CanAddTarget(GameUnit unit) {
        return false;
    }

    public override bool CanEnactSkill() {
        return true;
    }
}
