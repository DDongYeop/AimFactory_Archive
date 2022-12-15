using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] private float _destroyTime;

    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void ParticleDestroy()
    {
        _source.volume = 1f;
        StartCoroutine(DestroyWait());
    }

    IEnumerator DestroyWait()
    {
        float desTime = _destroyTime / 4;

        yield return new WaitForSeconds(desTime);
        _source.volume = .75f;
        yield return new WaitForSeconds(desTime);
        _source.volume = .5f;
        yield return new WaitForSeconds(desTime);
        _source.volume = .25f;
        yield return new WaitForSeconds(desTime);
        _source.volume = 0f;
        PoolManager.instance.Push(gameObject);
    }
}
