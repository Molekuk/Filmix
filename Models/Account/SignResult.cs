using System.Collections.Generic;

namespace Filmix.Models.AccountModels
{
    public class SignResult
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
