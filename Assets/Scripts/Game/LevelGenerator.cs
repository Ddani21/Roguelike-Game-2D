using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour{
    // Start is called before the first frame update
    private bool generation = true;
    [SerializeField]private bool shopPlaced = false;
    private int dimension,currentDirection,nextDirection,rand,emptyRoomCounter =0;
    private int xDimension =26;
    private int yDimension =-15;
 
    private int [,] levelArray;
    [SerializeField]private GameObject[] roomOutline;
    [SerializeField]private GameObject[] emptyRoomFloor,roomWithEnemies,secondaryPathsRooms;
    [SerializeField]private GameObject[] startRoom;
    [SerializeField]private GameObject exitRoom,shopRoom;
    /*index matrice:
        5 - starting position
        6 - end position
        7 - shop position;
        1 - LR opening
        2 - LRB opening
        3 - LRT opening
        4 - LRBT opening
        0 - all sides with destructible;
    */
    private void Start(){
        
        dimension = 4;
        levelArray = new int[dimension,dimension];
        int randStartPosition = Random.Range(0,4);
        GenerateMatrix(randStartPosition);
        Print();
        PutRooms();
        
        
    }

    

    // Update is called once per frame
    /*
        directia : 1,2 dreapta ; 3,4 stanga ; 5 jos
    */
    private void GenerateMatrix(int randStartPosition) {
        int i = 0;// linie
        int j = randStartPosition;// coloana
        levelArray[i,j] = 5; 
        //celor2 tipuri de camera
        currentDirection = Random.Range(1,5);
        nextDirection = Random.Range(1,6);

        if ( j == 0) {
            currentDirection = 1;
        }else if ( j == 3) {
            currentDirection = 3;
        }
        if ( currentDirection == 1 || currentDirection == 2) {
            j++;
            levelArray[i,j] = 1;
            currentDirection = 1; // ne asiguram ca mergem tot la dreapta
            nextDirection = Random.Range(1,6); 
                

        } else if ( currentDirection == 3 || currentDirection == 4) {
            
            j--;
            levelArray[i,j] = 1;
            currentDirection = 3;// we haveto keep moving left
            nextDirection = Random.Range(1,6);

        } 
        
        while (generation) {

            if ( currentDirection == 1 || currentDirection == 2) {
                
                if ( j == 3) { // we go down
                    currentDirection = 5;


                }else {
                    j++;

                        levelArray[i,j] = 1;
                        currentDirection = 1;
                        nextDirection = Random.Range(1,6);

                }
            }else if ( currentDirection == 3 || currentDirection == 4) {
                if ( j == 0) {
                    currentDirection = 5;
                }else{
                    j--;

                        levelArray[i,j] = 1;
                        currentDirection = 3;
                        nextDirection = Random.Range(1,6);

                }
            }else if ( currentDirection == 5) {
                if ( i == 3) {
                    levelArray[i,j] = 6;    //incheiam generarea si punem camera de final
                    generation = false;
                    
                }else {
                    if ( levelArray[i,j] == 3 ){//verificam daca avem LRT
                        levelArray[i,j] = 4; // daca avem, devin LRBT;
                    }else { //altfel  ne asiguram ca camera de mai sus  e LRB 
                        levelArray[i,j] = 2;
                    } 
                    i++;
                    levelArray[i,j] = 3;
                    currentDirection = nextDirection;
                    nextDirection = Random.Range(1,6);

                }
            }


        }
    }
        // punem conditia ca daca suntem la margini sa mergem in directia opusa
        // daca suntem la coloana 0 mergem la dreapta, daca suntem la coloana 3 mergem la stanga
    private void Print(){
        for( int i = 0 ; i<4 ; i++) {
            Debug.Log(levelArray[i,0] + " " + levelArray[i,1] + " "+ levelArray[i,2] + " " +levelArray[i,3]);

        }
    }
    /*index matrice:
        5- starting position
        6- end position
        1 - LR
        2 - LRB
        3 - LRT
        4 - LRBT
        0 - all sides with destructible;
    */
  
    private void PutRooms(){
        //linie cu linie e asta
        for ( int i = 0 ; i < 4; i++){
            for ( int j = 0 ; j < 4 ; j++){

                switch(levelArray[i,j]){
                        
                    case 0:

                            rand = Random.Range(0,roomOutline.Length);
                            Instantiate(roomOutline[rand],transform.position,Quaternion.identity);
                            rand = Random.Range(0,secondaryPathsRooms.Length);
                            Instantiate(secondaryPathsRooms[rand],transform.position,Quaternion.identity);
                            break;

                        case 1:

                            if(!shopPlaced){    // daca shop-ul nu e pus
                                if(i == 2){//daca suntem la i = 2
                                    shopPlaced = true;
                                    Instantiate(shopRoom,transform.position,Quaternion.identity);
                                }else{
                                    rand = Random.Range(0,10);
                                    if (rand == 0){
                                        shopPlaced = true;
                                        Instantiate(shopRoom,transform.position,Quaternion.identity);
                                    }else{

                                        SpawnRoom();
                                    }
                                }
                            }else{
                            
                                SpawnRoom();
                                
                            }
                            Instantiate(roomOutline[levelArray[i,j]],transform.position,Quaternion.identity);
                            break;

                        case 2:
                            if (!shopPlaced ){
                                if ( i == 2){
                                    shopPlaced = true;
                                    Instantiate(shopRoom,transform.position,Quaternion.identity);
                                }else{
                                    rand = Random.Range(0,10);
                                    if ( rand == 1){
                                        shopPlaced = true;
                                        Instantiate(shopRoom,transform.position,Quaternion.identity);
                                    }else{
                                        SpawnRoom();
                                    }
 
                                }

                            }else{
                                SpawnRoom();
                            }


                            Instantiate(roomOutline[levelArray[i,j]],transform.position,Quaternion.identity);
                            break;

                        case 3:
                            if(!shopPlaced){
                                
                                if ( i == 2){
                                    shopPlaced = true;
                                    Instantiate(shopRoom,transform.position,Quaternion.identity);
                                }else{
                                    rand = Random.Range(0,10);
                                    if ( rand == 1){
                                        shopPlaced = true;
                                        Instantiate(shopRoom,transform.position,Quaternion.identity);
                                    }else{
                                        SpawnRoom();
                                    }
                                }
                            }else{
                                SpawnRoom();
                                
                            }
                            Instantiate(roomOutline[levelArray[i,j]],transform.position,Quaternion.identity);
                            break;

                        case 4:
                            if (!shopPlaced ){
                                if ( i == 2 ){
                                    shopPlaced = true;
                                    Instantiate(shopRoom,transform.position,Quaternion.identity);
                                }else{
                                    rand = Random.Range(0,10);
                                    if(rand == 1){
                                        shopPlaced = true;
                                        Instantiate(shopRoom,transform.position,Quaternion.identity);
                                    }else{
                                        SpawnRoom();
                                    }
                                }
                            }else{
                                SpawnRoom();
                            }
                           

                            Instantiate(roomOutline[levelArray[i,j]],transform.position,Quaternion.identity);
                            break;

                        case 5:

                            rand = Random.Range(0,startRoom.Length);
                            Instantiate(startRoom[rand],transform.position,Quaternion.identity); // floor
                            Instantiate(roomOutline[1],transform.position,Quaternion.identity);//outline
                            break;

                        case 6:
                            Instantiate(exitRoom,transform.position,Quaternion.identity);
                            Instantiate(roomOutline[3],transform.position,Quaternion.identity);
                            break;

                        default:
                            break;
                    }
                
                transform.position = transform.position + new Vector3 (xDimension,0,0);
            }
            transform.position = transform.position + new Vector3(-xDimension*4,yDimension,0);
            //se intoarce la x original, si se deplaseaza pe y cu dimensiune.
        }
    }
    private void SpawnRoom(){
        int random = Random.Range(0,4);
        rand = Random.Range(0,emptyRoomFloor.Length);
        if (emptyRoomCounter == 2){
            if (random == 3){
                rand = Random.Range(0,emptyRoomFloor.Length);
                Instantiate(emptyRoomFloor[rand],transform.position,Quaternion.identity);
                emptyRoomCounter++;
            }else{
                rand = Random.Range(0,roomWithEnemies.Length);
                Instantiate(roomWithEnemies[rand],transform.position,Quaternion.identity);
            }
            rand = Random.Range(0,roomWithEnemies.Length);
            Instantiate(roomWithEnemies[rand],transform.position,Quaternion.identity);
            Debug.Log("nu intra");
        }else{
            rand = Random.Range(0,roomWithEnemies.Length);
            Instantiate(roomWithEnemies[rand],transform.position,Quaternion.identity);
        }
    }
}
