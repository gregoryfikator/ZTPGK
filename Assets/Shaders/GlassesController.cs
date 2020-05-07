using UnityEngine;

public class GlassesController : MonoBehaviour
{
    private Material material;

    private int presetsCounter = 0;

    private GlassesPreset[] presets = new GlassesPreset[6]
        {
            new GlassesPreset
            {
               BlurSamplingRate = 20,
               BlurSize = 0.0f,
               EllipsisA = 0.25f,
               EllipsisB = 0.25f,
               EllipsisCenter1 = new Vector2(0.25f, 0.5f),
               EllipsisCenter2 = new Vector2(0.750f, 0.5f),
               LensesColor = new Color(1.0f, 1.0f, 1.0f)
            },
            new GlassesPreset
            {
               BlurSamplingRate = 30,
               BlurSize = 1.0f,
               EllipsisA = 0.11f,
               EllipsisB = 0.09f,
               EllipsisCenter1 = new Vector2(0.215f, 0.660f),
               EllipsisCenter2 = new Vector2(0.700f, 0.280f),
               LensesColor = new Color(0.24f, 0.16f, 0.18f)
            },
            new GlassesPreset
            {
               BlurSamplingRate = 10,
               BlurSize = 0.003f,
               EllipsisA = 0.130f,
               EllipsisB = 0.115f,
               EllipsisCenter1 = new Vector2(0.300f, 0.465f),
               EllipsisCenter2 = new Vector2(0.700f, 0.465f),
               LensesColor = new Color(0.45f, 0.50f, 1.0f)
            },
            new GlassesPreset
            {
               BlurSamplingRate = 45,
               BlurSize = 0.250f,
               EllipsisA = 0.185f,
               EllipsisB = 0.185f,
               EllipsisCenter1 = new Vector2(0.300f, 0.465f),
               EllipsisCenter2 = new Vector2(0.750f, 0.465f),
               LensesColor = new Color(0.3f, 0.3f, 0.3f)
            },
            new GlassesPreset
            {
               BlurSamplingRate = 7,
               BlurSize = 0.150f,
               EllipsisA = 0.185f,
               EllipsisB = 0.085f,
               EllipsisCenter1 = new Vector2(0.300f, 0.260f),
               EllipsisCenter2 = new Vector2(0.770f, 0.625f),
               LensesColor = new Color(0.1f, 0.9f, 0.6f)
            },
            new GlassesPreset
            {
               BlurSamplingRate = 20,
               BlurSize = -0.150f,
               EllipsisA = 0.035f,
               EllipsisB = 0.175f,
               EllipsisCenter1 = new Vector2(0.300f, 0.450f),
               EllipsisCenter2 = new Vector2(0.770f, 0.450f),
               LensesColor = new Color(0.9f, 1.0f, 0.6f)
            }
        };


    [Header("Blur settings")]

    [SerializeField]
    [Range(2, 100)]
    private int _blurSamplingRate = 30;

    [SerializeField]
    [Range(-0.32f, 0.32f)]
    private float _blurSize = 0.0f;

    [Header("Lens settings")]

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _ellipsisCenterX1 = 0.25f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _ellipsisCenterY1 = 0.5f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _ellipsisCenterX2 = 0.75f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _ellipsisCenterY2 = 0.5f;

    [SerializeField]
    [Range(0.05f, 0.25f)]
    private float _ellipsisA = 0.1f;

    [SerializeField]
    [Range(0.05f, 0.25f)]
    private float _ellipsisB = 0.1f;

    [SerializeField]
    private Color _lensesColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    public int BlurSamplingRate
    {
        get { return _blurSamplingRate; }
        set { _blurSamplingRate = Mathf.Clamp(value, 2, 100); }
    }

    public float BlurSize
    {
        get { return _blurSize; }
        set { _blurSize = Mathf.Clamp(value, -0.32f, 0.32f); }
    }

    public float EllipsisCenterX1
    {
        get { return _ellipsisCenterX1; }
        set
        {
            _ellipsisCenterX1 = Mathf.Clamp(value, 0.0f, 1.0f);
        }
    }

