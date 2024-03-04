using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    public int openside; //para saber donde sera el sitio donde tiene la puerta o entrada abierta
    //1 Need Below door
    //2 Need Top door
    //3 Need left door
    //4 Need right door
    private int random;
    private RoomTemplates templates;
    private bool spawned = false;

    void Start()
    {
        templates=GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("spawn", 5.0f);
    }
    void spawn()
    {
        if (spawned == false) //si es falso spawned instanciame las salas
        {
            if (openside == 1)
            {
                //Necesitaremos una habitacion con una entrada abajo
                random = Random.Range(0, templates.belowRooms.Length); //es un random que nos va a sacar desde cero hasta templates con aperturas abajo
                Instantiate(templates.belowRooms[random], transform.position, templates.belowRooms[random].transform.rotation);// la va a instanciar en una posicion con una rotacion especifica
            }
            else if (openside == 2)
            {
                //Necesitaremos una habitacion con una entrada arriba
                random = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[random], transform.position, templates.topRooms[random].transform.rotation);
            }
            else if (openside == 3)
            {
                //Necesitaremos una habitacion con una entrada en la parte izquierda
                random = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[random], transform.position, templates.leftRooms[random].transform.rotation);
            }
            else if (openside == 4)
            {
                //Necesitaremos una habitacion con una entrada en la parte derecha
                random = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[random], transform.position, templates.rightRooms[random].transform.rotation);
            }
            spawned = true; //como ya ha pasado por aqui y las ha instanciado ya, lo hacemos true 
        }
        
    }//spawn

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedRoom, transform.position,Quaternion.identity); //para evitar que queden salas abiertas en las ultima sala
                Destroy(gameObject);///para que no instancie varios en el mismo sitio
            }
            spawned = true;
        }
    }
}
