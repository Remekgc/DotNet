﻿
using System;

namespace TestNinja.Fundamentals
{
    public class ErrorLogger
    {
        private Guid errorID;

        public string LastError { get; set; }

        public event EventHandler<Guid> ErrorLogged; 
        
        public void Log(string error)
        {
            if (String.IsNullOrWhiteSpace(error))
                throw new ArgumentNullException();
                
            LastError = error; 
            
            // Write the log to a storage
            // ...

            OnErrorLogged(Guid.NewGuid());
        }

        protected virtual void OnErrorLogged(Guid errorID)
        {
            ErrorLogged?.Invoke(this, errorID);
        }
    }
}