using UnityEngine;
using System.Collections.Generic;

public class PlayerTriggerHandler : MonoBehaviour
{
    private ParticleSystem ps;
    private List<ParticleSystem.Particle> enterParticles = new List<ParticleSystem.Particle>();

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleTrigger()
    {
        // プレイヤーに入ったパーティクルを取得
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);

        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enterParticles[i]; // コピーを取得
            p.remainingLifetime = 0f;             // 寿命を変更
            enterParticles[i] = p;
            WetnessCounter wet = GameObject.FindWithTag("walker").GetComponent<WetnessCounter>();
            wet.wetness = Mathf.Min(wet.wetness + 1, wet.wetnessSup);
            Debug.Log(wet);
        }

        // 変更を反映
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);
    }
}