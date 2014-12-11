using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelphiCTCombinaiton.Models
{
    public class TaskEngineModel
    {
        public string TaskName { get; set; }

        public TaskParameter[] Parameters { get; set; }
    }

    public class TaskParameter
    {
        public object Name { get; set; }
        public object Value { get; set; }
    }

    public class TaskLink
    {
        public string Href { get; set; }
        public string IsTemplated { get; set; }
        public string Rel { get; set; }
        public string Title { get; set; }
    }

    public class SingleTaskJson 
    {
        public object _embedded { get; set; }
        public TaskLink[] _links { get; set; }
        public string Completed { get; set; }
        public object Created { get; set; }
        public Error[] Errors { get; set; }
        public bool HasFailed { get; set; }
        public Guid Id { get; set; }
        public object Status { get; set; }
        public DateTimeOffset Updated { get; set; }
    }

    //not use frequently
    public class AllTaskJson
    {
        public object _embedded { get; set; }
        public int Page{get;set;}
        public TaskLink[] _links { get; set; }
        public SingleTaskJson[] ResourceList { get; set; }
    }

}
