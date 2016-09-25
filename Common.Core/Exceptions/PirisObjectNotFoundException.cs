using System;

namespace Common.Core.Exceptions
{
    public class PirisObjectNotFoundException : Exception
    {
        public PirisObjectNotFoundException(string objectName)
            : base($"{objectName} wasn't found in the system.")
        {
        }
    }
}
