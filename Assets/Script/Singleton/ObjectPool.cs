using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Dictionary<string,Queue<GameObject>> m_objectPool;

    public void returnObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
        string name = gameObject.name;
        if(!name.EndsWith("(Clone)"))
        {
            Debug.LogError("return object error: " + name);
            return;
        }
        name = name.Replace("(Clone)","");
        if(m_objectPool.ContainsKey(name))
        {
            m_objectPool[name].Enqueue(gameObject);
        }
        else
        {
            m_objectPool.Add(name,new Queue<GameObject>());
            m_objectPool[name].Enqueue(gameObject);
        }
    }

    public GameObject getObject(GameObject gameObject)
    {
        string name = gameObject.name;
        if(m_objectPool.ContainsKey(name) && m_objectPool[name].Count > 0)
        {
            GameObject midGameObject = m_objectPool[name].Dequeue();
            midGameObject.SetActive(true);
            return midGameObject;
        }
        else
        {
            GameObject midGameObject = Instantiate(gameObject);
            return midGameObject;
        }
    }

    private static ObjectPool instance;
    public static ObjectPool Instance {get {return instance;} set {instance = value;} }
    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
            m_objectPool = new Dictionary<string, Queue<GameObject>>();
        }
        else
        {
            Debug.LogWarning("found another objectpool instance");
            Destroy(gameObject);
        }
    }

}
