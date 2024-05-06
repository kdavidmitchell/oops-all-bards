using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAnimationManager : MonoBehaviour
{
    List<GameObject> particles = new List<GameObject>();
    List<string> sfx = new List<string>();

    public void ReceiveAnimationRequirements(string[] particles, string[] sfx)
    {
        this.particles.Clear();
        this.sfx.Clear();
        foreach (string particle in particles)
        {
            this.particles.Add(Resources.Load<GameObject>($"Particles/{particle}"));
        }
        if (sfx == null) return;
        this.sfx.AddRange(sfx);
    }

    public void PlayParticles(int particle)
    {
        if (particles[particle] == null) return;
        Instantiate(particles[particle]);
    }

    public void PlaySFX(int clip)
    {
        if (sfx[clip] == null) return;
        AudioManager.PlaySFX(sfx[clip]);
    }
}
