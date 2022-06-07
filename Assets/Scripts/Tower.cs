using System;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public int value;
    public int health = 100;

    [HideInInspector]
    public bool isActive = false;
    [HideInInspector]
    public int buildPos = 0;

    
    GameObject text;
    TextMesh textMesh;

    public event Action<int> DeathEvent;
    public void Start()
    {
        text = new GameObject("HealthText");
        text.transform.parent = this.transform;
        text.transform.position = transform.position + Vector3.up * 1.5f;
        text.transform.Rotate(0, -90, 0);
        textMesh = text.AddComponent<TextMesh>();
        //MeshRenderer meshRenderer = text.AddComponent<MeshRenderer>();
        textMesh.characterSize = 0.2f;
        textMesh.anchor = TextAnchor.MiddleCenter;
        
    }
    public void Kill()
    {
        if(DeathEvent != null)
        {
            DeathEvent(buildPos);
        }
        Destroy(gameObject);
        //I am dead Use an event system that the tower manager can subscribe to
        //so that when i die it can do stuff like clearing the blueprint spot...
    }
    public void TakeDamage(int n)
    {
        health -= n;
        textMesh.text = "Health: " + health;
        if (health <= 0)
        {
            Kill();
        }
    }
    public void SetHealth(int n)
    {
        health = n;
    }
}
