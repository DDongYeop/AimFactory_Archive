using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public List<GameObject> poolingList = new List<GameObject>();

    [SerializeField] float spawnLateTime = 1;

    private BoxCollider2D _boxCollider2D;

    public int spawnTargetNum = 0;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();

        PoolManager.instance = gameObject.AddComponent<PoolManager>();
        foreach (GameObject item in poolingList)
        {
            PoolManager.instance.CreatePool(item, transform, 10);
        }
    }

    public void AllTargetDestroy()
    {
        for (int i = 0; i <= spawnTargetNum; i++)
        {
            Target target = GameObject.Find("Target").GetComponent<Target>();
            target.TargetSmall();

            if (spawnTargetNum == -1)
                spawnTargetNum = 0;
        }
    }

    public void ParticleSpawn(Vector3 targetPos, int jump)
    {
        if (jump == 0)
        {
            GameObject particle = PoolManager.instance.Pop("ExoplosionParticle");
            particle.transform.position = targetPos;
            particle.GetComponent<Particle>().ParticleDestroy();
        }
        else
        {
            GameObject particle = PoolManager.instance.Pop("VortexParticle");
            particle.transform.position = targetPos;
            particle.GetComponent<Particle>().ParticleDestroy();
        }
    }

    #region Target spawn°ü·Ã
    private Vector3 RandomSpawn()
    {
        Vector3 originPosition = gameObject.transform.position;

        float range_X = _boxCollider2D.bounds.size.x;
        float range_Y = _boxCollider2D.bounds.size.y;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);

        Vector3 RandomPostion = new Vector3(range_X, range_Y, -2.5f);
        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }

    public void StartSpawn()
    {
        StartCoroutine(TargetSpawnCo());
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
    }

    IEnumerator TargetSpawnCo()
    {
        TargetSpawn();

        while (true)
        {
            TargetSpawn();

            yield return new WaitForSeconds(spawnLateTime);
        }
    }

    private void TargetSpawn()
    {
        GameObject target = PoolManager.instance.Pop("Target");
        Target targetSC = target.GetComponent<Target>();
        targetSC.time = 0;
        targetSC.TargetScale();
        target.transform.position = RandomSpawn();
        spawnTargetNum++;
    }

    #endregion
}
