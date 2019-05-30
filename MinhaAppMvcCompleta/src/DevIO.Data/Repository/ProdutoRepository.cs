﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppMvcBasica.Models;
using DevIO.Business.Interfaces;
using DevIO.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(MeuDbContext db) : base(db) { }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            return await Buscar(p => p.FornecedorId == fornecedorId);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            return await Db.Produtos.AsNoTracking().Include(p => p.Fornecedor).OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<Produto> ObterProdutoFornecedor(Guid id)
        {
            return await Db.Produtos.AsNoTracking().Include(p => p.Fornecedor).FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}