using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{

    void Start()
    {
        m_isCanCalc = false;
    }

    void Update()
    {

    }

    public bool DestroyMeteor()
    {
        m_durability--;
        if (m_durability == 0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        m_isCanCalc = true;
    }

    public void OnCollisionEnter2D()
    {
        ParticleSystem[] effects = GetComponentsInChildren<ParticleSystem>();
        foreach (var effect in effects)
        {
            var e = effect.emission;
            e.enabled = false;
            Destroy(effect.gameObject);
        }

        ParticleSystem[] inactiveEffects = GetComponentsInChildren<ParticleSystem>(true);
        foreach (var effect in inactiveEffects)
        {
            effect.gameObject.SetActive(true);
        }        
    }

    public bool IsCanCalc
    {
        get { return m_isCanCalc; }
    }

    public Rigidbody2D Rigidbody2D
    {
        get { return m_meteorRB; }
    }

    public int mass
    {
        get { return m_mass; }
    }
    public int ScorePoints
    {
        get { return m_scorePoints; }
    }

    [SerializeField]
    private int m_scorePoints = 0;
    [SerializeField]
    private Rigidbody2D m_meteorRB = null;
    [SerializeField]
    private int m_mass = 0;
    [SerializeField]
    private int m_durability = 0;
    [SerializeField]
    private MeteorType m_meteorType = MeteorType.Large;
    private bool m_isCanCalc = false;
    private ParticleSystem m_trail = null;
}
