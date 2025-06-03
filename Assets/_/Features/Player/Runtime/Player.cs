using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    #region Publics
    public bool m_jumpEnabled;
    public bool m_isAlive;
    public float m_baseGravityScale = 1f;
    public LayerMask m_damageMask;
    public LayerMask m_groundMask;
    public GameObject m_deadSprite;
    public GameObject m_upSprite;
    public GameObject m_downSprite;
    private GameObject m_fallingSprite;
    public Vector2 m_deathAddForce;
    public float m_deathAddTorque;
    #endregion


    #region Unity Api

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_boxLayer = (int)Math.Log(m_damageMask.value, 2);
        m_floorLayer = (int)Math.Log(m_groundMask.value, 2);

    }
    private void OnEnable()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_deadSprite.SetActive(false);
        m_upSprite.SetActive(false);
        m_downSprite.SetActive(false);
        m_rigidbody.gravityScale = m_baseGravityScale;
        m_isAlive = true;
        SetActiveSprite();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    #endregion

    #region Main Methods
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == m_floorLayer)
        {
            m_jumpEnabled = true;
            SetActiveSprite();
        }
        else if (collision.gameObject.layer == m_boxLayer)
        {
            {
                m_jumpEnabled = false;
                m_isAlive = false;
                SetActiveSprite();
                m_rigidbody.AddForce( new Vector2(m_deathAddForce.x *collision.rigidbody.linearVelocityX, m_deathAddForce.y  * collision.rigidbody.linearVelocityY * m_rigidbody.gravityScale));
                m_rigidbody.AddTorque(m_deathAddTorque * m_rigidbody.gravityScale);
            }
        }
        else
        {
            Debug.Log("collision failed.");
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (m_jumpEnabled && m_isAlive)
        {
            m_jumpEnabled = false;
            m_rigidbody.gravityScale = -m_rigidbody.gravityScale;
            SetActiveSprite();

        }
    }
    #endregion


    #region Utils
    private void SetActiveSprite()
    {
        if (m_isAlive)
        {
            if (m_rigidbody.gravityScale < 0)
            {
                m_upSprite.SetActive(true);
                m_downSprite.SetActive(false);
                m_deadSprite.SetActive(false);
            }
            else
            {
                m_upSprite.SetActive(false);
                m_downSprite.SetActive(true);
                m_deadSprite.SetActive(false);
            }
        }
        else
        {
            m_upSprite.SetActive(false);
            m_downSprite.SetActive(false);
            m_deadSprite.SetActive(true);
        }
    }
    #endregion


    #region Private and Protected
    private Rigidbody2D m_rigidbody;
    private int m_boxLayer;
    private int m_floorLayer;
    #endregion


}
