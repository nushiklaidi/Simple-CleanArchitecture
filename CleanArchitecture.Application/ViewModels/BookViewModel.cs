﻿using CleanArchitecture.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.ViewModels
{
    public class BookViewModel
    {
        public IEnumerable<Book> Books { get; set; }
    }
}
