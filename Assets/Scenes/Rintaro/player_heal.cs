using System.Collections;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using System.Buffers;
using UnityEngine.UI;
using Unity.VisualScripting.Antlr3.Runtime;

public class player_heal : MonoBehaviour
{
    [SerializeField] PlayerJump mukou;
    public GameObject window;
    public Text text;
    public int heal;
    public float Cooldown;
    public bool IsHealing;
    private PlayerController PC;
    private AudioSource audioSource;
    public AudioClip healingSE;
    void Start()
    {
        mukou = GetComponent<PlayerJump>();
        PC = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !IsHealing && !PC.damaged)
        {
            Debug.Log("started");
            mukou.enabled = false;
            IsHealing = true;
            StartCoroutine(healing());
        }
    }
    IEnumerator healing()
    {
        float sec = 0;
        window.SetActive(true);
        audioSource.clip = healingSE;
        audioSource.Play();
        text.text = "服を絞り中…";
        while (sec <= Cooldown && !PC.damaged)
        {
            sec += Time.deltaTime;
            yield return null;
        }
        if (sec > Cooldown)
        {
            var wet = this.gameObject.GetComponent<WetnessCounter>();
            wet.wetness = Mathf.Max(wet.wetness - heal, 0);
            text.text = null;
        }
        else
        {
            IsHealing = false;
            text.text = "妨害された！";
            yield return new WaitForSeconds(1f);
        }
        window.SetActive(false);
        IsHealing = false;
        mukou.enabled = true;
        yield return null;
    }
}
