using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Burst = false;
    public new ParticleSystem particleSystem;
    public GameObject particleSystemPrefab;
    private ParticleSystemRenderer particleSystemRenderer;
    [SerializeField] Mesh[] particle_meshes = new Mesh[3];
    private void Awake() {
        if (particleSystem == null){
            particleSystem = Instantiate(particleSystemPrefab, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        }
        particleSystemRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();    
    }
    private void TriggerParticle(Vector2 direction, Vector3 location, int meshNum){
        particleSystemRenderer.mesh = particle_meshes[meshNum];
        particleSystem.transform.position = location;
        particleSystem.transform.LookAt(direction);
        particleSystem.Play();
    }

    public void TriggerParticle(Vector2 direction, Vector3 location, int meshNum, float size){
        particleSystemRenderer.maxParticleSize = size;
        TriggerParticle(direction, location, meshNum);
    }
}
