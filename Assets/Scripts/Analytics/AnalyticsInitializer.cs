using System;
using GameAnalyticsSDK;
using UnityEngine;

namespace Analytics
{
    public class AnalyticsInitializer : MonoBehaviour
    {
        private void Start()
        {
            GameAnalytics.Initialize();
        }
    }
}
