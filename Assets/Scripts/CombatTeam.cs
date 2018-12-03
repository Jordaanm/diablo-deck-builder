using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[System.Serializable]
public class CombatTeam {
    public List<GameUnit> units;
    public GameUnit.UnitType type;
    protected TeamController controller;

    public void spawnTeamController() {
        if(type == GameUnit.UnitType.HERO) {
            controller = new PlayerTeamController(this);        
        } else {
            controller = new AITeamController(this);
        }
    }

    public TeamController Controller {
        get { return controller; }
    }
}