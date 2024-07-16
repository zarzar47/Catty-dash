using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Burst = false;
    public new ParticleSystem particleSystem;
    public void TriggerParticle(Vector2 direction){
        particleSystem.transform.position = transform.position;
        particleSystem.transform.LookAt(direction);
        particleSystem.Play();
    }
}
