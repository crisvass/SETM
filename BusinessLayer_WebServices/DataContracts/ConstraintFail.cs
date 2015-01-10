using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLayer_WebServices.DataContracts
{
    [DataContract]
    public class ConstraintFail
    {
        public string Reason { get; set; }
        public string Action { get; set; }

        public ConstraintFail(string action)
        {
            this.Reason = "Constraint Fail";
            this.Action = action;
        }
    }
}
