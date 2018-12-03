using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

    public List<GameUnit> m_units;
    public List<CombatTeam> teams;

    public static UnitManager Find() {
        return FindObjectOfType<UnitManager>();
    }

	void Start () {
        foreach (CombatTeam team in teams) {
            team.spawnTeamController();
        }
	}

    public GameUnit getFirstUnitOfType(GameUnit.UnitType unitType) {
        if(m_units.Count == 0) {
            return null;
        }
        return m_units.Find(unit => unit.m_unitType == unitType);
    }
}
