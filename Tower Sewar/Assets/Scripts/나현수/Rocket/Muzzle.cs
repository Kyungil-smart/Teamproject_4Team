using System.Collections.Generic;
using UnityEngine;

public class Muzzle : MonoBehaviour
{
    [SerializeField] private GameObject _rocketPrefab;
    [SerializeField] private int        _rocketPoolSize;
    [SerializeField] private Transform  _FirePoint;

    private List<GameObject> _rockets = new List<GameObject>();

    private void Awake()
    {
        for(int i=0;i<_rocketPoolSize;i++)
        {
            GameObject tempObejct = Instantiate(_rocketPrefab);
            tempObejct.SetActive(false);
            _rockets.Add(tempObejct);
        }
    }

    public void SetRocket(Transform target)
    {
        GameObject _curRocket = GetPooledRocket();

        if(_curRocket != null && target != null)
        {
            _curRocket.transform.position = _FirePoint.position;
            _curRocket.transform.rotation = _FirePoint.rotation;
            _curRocket.SetActive(true);

            Rocket rocket = _curRocket.GetComponent<Rocket>();
            if (rocket != null)
            {
                rocket.Launch(target);
            }
        }
    }

    private GameObject GetPooledRocket()
    {
        foreach (GameObject rocket in _rockets)
        {
            if (!rocket.activeInHierarchy)
            {
                return rocket;
            }
        }

        GameObject newRocket = Instantiate(_rocketPrefab);
        newRocket.SetActive(false);
        _rockets.Add(newRocket);
        return newRocket;
    }
}