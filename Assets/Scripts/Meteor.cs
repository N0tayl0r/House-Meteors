using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
            m_effects = GetComponentsInChildren<ParticleSystem>();
            foreach (var hit in m_effects)
            {
                if (hit.gameObject.name == "meteor_large_hit_effect(Clone)" || hit.gameObject.name == "meteor_medium_hit_effect(Clone)" || hit.gameObject.name == "meteor_small_hit_effect(Clone)")
                {
                    hit.gameObject.transform.SetParent(null);
                    hit.Play();
                    Destroy(hit.gameObject, hit.duration);
                }
            }

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
        m_effects = GetComponentsInChildren<ParticleSystem>();
        foreach (var trail in m_effects)
        {
            if (trail.gameObject.name == "meteor_large_trail_effect(Clone)" || trail.gameObject.name == "meteor_medium_trail_effect(Clone)" || trail.gameObject.name == "meteor_small_trail_effect(Clone)")
            {
                var e = trail.emission;
                e.enabled = false;
                Destroy(trail.gameObject);
            }
        }
        foreach (var hit in m_effects)
        {
            if (hit.gameObject.name == "meteor_large_hit_effect(Clone)" || hit.gameObject.name == "meteor_medium_hit_effect(Clone)" || hit.gameObject.name == "meteor_small_hit_effect(Clone)")
            {
                hit.Play();
            }
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
    private ParticleSystem[] m_effects = null;
}
