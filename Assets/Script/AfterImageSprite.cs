using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AfterImageSprite : MonoBehaviour
{
    public Transform player;

    //public SpriteRenderer spriteRenderer;
    //public SpriteRenderer playerSpriteRenderer;

    public float timeActive = 0.3f;
    private float timeSpentActive = 0f;
    public float alpha;
    public float originalAlpha = 0.9f;
    public float alphaMultiplier = 0.85f;

    public Material afterimageMaterial;

    private Color color;

    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //afterimageMaterial = player.GetComponent<MeshRenderer>().material;
        color = afterimageMaterial.color;

        //spriteRenderer = GetComponent<SpriteRenderer>();
        //playerSpriteRenderer = player.GetComponent<SpriteRenderer>();

        alpha = originalAlpha;
        //spriteRenderer.sprite = playerSpriteRenderer.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;
        timeSpentActive = Time.time;
    }

    void Update()
    {
        timeSpentActive = Time.deltaTime;
        float alpha = Mathf.Lerp(color.a, 0, timeSpentActive / timeActive);
        afterimageMaterial.color = new Color(color.r, color.g, color.b, alpha);

        //alpha *= alphaMultiplier;
        //spriteRenderer.color = color;

        if (timeSpentActive > timeActive){
            AfterImagePool.Instance.AddToPool(gameObject);
        }       
    }
}
