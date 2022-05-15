using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Destroy(other.gameObject);
        }

    }

    private void Dead(GameObject _gameObject)
    {
        _gameObject.transform.position = new Vector3(0, 0, 0);
    }
}
