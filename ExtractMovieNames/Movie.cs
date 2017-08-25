using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtractMovieNames
{
    public class Movie
    {
        public Movie()
        {
        }

        //TODO: When we figure out how to farm data from IMDB (or RottenTomatoes),
        //add these properties:
        //Release Date
        //Director
        //Actors
        //Page link
        //Rating (IMDB and/or RottenTomatoes)
        //Movie rating (NC-17, R, PG-13, PG, G)

        private string name = string.Empty;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        private string driveLabel = string.Empty;
        public string DriveLabel 
        {
            get
            {
                return this.driveLabel;
            }
            set
            {
                this.driveLabel = value;
            }
        }

        private string imageName = string.Empty;
        public string ImageName
        {
            get
            {
                return this.imageName;
            }
            set
            {
                this.imageName = value;
            }
        }

        private double size = 0;
        public double Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }
    }
    public class Movies : ICollection
    {
        public string CollectionName;
        private ArrayList movieArray = new ArrayList();

        public Movie this[int index]
        {
            get { return (Movie)movieArray[index]; }
        }

        public void CopyTo(Array a, int index)
        {
            movieArray.CopyTo(a, index);
        }
        public int Count
        {
            get { return movieArray.Count; }
        }
        public object SyncRoot
        {
            get { return this; }
        }
        public bool IsSynchronized
        {
            get { return false; }
        }
        public IEnumerator GetEnumerator()
        {
            return movieArray.GetEnumerator();
        }

        public void Add(Movie newEmployee)
        {
            movieArray.Add(newEmployee);
        }
    }
}
