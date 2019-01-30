using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[System.Serializable]
public class GameUnit: MonoBehaviour {

    public enum UnitType {
        HERO,
        ENEMY,
        PET,
        MINION
    };

    public GameObject body;
    public UnitType m_unitType;
    public List<Skill> skills;

    private int m_health = 10;
    public int m_maxHealth = 10;
    private int m_shield = 0;

    private bool m_isSelected = false;
    private bool m_isHighlit = false;
    private Outline outline;

    void Awake() {
        outline = GetComponent<Outline>();
    }

    private void Start() {
        m_health = m_maxHealth;
        //Spawn health bar
        createHealthBar();
        populateSkills();
    }

    public void SetUnitSelected(bool isSelected) {
        m_isSelected = isSelected;
    }

    public void SetHighlit(bool isHighlit) {
        m_isHighlit = isHighlit;
    }

    private void populateSkills() {
        skills = new List<Skill>();
        skills.Add(new Attack(this));
        skills.Add(new Block(this));
        skills.Add(new ShieldShatter(this));
    } 

    private GameObject createHealthBar() {
        GameObject healthBar = (GameObject)Instantiate(Resources.Load("Unit Health"));
        healthBar.GetComponent<Overhead>().target = body != null ? body : gameObject;
        healthBar.GetComponent<HealthDisplay>().unit = this;

        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        healthBar.transform.SetParent(canvas.transform);

        healthBar.name = this.name + "Health Bar";

        return healthBar;
    }

    public int Health {
        get { return m_health; }
        set { m_health = value; }
    }

    public int Shield {
        get { return m_shield; }
        set { m_shield = value; }
    }

    public void OnTurnStart() {
        Shield = 0;
    }

    public void OnMouseDown() {
        TurnManager.Find().UnitSelected(this);
    }

    public void TakeDamage(int damage) {
        int excessDamage = damage - Shield;
        if(Shield > 0) {
            Shield = Mathf.Max(Shield - damage, 0);
        }
        if(excessDamage > 0) {
            Health -= excessDamage;
        }
    }

    void Update() {
        if (outline != null) {
            outline.enabled = m_isSelected || m_isHighlit;
            if(m_isHighlit) {
                outline.OutlineColor = Color.yellow;
            } else {
                outline.OutlineColor = Color.white;
            }
        }
    }
}