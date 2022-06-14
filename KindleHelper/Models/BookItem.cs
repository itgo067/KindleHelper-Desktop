using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindleHelper.Models
{
    public class BookItem: ObservableObject
    {
        private int _index;
        public int Index { get { return _index; } set { SetProperty( ref _index, value); } }

        private string _title;
        public string Title { get { return _title; } set { SetProperty(ref _title, value); } }

        private string _author;
        public string Author { get { return _author; } set { SetProperty(ref _author, value); } }

        private bool _done;
        public bool Done { get { return _done; } set { SetProperty(ref _done, value);  } }

        private string _asin;
        public string Asin { get { return _asin; } set { SetProperty(ref _asin, value); } }

        private string _fileType;
        public string FileType { get { return _fileType; } set { SetProperty(ref _fileType, value); } }
    }
}
