using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    internal class Vertex
    {
        private string room;
        private string description;
        private bool isVisited;

        /// <summary>
        /// Shows room name
        /// </summary>
        public string Room
        {
            get { return room; }
        }

        /// <summary>
        /// Shows room's description
        /// </summary>
        public string Description
        {
            get { return description; }
        }

        /// <summary>
        /// Checks if a room is visted or not.
        /// </summary>
        public bool IsVisited
        {
            get { return isVisited; }
            set { isVisited = value; }
        }
        public Vertex(string room, string description)
        {
            this.room = room;
            this.description = description;
        }

        /// <summary>
        /// Converts the data into string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return room.ToUpper() + ": " + description;
        }
    }
}
