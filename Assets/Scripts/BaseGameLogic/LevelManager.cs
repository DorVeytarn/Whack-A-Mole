using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Количество дырок в рядах")]
    [SerializeField] private int[] _holeAmountInRaw;

    public int[] HoleAmountInRaw => _holeAmountInRaw;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

}
