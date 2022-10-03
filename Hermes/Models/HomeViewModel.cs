using Hermes.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Models
{
    public class HomeViewModel : BaseModel
    {
        public OBDReader OBDReader { get; set; }

        public HomeViewModel()
        {
            OBDReader = new OBDReader();
        }

    }
}
