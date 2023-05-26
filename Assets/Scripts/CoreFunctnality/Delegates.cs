using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delegates : MonoBehaviour
{
    public delegate void OnResetPositionsDlg();
    public delegate void OnPowerSliderReleasedDlg(float value);
}
