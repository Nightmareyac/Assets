using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{
    [SerializeField] private GameObject clonerPrefab;

    public void CreateClone()
    {
        GameObject newClone = Instantiate(clonerPrefab);

    }
}
