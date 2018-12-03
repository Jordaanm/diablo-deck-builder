using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TeamController {
    protected string name = "Base Team Controller";
    protected CombatTeam team;

    public TeamController(CombatTeam owner) {
        this.team = owner;
        Init();
    }

    protected abstract void Init();

    public virtual IEnumerator OnStartTurn() {
        Debug.Log(name + ": On Start Turn");
        return null;
    }

    public virtual void OnEndTurn() {
        Debug.Log(name + ": On End Turn");
    }
}
