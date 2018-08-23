using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApi.Models
{
    public class CheckList
    {
        public int Id { get; set; }
        public bool CheckListStatus { get; set; }
        public string CheckListName { get; set; }
    }
}
