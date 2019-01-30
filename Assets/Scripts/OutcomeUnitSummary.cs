using UnityEngine;
using System.Collections;

public class OutcomeUnitSummary {
    public UnitSnapshot before;
    public UnitSnapshot after;

    public OutcomeUnitSummary(UnitSnapshot before) {
        this.before = before;
        after = null;
    }

    public OutcomeUnitSummary(UnitSnapshot before, UnitSnapshot after) {
        this.before = before;
        this.after = after;
    }
}
