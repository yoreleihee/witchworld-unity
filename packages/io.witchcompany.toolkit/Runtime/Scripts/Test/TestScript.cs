using Newtonsoft.Json;
using UnityEngine;
using WitchCompany.Toolkit.Module;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start!!!!");

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            Debug.Log("update!!!!");
    }
}
