using UnityEngine;
using System.Collections;

public class UnitSnapshot {

    public int health;
    public int shield;

    public UnitSnapshot(GameUnit unit) {
        health = unit.Health;
        shield = unit.Shield;
    }

}
