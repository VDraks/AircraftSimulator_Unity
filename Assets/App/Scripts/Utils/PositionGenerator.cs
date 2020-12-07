using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.Utils {
    public static class PositionGenerator {

        [Serializable]
        public class PositionSettings {
            public Vector3 Offset = Vector3.zero;
            public float MinRadius = 50;
            public float MaxRadius = 100;
            public float MinHeight = 30;
            public float MaxHeight = 100;
        }
        
        public static Vector3 GetRandomPosition(PositionSettings settings) {
            var y = Random.Range(settings.MinHeight, settings.MaxHeight);
            
            var angle = Random.Range(0f, 2 * Mathf.PI);
            var radius = Random.Range(settings.MinRadius, settings.MaxRadius);

            var x = Mathf.Cos(angle) * radius;
            var z = Mathf.Sin(angle) * radius;

            return new Vector3(x, y, z) + settings.Offset;
        }
    }
}