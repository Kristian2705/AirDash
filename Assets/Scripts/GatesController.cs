using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class GatesController : MonoBehaviour
{
    public GameObject prefab;
    private List<Gate> pool;
    // Start is called before the first frame update
    void Awake()
    {
        pool = new List<Gate>();
    }

    // Update is called once per frame
    public Gate SpawnGate(Vector3 position, Quaternion rotation)
    {
        var gateToSpawn = GetAvailableGate();

        gateToSpawn.Transform.position = position;
        gateToSpawn.Transform.rotation = rotation;
        gateToSpawn.GameObject.SetActive(true);
        gateToSpawn.ToggleFlames(true);

        return gateToSpawn;
    }

    private Gate GetAvailableGate()
    {
        var foundGate = pool.Find(x => !x.gameObject.activeSelf);
        
        if(foundGate == null)
        {
            foundGate = Instantiate(prefab).GetComponent<Gate>();
            foundGate.Transform.parent = transform;
            pool.Add(foundGate);
            foundGate.name = $"N¹{pool.Count}";
        }

        return foundGate;
    }

    public void Disable(Gate gate)
    {
        gate.ToggleFlames(false);
    }
}
