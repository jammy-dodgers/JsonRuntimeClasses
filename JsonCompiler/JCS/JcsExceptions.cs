using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonCS.JCS
{

    [Serializable]
    public class JcsException : Exception
    {
        public JcsException() { }
        public JcsException(string message) : base(message) { }
        public JcsException(string message, Exception inner) : base(message, inner) { }
        protected JcsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    public class JcsClassbuilderException : JcsException
    {
        public JcsClassbuilderException() { }
        public JcsClassbuilderException(string message) : base(message) { }
        public JcsClassbuilderException(string message, Exception inner) : base(message, inner) { }
        protected JcsClassbuilderException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
