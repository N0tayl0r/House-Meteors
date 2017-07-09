using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SceneController : MonoBehaviour
{
    public System.Action EventMeteorDestroyed;

    public void SetRoof(GameObject roof)
    {
        Destroy(m_roof);
        m_roof = roof;
        roof.transform.SetParent(m_dummy.transform);
        roof.transform.localScale = Vector2.one;
        roof.transform.localPosition = Vector2.zero;
    }

    void Start()
    {
        m_profile = FindObjectOfType<Profile>();

        m_score = m_profile.CurrentScoreIndex;
    }

    void Update()
    {
        if (m_roof != null)
        {
            int rd = m_roof.GetComponent<Roof>().RoofDurability;
            if (CalcMass() >= rd)
            {
                Destroy(m_roof.gameObject);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                Meteor meteor = hit.collider.GetComponent<Meteor>();
                if (meteor != null)
                {
                    Vector2 position = meteor.gameObject.transform.position;
                    ParticleSystem ps = meteor.GetComponentInChildren<ParticleSystem>();
                    ps.Play();

                    if (meteor.DestroyMeteor())
                    {
                        m_score += meteor.ScorePoints * ScoreMultiplier;
                        m_profile.SetScore(m_score);
                        m_meteors.Remove(meteor);
                        BonusController bc = FindObjectOfType<BonusController>();
                        bc.CreateBonusActivator(position);
                        if (EventMeteorDestroyed != null)
                        {
                            EventMeteorDestroyed();
                        }
                    }
                }

                BonusActivator ba = hit.collider.GetComponent<BonusActivator>();
                if (ba != null)
                {
                    BonusController bc = FindObjectOfType<BonusController>();
                    bc.AddBonus(ba.Type);
                    Destroy(ba.gameObject);
                }
            }
        }
    }

    public int CalcMass()
    {
        int mass = 0;
        foreach (var meteor in m_meteors) // проходимся по всем метеоритам
        {
            if (meteor.IsCanCalc)
            {
                Rigidbody2D rb2d = meteor.Rigidbody2D; // в локальную переменную записываем RigidBody
                if (Mathf.Abs(rb2d.velocity.y) >= 0.0f && Mathf.Abs(rb2d.velocity.y) < m_velocity) // Проверяем движение
                {
                    RaycastHit2D[] hits = Physics2D.RaycastAll(meteor.transform.position, Vector2.down); // Пускаем луч сквозь всё
                    foreach (RaycastHit2D hit in hits) // проходимся по всем попаданиям
                    {
                        if (hit.collider.GetComponent<Roof>()) // пробуем взять комп. крыши
                        {
                            mass += meteor.mass; // накапливаем массу                            
                        }
                    }
                }
            }
        }
        //Debug.Log("Metiors' weight summary: " + mass);
        return mass;
    }

    public int ScoreMultiplier
    {
        get { return m_scoreMultiplier; }
        set { m_scoreMultiplier = value; }
    }

    public int Score
    {
        get { return m_score; }
    }

    [SerializeField]
    private GameObject m_dummy = null;
    [SerializeField]
    private Camera m_camera = null;
    [SerializeField]
    private GameObject m_roof = null;
    [SerializeField]
    private float m_velocity = 0.2f;
    private List<Meteor> m_meteors = new List<Meteor>();
    private int m_scoreMultiplier = 1;
    private int m_score = 0;
    private Profile m_profile = null;
}
