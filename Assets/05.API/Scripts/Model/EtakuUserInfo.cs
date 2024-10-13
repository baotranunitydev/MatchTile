using System.Collections.Generic;
using System;
using Newtonsoft.Json;

[Serializable]
public struct DevNgaoResponse
{
    public bool success;
    public object data;
    public string message;
}