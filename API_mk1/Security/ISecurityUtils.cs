using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API_mk1.Security
{
    public interface ISecurityUtils
    {
        Task<string> getGuidAsync();
        Task<long> getUnixSecondsAsync();
        object AssignDifferential(object subset, object superset);
        string GetSHA256(string inputData);
    }
}
