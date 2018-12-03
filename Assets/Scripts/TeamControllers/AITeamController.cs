using UnityEngine;
using System.Collections;

public class AITeamController : TeamController {
    public AITeamController(CombatTeam owner) : base(owner) { }

    protected override void Init() {
        this.name = "AI Team Controller";
    }

    public override IEnumerator OnStartTurn() {
        base.OnStartTurn();

        return PerformTurn();
    }

    public override void OnEndTurn() {
        base.OnEndTurn();
    }

    private IEnumerator PerformTurn() {

        yield return new WaitForSeconds(1);

        GameUnit hero = UnitManager.Find().getFirstUnitOfType(GameUnit.UnitType.HERO);
        if(hero != null) {
            hero.TakeDamage(1);
        }

        yield return new WaitForSeconds(1);
        TurnManager.Find().endTurn(GameUnit.UnitType.ENEMY);

        yield return null;
    }
}
