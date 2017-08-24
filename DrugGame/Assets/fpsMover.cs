using Assets.Source.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class fpsMover : MonoBehaviour, ICharacterMover{
    float ICharacterMover.maxSpeed {
        set{ }
    }

    void ICharacterMover.Move(float h, float v)
    {
        throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
