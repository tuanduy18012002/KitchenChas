using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProcessBar
{
    public bool isFried {  get; set; }
    public event EventHandler<ProcessChangedEventArgs> processChanged;
    public class ProcessChangedEventArgs : EventArgs { public float processPerMax; }
}
