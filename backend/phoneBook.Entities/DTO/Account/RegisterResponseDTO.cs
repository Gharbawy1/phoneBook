using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phoneBook.Entities.DTO.Account
{
    public class RegisterResponseDTO
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Token { get; set; }
    }
}
