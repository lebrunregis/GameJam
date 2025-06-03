using Spine.Unity;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    #region Publics
    public bool m_jumpEnabled;
    public SyncedBoolValue m_isAlive;
    public bool m_drawSprite = false;
    public float m_baseGravityScale = 1f;
    public LayerMask m_damageMask;
    public LayerMask m_groundMask;
    public GameObject m_deadSprite;
    public GameObject m_upSprite;
    public GameObject m_downSprite;
    public Vector2 m_deathAddForce;
    public float m_deathAddTorque;
    public AudioSource m_jumpAudioSource;
    public AudioSource m_deathAudioSource;
    public SkeletonAnimation m_skeletonAnimation;
    public ScoreSyncedScriptable m_score;
    #endregion


    #region Unity Api

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_boxLayer = (int)Math.Log(m_damageMask.value, 2);
        m_floorLayer = (int)Math.Log(m_groundMask.value, 2);
        m_isAlive.SourceValue = true;
        m_score.ResetScore();
    }
    private void OnEnable()
    {
        m_score.enabled = true;
        m_isAlive.SourceValue = true;
        m_baseScale = transform.localScale;
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_rigidbody.gravityScale = m_baseGravityScale;
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
                m_isAlive.SourceValue = false;
                SetActiveSprite();
                m_rigidbody.AddForce(new Vector2(m_deathAddForce.x * collision.rigidbody.linearVelocityX, m_deathAddForce.y * collision.rigidbody.linearVelocityY * m_rigidbody.gravityScale));
                m_rigidbody.AddTorque(m_deathAddTorque * m_rigidbody.gravityScale);
                m_deathAudioSource.Play();
            }
        }
        else
        {
            Debug.Log("collision failed.");
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (m_jumpEnabled && m_isAlive.SourceValue == true)
        {
            m_jumpEnabled = false;
            m_rigidbody.gravityScale = -m_rigidbody.gravityScale;
            SetActiveSprite();
            m_jumpAudioSource.Play();
        }
    }
    #endregion


    #region Utils
    private void SetActiveSprite()
    {
        if (m_skeletonAnimation != null)
        {
            if (m_isAlive.SourceValue == true)
            {
                if (m_rigidbody.gravityScale < 0)
                {
                    m_skeletonAnimation.loop = true;
                    m_skeletonAnimation.AnimationName = "Run-Up";
                    transform.localScale = new(m_baseScale.x, -m_baseScale.y, m_baseScale.z);

                }
                else
                {
                    m_skeletonAnimation.loop = true;
                    m_skeletonAnimation.AnimationName = "Run-Down";
                    transform.localScale = new(m_baseScale.x, m_baseScale.y, m_baseScale.z);

                }
            }
            else
            {
                m_skeletonAnimation.loop = false;
                m_skeletonAnimation.AnimationName = "Death";

            }
        }
        if (m_drawSprite)
        {
            if (m_isAlive.SourceValue == true)
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
        else
        {
            m_upSprite.SetActive(false);
            m_downSprite.SetActive(false);
            m_deadSprite.SetActive(false);
            m_spriteRenderer.enabled = false;
        }
    }
    #endregion


    #region Private and Protected
    private Rigidbody2D m_rigidbody;
    private int m_boxLayer;
    private int m_floorLayer;
    private SpriteRenderer m_spriteRenderer;
    private Vector3 m_baseScale;
    #endregion


}
