using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPointCollision : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Point")
        {
            TestSoundManager.Instance.PlaySound(_clip);
            ScoreManager.instance.AddPoint();
            Destroy(collision.gameObject);
        }
    }
}
