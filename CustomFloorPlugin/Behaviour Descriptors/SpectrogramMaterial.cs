using UnityEngine;


namespace CustomFloorPlugin {


    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1051:Do not declare visible instance fields", Justification = "Too old to change")]
    public class SpectrogramMaterial : MonoBehaviour {
        private Renderer renderer;
        private BasicSpectrogramData spectrogramData;
        [Header("The Array property (uniform float arrayName[64])")]
        public string PropertyName;
        [Header("The global intensity (float valueName)")]
        public string AveragePropertyName;

        public void SetData(BasicSpectrogramData newData) {
            spectrogramData = newData;
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Called by Unity")]
        private void Start() {
            renderer = gameObject.GetComponent<Renderer>();
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Called by Unity")]
        private void Update() {
            if (spectrogramData != null && renderer != null) {
                float average = 0.0f;
                for (int i = 0; i < 64; i++) {
                    average += spectrogramData.ProcessedSamples[i];
                }
                average /= 64.0f;

                foreach (Material mat in renderer.materials) {
                    mat.SetFloatArray(PropertyName, spectrogramData.ProcessedSamples);
                    mat.SetFloat(AveragePropertyName, average);
                }
            }
        }
    }
}