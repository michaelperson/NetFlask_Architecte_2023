namespace NetFlask.Web.Models
{
    public class HeaderMovie
    {
        #region Fields
        private string _title;
        private string _description;
        private string _picturePath;
        private DateTime _releaseDate;
        private string  _directors;
        private double _rating;
        private string _genre;
        private string _categorie;
        #endregion

        #region Properties
        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }

        public string PicturePath
        {
            get
            {
                return _picturePath;
            }

            set
            {
                _picturePath = value;
            }
        }

        public DateTime ReleaseDate
        {
            get
            {
                return _releaseDate;
            }

            set
            {
                _releaseDate = value;
            }
        }

        public string Directors
        {
            get
            {
                return _directors;
            }

            set
            {
                _directors = value;
            }
        }

        public double Rating
        {
            get
            {
                return _rating;
            }

            set
            {
                _rating = value;
            }
        }

        public string Genre
        {
            get
            {
                return _genre;
            }

            set
            {
                _genre = value;
            }
        }

        public string Categorie
        {
            get
            {
                return _categorie;
            }

            set
            {
                _categorie = value;
            }
        } 
        #endregion
    }
}
