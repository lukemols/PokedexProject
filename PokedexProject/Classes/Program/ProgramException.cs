using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexProject
{
    public class ProgramException : System.Exception
    {
        string message;
        public override string Message { get { return message; } }

        public ProgramException(string msg = "")
        {
            if (msg == "")
                message = "Oggetto non creato.";
            else
                msg = message;
        }
    }
}
