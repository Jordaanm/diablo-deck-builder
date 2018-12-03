using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    protected int round;
    protected GameUnit.UnitType activeTeamType;
    private UnitManager unitManager;

    public static TurnManager Find() {
        return FindObjectOfType<TurnManager>();
    }

	// Use this for initialization
	void Start () {
        unitManager = GetComponent<UnitManager>();        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartCombat() {
        Debug.Log("Starting Combat");
        round = 0;
        //Hero goes first
        ActiveTeamType = GameUnit.UnitType.HERO;

        CombatTeam heroTeam = getActiveTeam();
        if (heroTeam != null) {
            heroTeam.Controller.OnStartTurn();
        }
    }

    protected CombatTeam getActiveTeam() {
        return unitManager.teams.Find(t => t.type == ActiveTeamType);
    }

    public GameUnit.UnitType ActiveTeamType {
        get { return activeTeamType; }
        set { activeTeamType = value; }
    }

    public bool canEndTurn(GameUnit.UnitType type) {
        return type == activeTeamType;
    }

    public void endTurn(GameUnit.UnitType type) {
        if (canEndTurn(type)) {
            Debug.Log("Ending Turn");

            CombatTeam activeTeam = getActiveTeam();
            if (activeTeam != null) {
                activeTeam.Controller.OnEndTurn();
            }

            ActiveTeamType = (ActiveTeamType == GameUnit.UnitType.HERO) ? GameUnit.UnitType.ENEMY : GameUnit.UnitType.HERO;

            IEnumerator routine = getActiveTeam().Controller.OnStartTurn();
            if(routine != null) {
                StartCoroutine(routine);
            }
        }
    }

    public void UnitSelected(GameUnit unit) {
        CombatTeam activeTeam = getActiveTeam();
        if (activeTeam != null && activeTeam.type == GameUnit.UnitType.HERO) {
            PlayerTeamController activeController = (PlayerTeamController)activeTeam.Controller;
            if (activeController != null) {
                activeController.UnitSelected(unit);
            }
        }
    }
}
