using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWallCollision : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            TestSoundManager.Instance.PlaySound(_clip);
        }
    }
}
