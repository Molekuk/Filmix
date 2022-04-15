using System.Collections.Generic;

namespace Filmix.Models.AccountModels
{
    public class RegisterResult
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
