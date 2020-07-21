using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrystalManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countOfCrystalls;
    public List<Crystall> Crystalls = new List<Crystall>();

    public static CrystalManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
       _countOfCrystalls.text = "            " + Crystalls.Count.ToString();
    }
}
