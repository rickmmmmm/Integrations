using MiddleWay_DTO.ServiceInterfaces.MiddleWay_BLL;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_EDS.Services
{
    public class FlatFileService : IInputReaderService
    {
        #region Private Variables

        private string filePath;
        private string delimiter = ","; // Default delimiter is comma
        private string textQualifier = "\""; // Default qualifier is the double quote

        #endregion Private Variables

        #region Properties

        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    filePath = value;
                }
                else
                {
                    throw new ArgumentException("FilePath cannot be null or empty");
                }
            }
        }
        public string Delimiter
        {
            get
            {
                return delimiter;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    delimiter = value;
                }
            }
        }
        public string TextQualifier
        {
            get
            {
                return textQualifier;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    textQualifier = value;
                }
            }
        }

        #endregion Properties

        #region Constructor

        public FlatFileService()
        {
            //TODO: Define a configurable Streamable "CSV"-like reader
        }

        #endregion Constructor


        #region Get Functions

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public List<T> ReadInput<T>()
        {
            throw new NotImplementedException();
        }

        public List<T> ReadNext<T>()
        {
            throw new NotImplementedException();
        }

        public List<T> Read<T>(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        #endregion Get Functions

    }
}
