using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    internal class Graph
    {
        List<Vertex> vertices;
        Dictionary<string, List<Vertex>> adjacency;
        int[,] adjMatrix = new int[12, 12];

        public Graph()
        {
            // Define vertices
             var vertices = new Dictionary<string, Vertex>
            {
                ["main hall"] = new Vertex("main hall", "The main hall is central to the house."),
                ["library"] = new Vertex("library", "This library is packed with floor-to-ceiling bookshelves."),
                ["conservatory"] = new Vertex("conservatory", "The glass wall allows sunlight to reach the plants here."),
                ["billiards"] = new Vertex("billiards", "We got a pool table!"),
                ["bathroom"] = new Vertex("bathroom", "Adorned with the finest tilework."),
                ["study"] = new Vertex("study", "Two large chairs, a fireplace, and a bearskin rug."),
                ["kitchen"] = new Vertex("kitchen", "Large enough to prepare a feast."),
                ["dining room"] = new Vertex("dining room", "A huge table for sixteen has gold place settings."),
                ["ballroom"] = new Vertex("ballroom", " A room full of balls."),
                ["gallery"] = new Vertex("gallery", "Exquisite artwork decorates the walls."),
                ["deck"] = new Vertex("deck", "This covered deck looks over the landscaped grounds."),
                ["exit"] = new Vertex("exit", "Cobblestone pathway leads you to the gardens.")
            };

            // Define connections
            var adjacency = new Dictionary<string, List<Vertex>>();

            // Define connections for each vertex
            foreach (var vertex in vertices.Values)
            {
                adjacency[vertex.Room] = new List<Vertex>();
            }

            // Add connections
            adjacency["main hall"].AddRange(new[] { vertices["deck"], vertices["gallery"], vertices["ballroom"], vertices["study"], vertices["dining room"], vertices["conservatory"] });
            adjacency["library"].AddRange(new[] { vertices["conservatory"], vertices["main hall"] });
            adjacency["conservatory"].AddRange(new[] { vertices["deck"], vertices["library"] });
            adjacency["billiards"].AddRange(new[] { vertices["gallery"], vertices["bathroom"] });
            adjacency["bathroom"].AddRange(new[] { vertices["study"], vertices["billiards"] });
            adjacency["study"].AddRange(new[] { vertices["main hall"], vertices["bathroom"], vertices["ballroom"] });
            adjacency["gallery"].AddRange(new[] { vertices["billiards"], vertices["ballroom"], vertices["main hall"] });
            adjacency["deck"].AddRange(new[] { vertices["exit"], vertices["conservatory"], vertices["main hall"] });
            adjacency["dining room"].Add(vertices["kitchen"]);
            adjacency["kitchen"].Add(vertices["dining room"]);
            adjacency["ballroom"].AddRange(new[] { vertices["main hall"], vertices["gallery"], vertices["study"] });
            adjacency["exit"].Add(vertices["deck"]);


            //PART2 - Using a matrix
            adjMatrix = new int[,]
            {
                { 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0},  //main room
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0},  //billiards
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0},  //study
                { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0},  //library
                { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},  //dining
                { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0},  //kitchen
                { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0},  //conservatory
                { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1},   //deck
                { 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0},  //gallery
                { 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0},   //ballroom
                { 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0},  //bathroom
                { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0}   //exit
            };
        }

        /// <summary>
        /// Lists all the rooms in the map
        /// </summary>
        public void ListAllVertices()
        {
            foreach (Vertex room in vertices)
            {
                Console.WriteLine(room.ToString());
            }
        }

        /// <summary>
        /// Checks if the room is present or not.
        /// </summary>
        /// <param name="room">takes in room's name</param>
        /// <returns>returns true if it is present</returns>
        public bool MapContainsRoom(string room)
        {
            //check it as dictionary o(11)
            foreach (Vertex vertex in vertices)
            {
                if (vertex.Room == room)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if two rooms are adjacent or not.
        /// </summary>
        /// <param name="firstRoom"></param>
        /// <param name="secondRoom"></param>
        /// <returns></returns>
        public bool AreAdjacent(string firstRoom, string secondRoom)
        {
            //pseduocode
            //so, i am saying first room exists then check if it is adjacent to other one.
            //only way i can think of it is to use that iterate through the for loop and voila
            //check if the first room is adjacent to second room
            if (MapContainsRoom(firstRoom))
            {
                for (int i = 0; i < adjacency[firstRoom].Count; i++)
                {
                    if (adjacency[firstRoom][i].Room == secondRoom)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        ///  Checks if there is any adjacent room
        /// </summary>
        /// <param name="room">The name of the room</param>
        /// <returns>Returns null if there is nothing adjacent or returns those rooms</returns>
        public List<Vertex> GetAdjacentList(string room)
        {
            if (MapContainsRoom(room))
            {
                return adjacency[room];
            }
            //if there is nothing on the list
            return null;
        }

        /// <summary>
        /// Resets the matrix into zeroes only
        /// </summary>
        public void Reset()
        {
            foreach (Vertex room in vertices)
            {
                room.IsVisited = false;
            }
        }

        /// <summary>
        /// Checks if there is any adjacent unvisted room yet
        /// </summary>
        /// <param name="roomName">Takes that room name</param>
        /// <returns>Returns null or that vertex</returns>
        public Vertex GetAdjacentUnvisited(string roomName)
        {
            if (MapContainsRoom(roomName))
            {
                //Gives us the index we need to access adjacent matrix
                //int index =0;
                //add method
                int index = ReturnIndex(roomName);

                //checking if that room is connected to a room that is not visited yet
                for (int i = 0; i < adjMatrix.GetLength(1); i++)
                {
                    if (adjMatrix[index, i] == 1 && !vertices[i].IsVisited)
                    {
                        vertices[i].IsVisited = true;
                        return vertices[i];
                    }
                }
            }

            //Otherwise there are no room connected or unvisited
            return null;
        }

        /// <summary>
        /// Finds rooms using BFS tatic
        /// </summary>
        /// <param name="roomName">Room's name</param>
        /// <exception cref="NullReferenceException">Throws an error for invalid rooms</exception>
        public void BreadthFirst(string roomName)
        {

            if (!MapContainsRoom(roomName))
            {
                throw new NullReferenceException($"The room {roomName} is not a valid room.");
            }

            //Finds the vertex of the given room
            Vertex currentRoom = null;
            Queue<Vertex> track = new Queue<Vertex>();
            foreach (Vertex vertex in vertices)
            {
                if (vertex.Room == roomName)
                {
                    currentRoom = vertex;
                }
            }
            Console.WriteLine(currentRoom.Room);
            track.Enqueue(currentRoom);

            //As long as there is a room in the track, it finds the nearest room and then
            //shifts itself to the new room
            while (track.Count > 0)
            {
                Vertex nextRoom = new Vertex(track.Peek().Room,
                                             track.Peek().Description);
                Vertex adjRoom = GetAdjacentUnvisited(nextRoom.Room);

                if (adjRoom != null)
                {
                    Console.WriteLine(adjRoom.Room);
                    track.Enqueue(adjRoom);
                    track.Peek().IsVisited = true;
                }
                else
                {
                    track.Dequeue();
                }
            }
        }

        /// <summary>
        /// Returns index of the given room
        /// </summary>
        /// <param name="roomName">Takes in the room name</param>
        /// <returns>Returns an int position of the room in the 2D Array</returns>
        int ReturnIndex(string roomName)
        {
            for (int j = 0; j < vertices.Count; j++)
            {
                if (vertices[j].Room == roomName)
                {
                    return j;
                }
            }
            return -1;
        }

    }
}

