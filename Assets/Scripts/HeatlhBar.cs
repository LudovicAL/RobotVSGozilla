using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatlhBar : MonoBehaviour {

    private List<Machine> machineList;
    public float globalPower { get; private set; }
    public int nombreMachinesActives { get; private set; }
    private Slider slider;

    void Start()
    {
        GameObject[] bob = GameObject.FindGameObjectsWithTag("Machine");
        machineList = new List<Machine>();
        foreach (GameObject go in bob)
        {
            machineList.Add(go.GetComponent<Machine>());
        }

        slider = this.GetComponent<Slider>();
	}
	
	void Update ()
    {
        globalPower = 0.0f;
        nombreMachinesActives = 0;
        foreach (Machine m in machineList)
        {     
            globalPower += m.power;
            if (m.power > 0.0f)
            {
                nombreMachinesActives++;
            }
        }
        slider.value = globalPower / nombreMachinesActives;
	}
}
