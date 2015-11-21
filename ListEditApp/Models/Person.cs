using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListEditApp.Models
{
    class Person : BindableBase
    {
        private string name;

        public string Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }

    }
}
