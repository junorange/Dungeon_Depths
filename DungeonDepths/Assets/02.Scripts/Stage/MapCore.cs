using System.Collections;
using UnityEngine;

public class MapCore : MonoBehaviour
{
    [SerializeField]
    private int hitCount;
    public Transform Position { get; private set; }
    public bool IsDestroyed { get; private set; }
    [SerializeField]
    private MeshRenderer[] mesh;

    private void OnEnable()	// 활성화될 때 초기화되도록
    {
        hitCount = 3;
        IsDestroyed = false;
        Position = transform;
    }
    private void Start()
    {
        mesh = transform.GetComponentsInChildren<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("PlayerAA"))
        {
            if (--hitCount == 0)
                StartCoroutine(DestroyCore());
        }
    }
    private void OnDisable()
    {
        if (IsDestroyed)
        {
            StageManager.Instance.ClearStage();
            StageManager.Instance.MovePortal(this.transform.position, this.transform.rotation);
        }
    }
    IEnumerator DestroyCore()
    {
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            foreach (var m in mesh)
            {
                Color c = m.material.color;
                c.a = f;
                m.material.color = c;
                Debug.Log("fade out" + m);
                Debug.Log("fade out" + m);
            }
            yield return new WaitForSeconds(0.1f);
        }
        IsDestroyed = true;
        this.gameObject.SetActive(false);
    }
}
