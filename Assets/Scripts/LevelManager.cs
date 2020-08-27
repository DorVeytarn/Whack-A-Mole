using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int[] _holeAmountInRaw;

    public int[] HoleAmountInRaw => _holeAmountInRaw;
}
