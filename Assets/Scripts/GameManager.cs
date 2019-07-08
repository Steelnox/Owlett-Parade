using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        if (instance != this) Destroy(this);
    }

    #endregion

    public List<Projectile> projectilePool;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public Projectile GetProjectileNotActive()
    {
        for (int i = 0; i < projectilePool.Count; i++)
        {

            if (projectilePool[i].activated == false)
            {
                projectilePool[i].activated = true;
                return projectilePool[i];
            }
        }

        return null;
    }
}
