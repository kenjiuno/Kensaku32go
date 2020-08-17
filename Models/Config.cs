using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Kensaku32go.Models
{
    public class Config
    {
        [XmlElement]
        public ObservableCollection<string> Dirs { get; set; } = new ObservableCollection<string>();
    }
}