    public float EllipsisCenterY1
    {
        get { return _ellipsisCenterY1; }
        set
        {
            _ellipsisCenterY1 = Mathf.Clamp(value, 0.0f, 1.0f);
        }
    }

    public float EllipsisCenterX2
    {
        get { return _ellipsisCenterX2; }
        set
        {
            _ellipsisCenterX2 = Mathf.Clamp(value, 0.0f, 1.0f);
        }
    }

    public float EllipsisCenterY2
    {
        get { return _ellipsisCenterY2; }
        set
        {
            _ellipsisCenterY2 = Mathf.Clamp(value, 0.0f, 1.0f);
        }
    }

    public float EllipsisA
    {
        get { return _ellipsisA; }
        set { _ellipsisA = Mathf.Clamp(value, 0.05f, 0.15f); }
    }

    public float EllipsisB
    {
        get { return _ellipsisB; }
        set { _ellipsisB = Mathf.Clamp(value, 0.05f, 0.15f); }
    }

    public Color LensesColor
    {
        get { return _lensesColor; }
        set { _lensesColor = value; }
    }

    private void Awake()
    {
        material = new Material(Shader.Find("Hidden/GlassesEffect"));
        ResetGlasses();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(350, 50, 200, 30), "Press O to change glasses preset", new GUIStyle() { fontSize = 30 });
        GUI.Label(new Rect(350, 80, 200, 20), $"Preset: {presetsCounter + 1}/{presets.Length}", new GUIStyle() { fontSize = 20 });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SwitchGlassesPreset();
        }
    }

    private void ResetGlasses()
    {
        presetsCounter = 0;
        UpdateGlassesPreset();
    }

    private void SwitchGlassesPreset()
    {
        presetsCounter = ++presetsCounter % presets.Length;

        UpdateGlassesPreset();
    }

    private void UpdateGlassesPreset()
    {
        var preset = presets[presetsCounter];

        BlurSamplingRate = preset.BlurSamplingRate;
        BlurSize = preset.BlurSize;

        EllipsisCenterX1 = preset.EllipsisCenter1.x;
        EllipsisCenterY1 = preset.EllipsisCenter1.y;
        EllipsisCenterX2 = preset.EllipsisCenter2.x;
        EllipsisCenterY2 = preset.EllipsisCenter2.y;

        EllipsisA = preset.EllipsisA;
        EllipsisB = preset.EllipsisB;

        LensesColor = preset.LensesColor;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //Original texture is used in last pass of shader for applying lens focus
        material.SetTexture("_OriginalTex", source);

        material.SetInt("_BlurSamplingRate", BlurSamplingRate);
        material.SetFloat("_BlurSize", BlurSize);

        material.SetVector("_EllipsisCenter1", new Vector4(_ellipsisCenterX1, _ellipsisCenterY1, 0, 0));
        material.SetVector("_EllipsisCenter2", new Vector4(_ellipsisCenterX2, _ellipsisCenterY2, 0, 0));

        material.SetFloat("_EllipsisA", EllipsisA);
        material.SetFloat("_EllipsisB", EllipsisB);

        material.SetColor("_LensesColor", LensesColor);

        var halfBlurredTexture = RenderTexture.GetTemporary(source.width, source.height);
        var blurredTexture = RenderTexture.GetTemporary(source.width, source.height);

        Graphics.Blit(source, halfBlurredTexture, material, 0);
        Graphics.Blit(halfBlurredTexture, blurredTexture, material, 1);
        Graphics.Blit(blurredTexture, destination, material, 2);

        RenderTexture.ReleaseTemporary(halfBlurredTexture);
        RenderTexture.ReleaseTemporary(blurredTexture);
    }

    internal class GlassesPreset
    {
        public int BlurSamplingRate;
        public float BlurSize;
        public Vector2 EllipsisCenter1;
        public Vector2 EllipsisCenter2;
        public float EllipsisA;
        public float EllipsisB;
        public Color LensesColor;
    }
}
