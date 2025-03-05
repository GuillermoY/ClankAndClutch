//---------------------------------------------------------
// Breve descripción del contenido del archivo
// Responsable de la creación de este archivo
// Clank & Clutch
// Proyectos 1 - Curso 2024-25
//---------------------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// Añadir aquí el resto de directivas using
using UnityEngine.UI;


/// <summary>
/// Antes de cada class, descripción de qué es y para qué sirve,
/// usando todas las líneas que sean necesarias.
/// </summary>
public class Soldadora : MonoBehaviour
{
    // ---- ATRIBUTOS DEL INSPECTOR ----
    #region Atributos del Inspector (serialized fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // públicos y de inspector se nombren en formato PascalCase
    // (palabras con primera letra mayúscula, incluida la primera letra)
    // Ejemplo: MaxHealthPoints


    // CompletionImage es la barra de compleción del proceso de refinamiento
    [SerializeField] private Image CompletionBarReference;
    [SerializeField] private GameObject _metalProcesado;

    //Rapidez de trabajo: las unidades de tiempo en segundos que avanza el procesamiento del material
    [SerializeField] private float _workSpeed;

    // completionTime son las unidades de tiempo necesario para que el material se procese (segundos)
    [SerializeField] private int _completionTime = 6;

    #endregion

    // ---- ATRIBUTOS PRIVADOS ----
    #region Atributos Privados (private fields)
    // Documentar cada atributo que aparece aquí.
    // El convenio de nombres de Unity recomienda que los atributos
    // privados se nombren en formato _camelCase (comienza con _, 
    // primera palabra en minúsculas y el resto con la 
    // primera letra en mayúsculas)
    // Ejemplo: _maxHealthPoints

    //
    public bool _isWorking;

    private float _progress;

    public bool canUse;

    private Material _materialSource;
    

    #endregion

    // ---- MÉTODOS DE MONOBEHAVIOUR ----
    #region Métodos de MonoBehaviour

    // Por defecto están los típicos (Update y Start) pero:
    // - Hay que añadir todos los que sean necesarios
    // - Hay que borrar los que no se usen 

    

    /// <summary>
    /// Start is called on the frame when a script is enabled just before 
    /// any of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        _progress = 0;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {

        if (_isWorking)
        {
            _progress += (Time.deltaTime * _workSpeed) / _completionTime;
            _materialSource.UpdateProgress(_progress);
            
        }
        if (_progress >= 1)
        {
            _progress = 0;
            Destroy(_materialSource);
            GameObject metalProcesado = Instantiate(_metalProcesado, this.gameObject.transform.position, gameObject.transform.rotation);
            metalProcesado.transform.SetParent(this.transform);
            canUse = false;
        }
    }
    #endregion

    // ---- MÉTODOS PÚBLICOS ----
    #region Métodos públicos
    // Documentar cada método que aparece aquí con ///<summary>
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)
    // Ejemplo: GetPlayerController

    //
    public void TurnOnWelder()
    {
        Debug.Log("encendiendo soldadora");
        if (canUse) _isWorking = true;
    }
    public void TurnOffWelder()
    {
        Debug.Log("apagando soldadora");
        if (canUse) _isWorking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Material>() != null && collision.gameObject.GetComponent<Material>().matType == MaterialType.Metal)
        {
            _materialSource = collision.GetComponent<Material>();
            _progress = _materialSource.ReturnProgress();
            CompletionBarReference = _materialSource.ReturnProgressBar();
            canUse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Material>() != null)
        {
            _materialSource = null;
            CompletionBarReference = null;
            canUse = false;
        }
    }

    #endregion

    // ---- MÉTODOS PRIVADOS ----
    #region Métodos Privados
    // Documentar cada método que aparece aquí
    // El convenio de nombres de Unity recomienda que estos métodos
    // se nombren en formato PascalCase (palabras con primera letra
    // mayúscula, incluida la primera letra)



    // Actualiza la barra de compleción de la soldadora







    #endregion   

} // class Soldadora 
// namespace
