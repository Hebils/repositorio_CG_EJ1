using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.IO;

public class ControllerScene3 : MonoBehaviour
{

    List<Carro> lista_Carros = new List<Carro>();
    public TMP_InputField idCarro;
    public TMP_InputField marcaCarro;
    public TMP_InputField modeloCarro;
    public TMP_InputField placaCarro;
    public TMP_InputField numeroPuertasCarro;
    public TextMeshProUGUI textoCarros;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AgregarCarro()
    {
        string id = idCarro.text;
        string marca = marcaCarro.text;
        string modelo = modeloCarro.text;
        string placa = placaCarro.text;
        int numeroPuertas = int.Parse(numeroPuertasCarro.text);

        Carro carro = new Carro(id, marca, modelo, placa, numeroPuertas);
        lista_Carros.Add(carro);
        Debug.Log("Carro agregado: " + carro.IdVehiculo + ", " + carro.Marca + ", " + carro.Modelo + ", " + carro.Placa + ", " + carro.NumeroPuertas);
    }

    public void MostrarCarros()
    {
        string texto = "Lista de Carros: \n";
        foreach (Carro carro in lista_Carros)
        {
            texto += "ID: " + carro.IdVehiculo + ", Marca: " + carro.Marca + ", Modelo: " + carro.Modelo + ", Placa: " + carro.Placa + ", Número de Puertas: " + carro.NumeroPuertas + "\n";
        }

        textoCarros.text = texto;
    }

    public void CreateJsonFile()
    {
        ListaCarros objLista = new ListaCarros();
        objLista.carros = lista_Carros;

        string json = JsonUtility.ToJson(objLista, true);

        string carpeta = Application.streamingAssetsPath;

        string rutaArchivo = Path.Combine(carpeta, "carros.json");

        if (!Directory.Exists(carpeta))
        {
            Directory.CreateDirectory(carpeta);
        }

        File.WriteAllText(rutaArchivo, json);
        Debug.Log("Archivo JSON creado en: " + rutaArchivo);
    }

    public void ReadJsonFile()
    {
        string carpeta = Application.streamingAssetsPath;
        string rutaArchivo = Path.Combine(carpeta, "carros.json");
        string texto = "Lista de Carros: \n";
        if (File.Exists(rutaArchivo))
        {
            string json = File.ReadAllText(rutaArchivo);
            ListaCarros objLista = JsonUtility.FromJson<ListaCarros>(json);
            lista_Carros = objLista.carros;
            foreach (Carro carro in lista_Carros)
            {
                texto += "ID: " + carro.IdVehiculo + ", Marca: " + carro.Marca + ", Modelo: " + carro.Modelo + ", Placa: " + carro.Placa + ", Número de Puertas: " + carro.NumeroPuertas + "\n";
            }

            textoCarros.text = texto;
            Debug.Log("Archivo JSON leído correctamente. Número de carros: " + lista_Carros);
        }
        else
        {
            Debug.LogError("Archivo JSON no encontrado en: " + rutaArchivo);
        }

    }
}

[System.Serializable]
public class ListaCarros
{
    public List<Carro> carros;
}
