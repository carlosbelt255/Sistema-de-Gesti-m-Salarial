﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SistemaDeGestionSalarial.Data;
using SistemaDeGestionSalarial.Models;

namespace SistemaDeGestionSalarial.Pages.Departamentos
{
    public class IndexModel : PageModel
    {
        private readonly SistemaDeGestionSalarial.Data.DatabaseContext _context;

        public IndexModel(SistemaDeGestionSalarial.Data.DatabaseContext context)
        {
            _context = context;
        }

        public IList<Departamento> Departamento { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Departamento = await _context.Departamentos.ToListAsync();
        }
    }
}
