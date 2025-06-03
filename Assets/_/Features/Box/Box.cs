using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Box : MonoBehaviour
{
    #region Publics

    #endregion


    #region Unity Api

    private void OnEnable()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        if (transform.position.y > 0)
        {
            m_spriteRenderer.flipY = true;
        }
        else
        {
            m_spriteRenderer.flipY = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }

    #endregion


    #region Main Methods

    #endregion


    #region Utils

    #endregion


    #region Private and Protected
    private SpriteRenderer m_spriteRenderer = null;
    #endregion


}
